using Ajj.Core.Interface;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Text;

namespace Ajj.Infrastructure.Services
{
    public class ImageProcessing : IImageProcessing
    {
        private Image _image { get; set; }
        public ImageProcessing()
        {

        }
        public ImageProcessing(Stream stream)
        {
            _image = Image.FromStream(stream);
            
        }

        

        public (int Width, int Height) GetImageDimension()
        {
            return (_image.Width, _image.Height);

        }

        public Image Resize(int maxWidth, int maxHeight)
        {
            int width, height;
            #region reckon size 
            if (_image.Width > _image.Height)
            {
                width = maxWidth;
                height = Convert.ToInt32(_image.Height * maxHeight / (double)_image.Width);
            }
            else
            {
                width = Convert.ToInt32(_image.Width * maxWidth / (double)_image.Height);
                height = maxHeight;
            }
            #endregion

            #region get resized bitmap 
            var canvas = new Bitmap(width, height);

            using (var graphics = Graphics.FromImage(canvas))
            {
                graphics.CompositingQuality = CompositingQuality.HighSpeed;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.DrawImage(_image, 0, 0, width, height);
            }

            return canvas;
            #endregion
        }

        public byte[] ToByteArray()
        {
            using (var stream = new MemoryStream())
            {
                _image.Save(stream, _image.RawFormat);
                return stream.ToArray();
            }
        }
    }
}
