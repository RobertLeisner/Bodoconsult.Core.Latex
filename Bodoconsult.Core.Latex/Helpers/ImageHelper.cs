using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using DocumentFormat.OpenXml.Office2010.PowerPoint;

namespace Bodoconsult.Core.Latex.Helpers
{
    public static class ImageHelper
    {

        /// <summary>
        /// Saves the meta file.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="scale">The scale. Default value is 4.</param>
        /// <param name="backgroundColor">Color of the background.</param>
        /// <param name="format">The format. Default is JPEG.</param>
        /// <param name="parameters">The parameters.</param>
        public static Stream SaveMetaFile(
            Stream source,
            float scale = 4f,
            Color? backgroundColor = null,
            ImageFormat format = null,
            EncoderParameters parameters = null)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            



            var destination = new MemoryStream();

            using (var img = new Metafile(source))
            {
                var f = format ?? ImageFormat.Jpeg;

                //Determine default background color. 
                //Not all formats support transparency. 
                if (backgroundColor == null)
                {
                    var transparentFormats = new[] { ImageFormat.Gif, ImageFormat.Png, ImageFormat.Wmf, ImageFormat.Emf };
                    var isTransparentFormat = transparentFormats.Contains(f);

                    backgroundColor = isTransparentFormat ? Color.Transparent : Color.White;
                }

                //header contains DPI information
                var header = img.GetMetafileHeader();

                //calculate the width and height based on the scale
                //and the respective DPI
                var width = (int)Math.Round((scale * img.Width / header.DpiX * 100), 0, MidpointRounding.ToEven);
                var height = (int)Math.Round((scale * img.Height / header.DpiY * 100), 0, MidpointRounding.ToEven);

                using (var bitmap = new Bitmap(width, height))
                {
                    using (var g = Graphics.FromImage(bitmap))
                    {
                        //fills the background
                        g.Clear(backgroundColor.Value);

                        //reuse the width and height to draw the image
                        //in 100% of the square of the bitmap
                        g.DrawImage(img, 0, 0, bitmap.Width, bitmap.Height);
                    }

                    //get codec based on GUID
                    var codec = ImageCodecInfo.GetImageEncoders().FirstOrDefault(c => c.FormatID == f.Guid);

                    bitmap.Save(destination, codec, parameters);
                }
            }

            destination.Position = 0;
            return destination;
        }


        /// <summary>
        /// Saves the meta file.
        /// </summary>
        /// <param name="path">The source file path.</param>
        /// <param name="scale">The scale. Default value is 4.</param>
        /// <param name="backgroundColor">Color of the background.</param>
        /// <param name="format">The format. Default is JPEG.</param>
        /// <param name="parameters">The parameters.</param>
        public static Stream SaveMetaFile(
            string path,
            float scale = 4f,
            Color? backgroundColor = null,
            ImageFormat format = null,
            EncoderParameters parameters = null)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException(nameof(path));
            }





            var destination = new MemoryStream();

            using (var img = new Metafile(path))
            {
                var f = format ?? ImageFormat.Jpeg;

                //Determine default background color. 
                //Not all formats support transparency. 
                if (backgroundColor == null)
                {
                    var transparentFormats = new[] { ImageFormat.Gif, ImageFormat.Png, ImageFormat.Wmf, ImageFormat.Emf };
                    var isTransparentFormat = transparentFormats.Contains(f);

                    backgroundColor = isTransparentFormat ? Color.Transparent : Color.White;
                }

                //header contains DPI information
                var header = img.GetMetafileHeader();

                //calculate the width and height based on the scale
                //and the respective DPI
                var width = (int)Math.Round((scale * img.Width / header.DpiX * 100), 0, MidpointRounding.ToEven);
                var height = (int)Math.Round((scale * img.Height / header.DpiY * 100), 0, MidpointRounding.ToEven);

                using (var bitmap = new Bitmap(width, height))
                {
                    using (var g = Graphics.FromImage(bitmap))
                    {
                        //fills the background
                        g.Clear(backgroundColor.Value);

                        //reuse the width and height to draw the image
                        //in 100% of the square of the bitmap
                        g.DrawImage(img, 0, 0, bitmap.Width, bitmap.Height);
                    }

                    //get codec based on GUID
                    var codec = ImageCodecInfo.GetImageEncoders().FirstOrDefault(c => c.FormatID == f.Guid);

                    bitmap.Save(destination, codec, parameters);
                }
            }

            destination.Position = 0;
            return destination;
        }


        public static Stream ConvertToJpg(Stream bmp)
        {

            var bitmap = Image.FromStream(bmp);

            var stream = new MemoryStream();
            var encoderParameters = new EncoderParameters(1)
            {
                Param = {[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 100L)}
            };
            bitmap.Save(stream, GetEncoder(ImageFormat.Jpeg), encoderParameters);

            return stream;
        }

        public static ImageCodecInfo GetEncoder(ImageFormat format)
        {
            var codecs = ImageCodecInfo.GetImageDecoders();
            return codecs.FirstOrDefault(codec => codec.FormatID == format.Guid);
        }

    }
}
