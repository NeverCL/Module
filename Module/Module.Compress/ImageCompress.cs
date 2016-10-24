using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Module.Compress
{
    /// <summary>
    /// 图片压缩
    /// </summary>
    public static class ImageCompress
    {
        /// <summary>
        /// 保存图片
        /// </summary>
        /// <param name="bitmap"></param>
        /// <param name="filePath"></param>
        public static void SaveImg(this Image bitmap, string filePath)
        {
            using (bitmap)
                bitmap.Save(filePath, GetImageFormat(filePath));
        }

        public static ImageFormat GetImageFormat(string filePath)
        {
            var ext = (ImageFormat)null;
            switch (Path.GetExtension(filePath))
            {
                case ".gif":
                case ".GIF":
                    ext = ImageFormat.Gif;
                    break;
                case ".jpg":
                case ".JPG":
                    ext = ImageFormat.Jpeg;
                    break;
                case ".bmp":
                case ".BMP":
                    ext = ImageFormat.Bmp;
                    break;
                case ".png":
                case ".PNG":
                    ext = ImageFormat.Png;
                    break;
            }
            return ext;
        }

        /// <summary>
        /// 压缩图片
        /// </summary>
        /// <param name="sourceImg"></param>
        /// <param name="targetSize"></param>
        /// <returns></returns>
        public static Image CompressionToSize(this Image sourceImg, long targetSize = 200 * 1024)
        {
            using (var memory = new MemoryStream())
            {
                sourceImg.Save(memory, sourceImg.RawFormat);
                var fileSize = memory.Length;
                return CompressionToSize(sourceImg, fileSize, targetSize);
            }
        }

        public static Image CompressionToSize(this Image sourceImg, long fileSize, long targetSize)
        {
            var times = 1 / Math.Ceiling(Math.Sqrt(fileSize / (double)targetSize));
            return CompressionByTimes(sourceImg, times);
        }

        /// <summary>
        /// 根据压缩系数 压缩图片
        /// </summary>
        /// <param name="sourceImg"></param>
        /// <param name="times"></param>
        /// <returns></returns>
        public static Image CompressionByTimes(this Image sourceImg, double times = 0.5)
        {
            var width = (int)Math.Floor(sourceImg.Width * times);
            var height = (int)Math.Floor(sourceImg.Height * times);
            var targetImg = new Bitmap(sourceImg, width, height);
            return targetImg;
        }
    }
}
