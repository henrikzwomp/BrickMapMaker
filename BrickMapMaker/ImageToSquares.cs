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
        public static List<SquareTypes> Go(int square_size, int squaresX, int squaresY, Bitmap bitmap)
        {
            var result = ParseInputImage(square_size, bitmap);
            return CreateOutputSquares(squaresX, squaresY, result);
        }

        private static List<InputSquareData> ParseInputImage(int square_size, Bitmap bitmap)
        {
            var squaresX = bitmap.Width / square_size;
            var squaresY = bitmap.Height / square_size;

            var ic = new InputColors();
            Color pixel;

            var result = new List<InputSquareData>();

            for (var sy = 0; sy < squaresY; sy++)
            {
                for (var sx = 0; sx < squaresX; sx++)
                {
                    var square_result = new InputSquareData();

                    for (var py = 0; py < square_size; py++)
                    {
                        for (var px = 0; px < square_size; px++)
                        {
                            pixel = bitmap.GetPixel((sx * square_size) + px, (sy * square_size) + py);

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

                    result.Add(square_result);
                }
            }

            return result;
        }

        private static List<SquareTypes> CreateOutputSquares(int squaresX, int squaresY,
    List<InputSquareData> input)
        {
            var result = new List<SquareTypes>();

            var square_type = SquareTypes.Land;

            foreach (var square in input)
            {
                square_type = SquareTypes.Land;
                square_type = MapConfig.GetSquareType(square_type, square);

                result.Add(square_type);

            }

            return result;
        }

    }
}
