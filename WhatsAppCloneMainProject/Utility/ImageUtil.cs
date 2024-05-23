using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace WhatsAppCloneMainProject.Utility
{
    /// <summary>
    /// This is an Image utility class for converting images to different formats
    /// </summary>
    public static class ImageUtil
    {
        /// <summary>
        /// This method creates a new BigMapImage from the path provided, 
        /// encode the image to a frame and saves the encoded data as a memory stream
        /// and convert the memory stream to bytes
        /// </summary>
        public static byte[] ImageConverter(string path)
        {
            BitmapImage newImage = new BitmapImage(new Uri(path));

            JpegBitmapEncoder encoder = new JpegBitmapEncoder();

            encoder.Frames.Add(BitmapFrame.Create(newImage));

            using (MemoryStream memoryStream = new MemoryStream())
            {
                encoder.Save(memoryStream);
                return memoryStream.ToArray();
            }
        }

        public static byte[] ImageConverter(Uri uri)
        {
            BitmapImage newImage = new BitmapImage(uri);

            JpegBitmapEncoder encoder = new JpegBitmapEncoder();

            encoder.Frames.Add(BitmapFrame.Create(newImage));

            using (MemoryStream memoryStream = new MemoryStream())
            {
                encoder.Save(memoryStream);
                return memoryStream.ToArray();
            }
        }

        /// <summary>
        /// Convert an array of bytes to an Image source by creating a BitMapImage
        /// using the bytes as a memory stream.
        /// </summary>
        /// <param name="imageBytes"></param>
        /// <returns></returns>
        public static BitmapImage BytesToImageSource(byte[] imageBytes)
        {
            if (imageBytes == null || imageBytes.Length == 0) return null;

            var image = new BitmapImage();
            using (var mem = new MemoryStream(imageBytes))
            {
                mem.Position = 0;
                image.BeginInit();
                image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = null;
                image.StreamSource = mem;
                image.EndInit();
            }
            image.Freeze();
            return image;
        }

        /// <summary>
        /// Similar to ImageConverter encode the BitMapImage then save encoded data
        /// as memory stream and convert it to an array of bytes
        /// </summary>
        /// <param name="imageSource"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static byte[] ImageSourceToBytes(BitmapSource imageSource, string format)
        {
            if (imageSource == null) return null;

            BitmapEncoder encoder = new JpegBitmapEncoder(); ;

            encoder.Frames.Add(BitmapFrame.Create(imageSource));

            using (var stream = new MemoryStream())
            {
                encoder.Save(stream);
                return stream.ToArray();
            }
        }
    }
}

