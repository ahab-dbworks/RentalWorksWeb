using FwStandard.SqlServer;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;

namespace FwStandard.Utilities
{
    public class FwGraphics
    {
        static int ThumbnailWidth = 128, ThumbnailHeight = 128;
        //---------------------------------------------------------------------------------------------
        public static byte[] ConvertToJpg(byte[] image, ref int width, ref int height)
        {
            byte[] outputImage;
            MemoryStream inputStream, outputStream;
            Bitmap inputBitmap;
            ImageCodecInfo[] codecs;
            ImageCodecInfo encoder;
            EncoderParameters encoderParams;

            outputImage = null;
            inputStream = null;
            outputStream = null;
            inputBitmap = null;
            codecs = null;
            encoder = null;
            encoderParams = null;
            try
            {
                inputStream = new MemoryStream(image);
                inputBitmap = new Bitmap(inputStream);
                width = inputBitmap.Width;
                height = inputBitmap.Height;
                if (inputBitmap.RawFormat != ImageFormat.Jpeg)
                {
                    codecs = ImageCodecInfo.GetImageEncoders();
                    foreach (ImageCodecInfo codec in codecs)
                    {
                        if (codec.MimeType == "image/jpeg")
                        {
                            encoder = codec;
                            break;
                        }
                    }
                    encoderParams = new EncoderParameters();
                    encoderParams.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, (long)80);
                    outputStream = new MemoryStream();
                    inputBitmap.Save(outputStream, encoder, encoderParams);
                    outputImage = new byte[(int)outputStream.Length];
                    outputStream.Position = 0;
                    outputStream.Read(outputImage, 0, (int)outputStream.Length);
                }
                else
                {
                    outputImage = image;
                }
            }
            //catch (ArgumentException ex)
            //{
            //    //FwErrorCollection.Current.Add("Unsupported image format.  The following image formats are supported: bmp, gif, jpg, png, tif.");
            //}
            //catch(Exception ex)
            //{
            //    //FwErrorCollection.Current.Add(ex);
            //}
            catch
            {

            }
            finally
            {
                if (inputStream != null)
                {
                    inputStream.Close();
                    inputStream.Dispose();
                }
                if (outputStream != null)
                {
                    outputStream.Close();
                    outputStream.Dispose();
                }
                inputBitmap.Dispose();
            }
            return outputImage;
        }
        //---------------------------------------------------------------------------------------------
        public static byte[] GetJpgThumbnail(byte[] image)
        {
            decimal scale;
            byte[] thumbnail;
            Graphics graphicsCanvas;
            ImageCodecInfo[] codecs;
            ImageCodecInfo encoder;
            EncoderParameters encoderParams;
            int width, height;

            width = FwGraphics.ThumbnailWidth;
            height = FwGraphics.ThumbnailHeight;
            thumbnail = new byte[0];
            using (MemoryStream inputStream = new MemoryStream(image))
            using (Bitmap inputBitmap = new Bitmap(inputStream))
            using (MemoryStream outputStream = new MemoryStream())
            {
                if (inputBitmap.Width > inputBitmap.Height)
                {
                    scale = (decimal)width / (decimal)inputBitmap.Width;
                    height = FwConvert.ToInt32(Math.Round(scale * inputBitmap.Height));
                }
                else
                {
                    scale = (decimal)height / (decimal)inputBitmap.Height;
                    width = FwConvert.ToInt32(Math.Round(scale * inputBitmap.Width));
                }
                using (Bitmap thumbnailBitmap = new Bitmap(width, height))
                {
                    graphicsCanvas = Graphics.FromImage(thumbnailBitmap);
                    graphicsCanvas.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                    graphicsCanvas.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    graphicsCanvas.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    graphicsCanvas.DrawImage(inputBitmap, 0, 0, width, height);
                    codecs = ImageCodecInfo.GetImageEncoders();
                    foreach (ImageCodecInfo codec in codecs)
                    {
                        if (codec.MimeType == "image/jpeg")
                        {
                            encoder = codec;
                            encoderParams = new EncoderParameters();
                            encoderParams.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, (long)80);
                            thumbnailBitmap.Save(outputStream, encoder, encoderParams);
                            thumbnail = new byte[(int)outputStream.Length];
                            outputStream.Position = 0;
                            outputStream.Read(thumbnail, 0, (int)outputStream.Length);
                            break;
                        }
                    }

                }
            }

            return thumbnail;
        }
        //---------------------------------------------------------------------------------------------
    }
}
