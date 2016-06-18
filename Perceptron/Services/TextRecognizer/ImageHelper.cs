using System.Drawing;

namespace Perceptron.Services.TextRecognizer
{
    public class ImageHelper
    {
        static public double[] GetVector(Bitmap image)
        {
            var vector = new double[image.Height * image.Width];
            for (var i = 0; i < image.Height; i++)
                for (var j = 0; j < image.Width; j++)
                    if (IsBlack(image.GetPixel(j, i)))
                        vector[i * image.Width + j] = 1;
                    else
                        vector[i * image.Width + j] = 0;

            return vector;
        }

        static private bool IsBlack(Color color)
        {
            return color.R == 0 && color.G == 0 && color.B == 0;
        }

        public static Bitmap ChangeImageSize(Bitmap image, int newWidth, int newHeight)
        {
            /*var scaleX = 1.0;
            var scaleY = 1.0;

            if (image.Height > image.Width)
                scaleX = ((float)newHeight / image.Height);

            if (image.Height < image.Width)
                scaleY = ((float)newWidth / image.Width);

            var newImage = new Bitmap(image, new Size((int)(image.Width * scaleX), (int)(image.Height * scaleY)));

            var tmpFile = new Bitmap(newWidth, newHeight);
            var gr = Graphics.FromImage(tmpFile);
            gr.Clear(Color.White);
            gr.DrawImage(newImage, new Point((int)((newWidth - image.Width * scaleX) / 2), 0));

            return tmpFile;*/

            if (image.Height > image.Width)
                newWidth = (int)(image.Width / ((float)image.Height / 16));

            if (image.Height < image.Width)
                newHeight = (int)(image.Height / ((float)image.Width / 16));

            Bitmap newImage = new Bitmap(image, new Size(newWidth, newHeight));

            Bitmap tmpFile = new Bitmap(16, 16);
            Graphics gr = Graphics.FromImage(tmpFile);
            gr.Clear(Color.White);
            gr.DrawImage(newImage, new Point((16 - newWidth) / 2, 0));

            return tmpFile;
        }
    }
}
