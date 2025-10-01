using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;

namespace FTC_Generic_Printing_App_POC.Utils
{
    public class ThermalPrinterImageUtility
    {
        // The selected image processing mode should be based on the image type.
        // - Images with high contrast and simple lines (e.g. logos) work best with HighContrast mode.
        // - Images/photos with gradients and many colors work better with Dithering mode.
        // - Images with inverted colors (e.g. white on black) can be corrected with Invert mode.
        // For performance reasons, the least processing mode that gives acceptable results should be chosen.
        // For this, is recommended that the raw image is pre-processed (cropped, resized, adjusted contrast)
        // before being used here togehter with the None mode.
        public enum ImageProcessingMode
        {
            None,
            HighContrast,
            Dithering,
            Invert
        }

        public class ImageConfig
        {
            // Target width in pixels (For thermal printers: 384, 576, 832)
            public int PrinterWidth { get; set; } = 384;

            public ImageProcessingMode ProcessingMode { get; set; } = ImageProcessingMode.None;

            // Parameters for HighContrast mode (0-255, default 128)
            // Lower = more black, Higher = more white
            public int ContrastThreshold { get; set; } = 128;

            public bool MaintainAspectRatio { get; set; } = true;
        }

        // Load an embedded resource image and convert to thermal printer (POS) commands
        // Images should be added to the project as Embedded Resource on the Resources folder from the project root.
        // If so, the resource path should be (example): "FTC_Generic_Printing_App_POC.Resources.your_image.png"
        public static List<byte[]> LoadEmbeddedImageAsCommands(
            string resourceName,
            ImageConfig config = null)
        {
            config = config ?? new ImageConfig();

            try
            {
                var assembly = Assembly.GetExecutingAssembly();

                using (Stream resourceStream = assembly.GetManifestResourceStream(resourceName))
                {
                    if (resourceStream == null)
                    {
                        string[] resources = assembly.GetManifestResourceNames();
                        AppLogger.LogError($"Embedded resource not found: {resourceName}");
                        AppLogger.LogInfo($"Available resources: {string.Join(", ", resources)}");
                        return null;
                    }

                    using (Bitmap originalImage = new Bitmap(resourceStream))
                    {
                        return ProcessImageToCommands(originalImage, config);
                    }
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"Error loading embedded image: {ex.Message}", ex);
                return null;
            }
        }

        // Process a bitmap image into thermal printer (POS) commands
        private static List<byte[]> ProcessImageToCommands(Bitmap originalImage, ImageConfig config)
        {
            // Calculate dimensions
            int newWidth = config.PrinterWidth;
            int newHeight = originalImage.Height;

            if (config.MaintainAspectRatio)
            {
                newHeight = (int)((float)originalImage.Height / originalImage.Width * newWidth);
            }

            using (Bitmap resizedImage = new Bitmap(originalImage, new Size(newWidth, newHeight)))
            {
                // Apply processing based on mode (see ImageProcessingMode)
                Bitmap processedImage = resizedImage;

                switch (config.ProcessingMode)
                {
                    case ImageProcessingMode.HighContrast:
                        processedImage = ApplyHighContrast(resizedImage, config.ContrastThreshold);
                        break;

                    case ImageProcessingMode.Dithering:
                        processedImage = ApplyDithering(resizedImage);
                        break;

                    case ImageProcessingMode.Invert:
                        processedImage = ApplyInvert(resizedImage);
                        break;

                    case ImageProcessingMode.None:
                    default:
                        break;
                }

                byte[] rasterData = ConvertBitmapToRaster(processedImage);
                return GenerateRasterCommands(rasterData, newWidth, newHeight);
            }
        }

        // Apply high contrast conversion for HighContrast ImageProcessingMode
        private static Bitmap ApplyHighContrast(Bitmap source, int threshold)
        {
            Bitmap result = new Bitmap(source.Width, source.Height);

            for (int y = 0; y < source.Height; y++)
            {
                for (int x = 0; x < source.Width; x++)
                {
                    Color pixel = source.GetPixel(x, y);
                    double luminance = 0.299 * pixel.R + 0.587 * pixel.G + 0.114 * pixel.B;

                    Color newColor = luminance < threshold
                        ? Color.Black
                        : Color.White;

                    result.SetPixel(x, y, newColor);
                }
            }

            return result;
        }

        // Apply dithering for Dithering ImageProcessingMode
        private static Bitmap ApplyDithering(Bitmap source)
        {
            Bitmap result = new Bitmap(source.Width, source.Height);
            int[,] gray = new int[source.Width, source.Height];

            // Convert to grayscale
            for (int y = 0; y < source.Height; y++)
            {
                for (int x = 0; x < source.Width; x++)
                {
                    Color pixel = source.GetPixel(x, y);
                    gray[x, y] = (int)(0.299 * pixel.R + 0.587 * pixel.G + 0.114 * pixel.B);
                }
            }

            // Floyd-Steinberg dithering
            for (int y = 0; y < source.Height; y++)
            {
                for (int x = 0; x < source.Width; x++)
                {
                    int oldPixel = gray[x, y];
                    int newPixel = oldPixel < 128 ? 0 : 255;
                    gray[x, y] = newPixel;

                    int error = oldPixel - newPixel;

                    if (x + 1 < source.Width)
                        gray[x + 1, y] += error * 7 / 16;
                    if (y + 1 < source.Height)
                    {
                        if (x > 0)
                            gray[x - 1, y + 1] += error * 3 / 16;
                        gray[x, y + 1] += error * 5 / 16;
                        if (x + 1 < source.Width)
                            gray[x + 1, y + 1] += error * 1 / 16;
                    }

                    result.SetPixel(x, y, newPixel == 0 ? Color.Black : Color.White);
                }
            }

            return result;
        }

        // Invert image colors
        private static Bitmap ApplyInvert(Bitmap source)
        {
            Bitmap result = new Bitmap(source.Width, source.Height);

            for (int y = 0; y < source.Height; y++)
            {
                for (int x = 0; x < source.Width; x++)
                {
                    Color pixel = source.GetPixel(x, y);
                    Color inverted = Color.FromArgb(
                        pixel.A,
                        255 - pixel.R,
                        255 - pixel.G,
                        255 - pixel.B
                    );
                    result.SetPixel(x, y, inverted);
                }
            }

            return result;
        }

        // Convert bitmap to monochrome raster format
        private static byte[] ConvertBitmapToRaster(Bitmap bitmap)
        {
            int width = bitmap.Width;
            int height = bitmap.Height;
            int bytesPerLine = (width + 7) / 8;
            byte[] rasterData = new byte[bytesPerLine * height];

            int darkPixelCount = 0;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Color pixel = bitmap.GetPixel(x, y);
                    double luminance = 0.299 * pixel.R + 0.587 * pixel.G + 0.114 * pixel.B;

                    if (luminance < 128)
                    {
                        darkPixelCount++;
                        int byteIndex = y * bytesPerLine + x / 8;
                        int bitIndex = 7 - (x % 8);
                        rasterData[byteIndex] |= (byte)(1 << bitIndex);
                    }
                }
            }

            int nonZeroBytes = rasterData.Count(b => b != 0);
            AppLogger.LogInfo($"Raster data: {width}x{height}, {darkPixelCount} dark pixels, {nonZeroBytes} non-zero bytes");

            return rasterData;
        }

        // Generate ESC/POS commands for raster image printing (GS v 0)
        private static List<byte[]> GenerateRasterCommands(byte[] rasterData, int width, int height)
        {
            List<byte[]> commands = new List<byte[]>();
            int bytesPerLine = (width + 7) / 8;

            byte[] command = new byte[8 + rasterData.Length];

            command[0] = 0x1D; // GS
            command[1] = 0x76; // v
            command[2] = 0x30; // 0
            command[3] = 0x00; // m = 0 (normal mode)
            command[4] = (byte)(bytesPerLine & 0xFF);        // xL
            command[5] = (byte)((bytesPerLine >> 8) & 0xFF); // xH
            command[6] = (byte)(height & 0xFF);              // yL
            command[7] = (byte)((height >> 8) & 0xFF);       // yH

            Array.Copy(rasterData, 0, command, 8, rasterData.Length);

            commands.Add(command);

            return commands;
        }


        // Load an image from normal file path (not as Embedded Resource) and convert to thermal printer commands
        public static List<byte[]> LoadImageFromFileAsCommands(
            string filePath,
            ImageConfig config = null)
        {
            config = config ?? new ImageConfig();

            try
            {
                using (Bitmap originalImage = new Bitmap(filePath))
                {
                    return ProcessImageToCommands(originalImage, config);
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"Error loading image from file: {ex.Message}", ex);
                return null;
            }
        }
    }
}
