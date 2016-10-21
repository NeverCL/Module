using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Module.Picture
{
    public static class ImageHelper
    {
        /// <summary>
        /// 保存图片
        /// </summary>
        /// <param name="bitmap"></param>
        /// <param name="filePath"></param>
        public static void SaveImg(this Image bitmap, string filePath)
        {
            switch (Path.GetExtension(filePath))
            {
                case ".gif":
                case ".GIF":
                    bitmap.Save(filePath, ImageFormat.Gif);
                    break;
                case ".jpg":
                case ".JPG":
                    bitmap.Save(filePath, ImageFormat.Jpeg);
                    break;
                case ".bmp":
                case ".BMP":
                    bitmap.Save(filePath, ImageFormat.Bmp);
                    break;
                case ".png":
                case ".PNG":
                    bitmap.Save(filePath, ImageFormat.Png);
                    break;
            }
        }

        /// <summary>
        /// 根据指定大小压缩图片
        /// </summary>
        /// <param name="sourceImg">原图像</param>
        /// <param name="fileSize">图像文件大小</param>
        /// <param name="targetSize">文件最大目标大小</param>
        /// <returns></returns>
        public static Image CompressionImg(Image sourceImg, int fileSize, int targetSize = 200 * 1024)
        {
            var times = 1 / (fileSize / (double)(200 * 1024));
            return CompressionImg(sourceImg, times);
        }

        /// <summary>
        /// 调整图片尺寸
        /// </summary>
        /// <param name="sourceImg">原图</param>
        /// <param name="times">系数</param>
        public static Image CompressionImg(Image sourceImg, double times = 0.5)
        {
            var width = (int)(sourceImg.Width * times);
            var height = (int)(sourceImg.Height * times);
            var targetImg = new Bitmap(sourceImg, width, height);
            return targetImg;
        }
    }
}
