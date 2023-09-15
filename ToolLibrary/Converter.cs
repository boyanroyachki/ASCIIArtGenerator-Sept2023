using System.Drawing;

namespace ToolLibrary
{
    public static class Converter
    {
        public static string GenerateASCIIArt(Bitmap img)
        {
            //string asciiChars = "@%#*+=-:. ";
            string asciiChars = "Ñ@#W$9876543210?!abc;:+=-,._ ";
            //string asciiChars = "@&*^+--.. ";
            int width = img.Width;
            int height = img.Height;
            double aspectRatio = (double)height / width;
            int newWidth = 100;
            int newHeight = (int)(aspectRatio * newWidth * 0.55);
            Bitmap resizedImg = new Bitmap(img, newWidth, newHeight);
            Bitmap grayImg = Grayscale(resizedImg);
            string asciiArt = "";

            for (int y = 0; y < grayImg.Height; y++)
            {
                for (int x = 0; x < grayImg.Width; x++)
                {
                    Color pixelColor = grayImg.GetPixel(x, y);
                    int grayValue = (int)(pixelColor.R * 0.3 + pixelColor.G * 0.59 + pixelColor.B * 0.11);
                    int index = grayValue * (asciiChars.Length - 1) / 255;
                    asciiArt += asciiChars[index];
                }
                asciiArt += "\n";
            }
            return asciiArt;
        }

        public static Bitmap Grayscale(Bitmap source)  //only Windows
        {
            Bitmap target = new Bitmap(source.Width, source.Height);

            for (int y = 0; y < source.Height; y++)
            {
                for (int x = 0; x < source.Width; x++)
                {
                    Color pixelColor = source.GetPixel(x, y);
                    int grayValue = (int)(pixelColor.R * 0.3 + pixelColor.G * 0.59 + pixelColor.B * 0.11);
                    Color grayColor = Color.FromArgb(grayValue, grayValue, grayValue);
                    target.SetPixel(x, y, grayColor);
                }
            }
            return target;
        }
    }
}
