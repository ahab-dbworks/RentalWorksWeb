using FwStandard.SqlServer;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Text;

namespace FwStandard.Utilities
{
    public class FwGraphics
    {
        static int ThumbnailWidth = 128, ThumbnailHeight = 128, MaxImageWidth = 1200, MaxImageHeight = 1200, ThumbnailImageJpgQuality = 70, FullSizeImageJpgQuality = 70;
        //---------------------------------------------------------------------------------------------
        //public static byte[] ConvertToJpg(byte[] image, ref int width, ref int height)
        //{
        //    byte[] outputImage;
        //    MemoryStream inputStream, outputStream;
        //    Bitmap inputBitmap;
        //    ImageCodecInfo[] codecs;
        //    ImageCodecInfo encoder;
        //    EncoderParameters encoderParams;

        //    outputImage = null;
        //    inputStream = null;
        //    outputStream = null;
        //    inputBitmap = null;
        //    codecs = null;
        //    encoder = null;
        //    encoderParams = null;
        //    try
        //    {
        //        inputStream = new MemoryStream(image);
        //        inputBitmap = new Bitmap(inputStream);
        //        width = inputBitmap.Width;
        //        height = inputBitmap.Height;
        //        if (inputBitmap.RawFormat.Guid.ToString() != ImageFormat.Jpeg.Guid.ToString())
        //        {
        //            codecs = ImageCodecInfo.GetImageEncoders();
        //            foreach (ImageCodecInfo codec in codecs)
        //            {
        //                if (codec.MimeType == "image/jpeg")
        //                {
        //                    encoder = codec;
        //                    break;
        //                }
        //            }
        //            encoderParams = new EncoderParameters();
        //            encoderParams.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, (long)90);
        //            outputStream = new MemoryStream();
        //            inputBitmap.Save(outputStream, encoder, encoderParams);
        //            outputImage = new byte[(int)outputStream.Length];
        //            outputStream.Position = 0;
        //            outputStream.Read(outputImage, 0, (int)outputStream.Length);
        //        }
        //        else
        //        {
        //            outputImage = image;
        //        }
        //    }
        //    //catch (ArgumentException ex)
        //    //{
        //    //    //FwErrorCollection.Current.Add("Unsupported image format.  The following image formats are supported: bmp, gif, jpg, png, tif.");
        //    //}
        //    //catch(Exception ex)
        //    //{
        //    //    //FwErrorCollection.Current.Add(ex);
        //    //}
        //    catch
        //    {

        //    }
        //    finally
        //    {
        //        if (inputStream != null)
        //        {
        //            inputStream.Close();
        //            inputStream.Dispose();
        //        }
        //        if (outputStream != null)
        //        {
        //            outputStream.Close();
        //            outputStream.Dispose();
        //        }
        //        inputBitmap.Dispose();
        //    }
        //    return outputImage;
        //}
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
                            encoderParams.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, (long)ThumbnailImageJpgQuality);
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
        public static byte[] ResizeAndConvertToJpg(byte[] imageData)
        {
            int widthNew = 0, heightNew = 0;
            using (MemoryStream stream = new MemoryStream(imageData))
            using (Bitmap originalImage = new Bitmap(stream))
            {
                widthNew = originalImage.Width;
                heightNew = originalImage.Height;
                int maxWidth = MaxImageWidth;
                int maxHeight = MaxImageHeight;
                bool isWidthGreaterThanMaxWidth = widthNew > maxWidth;
                bool isHeightGreaterThanMaxHeight = widthNew > maxWidth;
                if (isWidthGreaterThanMaxWidth || isHeightGreaterThanMaxHeight)
                {
                    if (isWidthGreaterThanMaxWidth && isHeightGreaterThanMaxHeight)
                    {
                        int deltaFromMaxWidth = maxWidth - widthNew;
                        int deltaFromMaxHeight = maxHeight - heightNew;
                        if (deltaFromMaxWidth <= deltaFromMaxHeight)
                        {
                            widthNew = maxWidth;
                            heightNew = widthNew * originalImage.Height / originalImage.Width;
                        }
                        else
                        {
                            heightNew = maxHeight;
                            widthNew = heightNew * originalImage.Width / originalImage.Height;
                        }
                    }
                    else if (isWidthGreaterThanMaxWidth)
                    {
                        widthNew = maxWidth;
                        heightNew = widthNew * originalImage.Height / originalImage.Width;
                    }
                    else if (isHeightGreaterThanMaxHeight)
                    {
                        heightNew = maxHeight;
                        widthNew = heightNew * originalImage.Width / originalImage.Height;
                    }
                }
                var destRect = new Rectangle(0, 0, widthNew, heightNew);
                decimal scale;
                byte[] destImage;
                Graphics graphicsCanvas;
                ImageCodecInfo[] codecs;
                ImageCodecInfo encoder;
                EncoderParameters encoderParams;
                destImage = new byte[0];
                using (MemoryStream outputStream = new MemoryStream())
                {
                    if (originalImage.Width > originalImage.Height)
                    {
                        scale = (decimal)widthNew / (decimal)originalImage.Width;
                        heightNew = FwConvert.ToInt32(Math.Round(scale * originalImage.Height));
                    }
                    else
                    {
                        scale = (decimal)heightNew / (decimal)originalImage.Height;
                        widthNew = FwConvert.ToInt32(Math.Round(scale * originalImage.Width));
                    }
                    using (Bitmap destBitmap = new Bitmap(widthNew, heightNew))
                    {
                        graphicsCanvas = Graphics.FromImage(destBitmap);
                        graphicsCanvas.CompositingMode = CompositingMode.SourceCopy;
                        graphicsCanvas.CompositingQuality = CompositingQuality.HighQuality;
                        graphicsCanvas.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        graphicsCanvas.SmoothingMode = SmoothingMode.HighQuality;
                        graphicsCanvas.PixelOffsetMode = PixelOffsetMode.HighQuality;
                        using (var wrapMode = new ImageAttributes())
                        {
                            wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                            graphicsCanvas.DrawImage(originalImage, destRect, 0, 0, originalImage.Width, originalImage.Height, GraphicsUnit.Pixel, wrapMode);
                        }
                        codecs = ImageCodecInfo.GetImageEncoders();
                        foreach (ImageCodecInfo codec in codecs)
                        {
                            if (codec.MimeType == "image/jpeg")
                            {
                                encoder = codec;
                                encoderParams = new EncoderParameters();
                                encoderParams.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, (long)FullSizeImageJpgQuality);
                                destBitmap.Save(outputStream, encoder, encoderParams);
                                destImage = new byte[(int)outputStream.Length];
                                outputStream.Position = 0;
                                outputStream.Read(destImage, 0, (int)outputStream.Length);
                                break;
                            }
                        }
                    }
                }
                return destImage;
            }
        }
        //---------------------------------------------------------------------------------------------
    }
}
