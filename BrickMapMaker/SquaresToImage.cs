using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace BrickMapMaker
{
    class SquaresToImage
    {
        public static void CreatePreviewImage(int squaresX, int squaresZ, List<MapSquare> map_squares)
        {
            var output_bmp = new Bitmap(squaresX, squaresZ);
            var oc = new OutputColors();

            Color color_to_use;


            foreach (var square in map_squares)
            {
                color_to_use = MapConfig.GetOutputColor(oc, square.Type);

                output_bmp.SetPixel(square.PositionX, square.PositionZ, color_to_use);
            }

            output_bmp.Save("result.png", System.Drawing.Imaging.ImageFormat.Png);
        }
    }
}
