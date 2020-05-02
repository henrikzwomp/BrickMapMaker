using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace BrickMapMaker
{
    class ImageToSquares
    {
        public static List<MapSquare> Go(int square_size, int squaresX, int squaresY, Bitmap bitmap)
        {
            var result = ParseInputImage(square_size, bitmap);
            return result;
        }

        private static List<MapSquare> ParseInputImage(int square_size, Bitmap bitmap)
        {
            var squaresX = bitmap.Width / square_size;
            var squaresZ = bitmap.Height / square_size;

            var configs = MapConfig.GetSquareConfigurations();
            Color pixel;

            var result = new List<MapSquare>();

            for (var sz = 0; sz < squaresZ; sz++)
            {
                for (var sx = 0; sx < squaresX; sx++)
                {
                    for (var py = 0; py < square_size; py++)
                    {
                        for (var px = 0; px < square_size; px++)
                        {
                            pixel = bitmap.GetPixel((sx * square_size) + px, (sz * square_size) + py);

                            var square_config = configs.FirstOrDefault(x => x.InputColor == pixel);

                            if (square_config == null)
                                continue;

                            square_config.Count++;
                        }
                    }

                    var map_square = new MapSquare() { 
                        Type = MapConfig.CalculateSquareType(configs), 
                        PositionX = sx, PositionZ = sz 
                    };

                    MapConfig.ResetCount();

                    result.Add(map_square);
                }
            }

            return result;
        }


    }

    public class MapSquare
    {
        public SquareTypes Type { get; set; }
        public int PositionX { get; set; }
        public int PositionZ { get; set; }
    }
}
