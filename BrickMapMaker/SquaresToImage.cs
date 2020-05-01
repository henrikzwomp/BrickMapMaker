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
        public static void CreatePreviewImage(int squaresX, int squaresZ, List<SquareTypes> result)
        {
            var output_bmp = new Bitmap(squaresX, squaresZ);
            var oc = new OutputColors();
            int x = 0;
            int y = 0;

            Color color_to_use;


            foreach (var square in result)
            {
                color_to_use = MapConfig.GetOutputColor(oc, square);

                output_bmp.SetPixel(x, y, color_to_use);

                x++;

                if (x >= squaresX)
                {
                    y++;
                    x = 0;
                }
            }

            output_bmp.Save("result.png", System.Drawing.Imaging.ImageFormat.Png);
        }
    }
}
