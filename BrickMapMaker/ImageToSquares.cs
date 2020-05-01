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

            var ic = new InputColors();
            Color pixel;

            var result = new List<MapSquare>();

            for (var sz = 0; sz < squaresZ; sz++)
            {
                for (var sx = 0; sx < squaresX; sx++)
                {
                    var square_result = new InputSquareData();

                    for (var py = 0; py < square_size; py++)
                    {
                        for (var px = 0; px < square_size; px++)
                        {
                            pixel = bitmap.GetPixel((sx * square_size) + px, (sz * square_size) + py);

                            if (pixel == ic.Mountain)
                                square_result.Mountain++;
                            else if (pixel == ic.Forest)
                                square_result.Forest++;
                            else if (pixel == ic.Marsh)
                                square_result.Marsh++;
                            else if (pixel == ic.Water)
                                square_result.Water++;
                            else if (pixel == ic.Land)
                                square_result.Land++;
                            else if (pixel == ic.Road)
                                square_result.Road++;
                        }
                    }

                    var map_square = new MapSquare() { 
                        Type = MapConfig.GetSquareType(square_result), 
                        PositionX = sx, PositionZ = sz 
                    };
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
