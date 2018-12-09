using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace Ajj.Extensions
{
    public static class ImageProcessingExtension
    {
        public static Image Resize(this Image current, int maxWidth, int maxHeight)
        {
            int width, height;
            
            #region reckon size

            if (current.Width > current.Height)
            {
                width = maxWidth;
                height = Convert.ToInt32(current.Height * maxHeight / (double)current.Width);
            }
            else
            {
                width = Convert.ToInt32(current.Width * maxWidth / (double)current.Height);
                height = maxHeight;
            }

            #endregion reckon size

            #region get resized bitmap

            var canvas = new Bitmap(width, height);

            using (var graphics = Graphics.FromImage(canvas))
            {
                graphics.CompositingQuality = CompositingQuality.HighSpeed;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.DrawImage(current, 0, 0, width, height);
            }

            return canvas;

            #endregion get resized bitmap
        }

        public static byte[] ToByteArray(this Image current)
        {
            using (var stream = new MemoryStream())
            {
                current.Save(stream, ImageFormat.Png);
                return stream.ToArray();
            }
        }

        public static void SaveIntoDisk(this Image current, string filePath)
        {
            current.Save(filePath, ImageFormat.Png);
        }
    }
}