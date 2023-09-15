using System.Drawing;
using System.Text;

namespace ASCIIArtGenerator
{
    public class Program
    {
        static void Main(string[] args)
        {
            // Define the character density string
            string density = "Ñ@#W$9876543210?!abc;:+=-,._ ";

            // Load the image (change the path to your specific image)

            Bitmap image = null;
            try
            {
                image = new Bitmap("C:\\path\\to\\your\\image.jpg");
            }
            catch (ArgumentException)
            {
                Console.WriteLine("File not found. The given path is not valid!");
                return;
            }
            catch (Exception ex)
            {
                Console.WriteLine("An unexpected error occurred: " + ex.Message);
                return;
            }


            // Define the new width and dynamically calculate the new height based on the original image's aspect ratio
            int newWidth = 100;
            int newHeight = image.Height * newWidth / image.Width;

            // Create a new bitmap with the new dimensions
            using Bitmap resizedImage = new Bitmap(image, newWidth, newHeight);

            // Initialize a StringBuilder with a capacity equal to the new width to optimize memory allocation
            StringBuilder row = new StringBuilder(newWidth);

            // Loop through each pixel in the height of the image
            for (int y = 0; y < newHeight; y++)
            {
                // Clear the StringBuilder to reuse it in each iteration
                row.Clear();

                // Loop through each pixel in the width of the image
                for (int x = 0; x < newWidth; x++)
                {
                    // Get the color of the current pixel
                    Color pixelColor = resizedImage.GetPixel(x, y);

                    // Calculate the average color value
                    int avg = (pixelColor.R + pixelColor.G + pixelColor.B) / 3;

                    // Map the average color value to a character index based on the density string
                    int charIndex = (int)Map(avg, 0, 255, density.Length - 1, 0);

                    // Get the character that represents the current pixel
                    char c = density[charIndex];

                    // Append the character to the row string, replacing space with "&nbsp;"
                    row.Append(c == ' ' ? "&nbsp;" : c.ToString());
                }

                // Write the row string to the console
                Console.WriteLine(row);
            }
        }

        /// <summary>
        /// Maps a value from one range to another.
        /// </summary>
        /// <param name="value">The value to map.</param>
        /// <param name="fromSource">The inclusive start of the source range.</param>
        /// <param name="toSource">The inclusive end of the source range.</param>
        /// <param name="fromTarget">The inclusive start of the target range.</param>
        /// <param name="toTarget">The inclusive end of the target range.</param>
        /// <returns>The value mapped to the target range.</returns>
        static double Map(double value, double fromSource, double toSource, double fromTarget, double toTarget)
        {
            // Perform the mapping and return the result
            return (value - fromSource) / (toSource - fromSource) * (toTarget - fromTarget) + fromTarget;
        }
    }
}