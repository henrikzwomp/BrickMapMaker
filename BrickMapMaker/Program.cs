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

            var sub_part_max_x = 48;
            var sub_part_max_z = 32;

            var image = Image.FromFile("source_map.png");
            var bitmap = new Bitmap(image);

            var squaresX = bitmap.Width / square_size;
            var squaresZ = bitmap.Height / square_size;

            Console.WriteLine("Output X: " + squaresX);
            Console.WriteLine("Output Y/Z: " + squaresZ);

            Console.WriteLine("Parsing input image and creating Output squares...");
            var map_squares = ImageToSquares.Go(square_size, squaresX, squaresZ, bitmap);
            Console.WriteLine("Squares: " + map_squares.Count);

            Console.WriteLine("Creating preview image...");
            SquaresToImage.CreatePreviewImage(squaresX, squaresZ, map_squares);

            Console.WriteLine("Creating brick list...");
            var s2b = new SquaresToBrickMaps(brick_repo);
            var bricks = s2b.ParseList(squaresX, squaresZ, sub_part_max_x, sub_part_max_z, map_squares);
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

            var groups = new StringBuilder();

            foreach (var group_id in input.Select(x => x.GroupId).Distinct())
            {
                groups.Append("<Group transformation=\"1,0,0,0,1,0,0,0,1,0,0,0\" pivot=\"0, 0, 0\" partRefs =\"");
                groups.Append(string.Join(",", input.Where(x => x.GroupId == group_id).Select(x => x.RefId).ToArray()));
                groups.AppendLine("\"/>");
            }

            string file_data = File.ReadAllText("empty.LXFML");

            file_data = file_data.Replace("[Bricks Here]", bricks.ToString());
            file_data = file_data.Replace("[Groups Here]", groups.ToString());

            File.WriteAllText("map.LXFML", file_data);
        }
    }

    

    

    

    
}
