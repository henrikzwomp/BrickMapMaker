using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrickMapMaker
{
    public class CoastFixer
    {
        static public void Go(MapSquare[,] map)
        {
            bool surrounded_by_water = true;

            int x_length = map.GetLength(0);
            int z_length = map.GetLength(1);

            for (int z = 0; z < z_length; z++)
            {
                for (int x = 0; x < x_length; x++)
                {
                    var map_square = map[x, z];

                    if (map_square.Type != SquareTypes.Sea && map_square.Type != SquareTypes.Water)
                        continue;

                    surrounded_by_water = true;

                    if (z > 0 && !isWater(map[x, z - 1].Type)) // Above
                        surrounded_by_water = false;
                    else if (z > 0 && x < (x_length - 1) && !isWater(map[x + 1, z - 1].Type)) // Above right
                        surrounded_by_water = false;
                    else if (x < (x_length - 1) && !isWater(map[x + 1, z].Type)) // Right
                        surrounded_by_water = false;
                    else if (z < (z_length - 1) && x < (x_length - 1) && !isWater(map[x + 1, z + 1].Type)) // Below right
                        surrounded_by_water = false;
                    else if (z < (z_length - 1) && !isWater(map[x, z + 1].Type)) // Below
                        surrounded_by_water = false;
                    else if (z < (z_length - 1) && x > 0 && !isWater(map[x - 1, z + 1].Type)) // Below left
                        surrounded_by_water = false;
                    else if (x > 0 && !isWater(map[x - 1, z].Type)) // Left
                        surrounded_by_water = false;
                    else if (z > 0 && x > 0 && !isWater(map[x - 1, z - 1].Type)) // Above left
                        surrounded_by_water = false;

                    if (surrounded_by_water == false)
                        map[x, z].Type = SquareTypes.Water;
                    else if (surrounded_by_water == true)
                        map[x, z].Type = SquareTypes.Sea;
                }
            }
        }

        static private bool isWater(SquareTypes intype)
        {
            return (intype == SquareTypes.Water || intype == SquareTypes.Sea);
        }
    }
}
