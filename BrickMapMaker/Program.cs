using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using System.Threading;

namespace BrickMapMaker
{
    class Program
    {
        static void Main(string[] args)
        {
            var brick_repo = new BrickRepo("BrickList.xlsx");

            var square_size = 12;

            var image = Image.FromFile("source_map.png");
            var bitmap = new Bitmap(image);

            var squaresX = bitmap.Width / square_size;
            var squaresZ = bitmap.Height / square_size;

            Console.WriteLine("Output X: " + squaresX);
            Console.WriteLine("Output Y/Z: " + squaresZ);

            Console.WriteLine("Parsing input image and creating Output squares...");
            var output = ImageToSquares.Go(square_size, squaresX, squaresZ, bitmap);
            Console.WriteLine("Squares: " + output.Count);

            Console.WriteLine("Creating preview image...");
            SquaresToImage.CreatePreviewImage(squaresX, squaresZ, output);

            Console.WriteLine("Creating brick list...");
            var s2b = new SquaresToBrickMaps(brick_repo);
            var bricks = s2b.ParseList(squaresX, squaresZ, output);
            Console.WriteLine("Bricks: " + bricks.Count);

            Console.WriteLine("Creating Lxfml file...");
            CreateLxfml(bricks);

            Console.WriteLine("Done");

        }

        private static void CreateLxfml(List<Brick> input)
        {
            Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.GetCultureInfo("en-US");

            var bricks = new StringBuilder();

            foreach(var item in input)
            {
                bricks.Append(item.ToXml() + Environment.NewLine);
            }

            string file_data = File.ReadAllText("empty.LXFML");

            file_data = file_data.Replace("[Bricks Here]", bricks.ToString());

            File.WriteAllText("map.LXFML", file_data);
        }
    }

    

    

    

    
}
