using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrickMapMaker
{
    public class SquaresToBrickMaps
    {
        public IBrickRepo _brick_repo;

        public SquaresToBrickMaps(IBrickRepo brick_repo)
        {
            _brick_repo = brick_repo;
        }

        public List<Brick> ParseList(int squaresX, int squaresZ, List<SquareTypes> input)
        {
            var result = new List<Brick>();

            var map = CreateMapFromList(squaresX, squaresZ, input);

            var counter = 0;

            foreach (SquareTypes current_type in (SquareTypes[])Enum.GetValues(typeof(SquareTypes)))
            {
                var material_id = MapConfig.GetMaterialId(current_type);

                var brick_sizes = _brick_repo.GetBrickSizesForMaterialId(material_id);

                foreach (var brick_size in brick_sizes)
                {
                    for (int z = 0; z < squaresZ; z++)
                    {
                        for (int x = 0; x < squaresX; x++)
                        {
                            if (map[x, z] == SquareTypes.Ignore)
                                continue;

                            if (map[x, z] != current_type)
                                continue;

                            if (DoesBrickFitOnMap(current_type, map, x, z, brick_size.SizeX, brick_size.SizeZ))
                            {
                                var new_brick = _brick_repo.GetBrick(brick_size, material_id, counter, x, z);
                                counter++;

                                result.Add(new_brick);

                                ClearAreaOfMap(map, x, z, brick_size.SizeX, brick_size.SizeZ);
                            }
                        }
                    }
                }
            }

            

            return result;
        }

        private SquareTypes[,] CreateMapFromList(int squaresX, int squaresZ, List<SquareTypes> result)
        {
            var map = new SquareTypes[squaresX, squaresZ];

            int x = 0;
            int z = 0;

            foreach (var square in result)
            {
                map[x, z] = square;

                x++;

                if (x >= squaresX)
                {
                    z++;
                    x = 0;
                }
            }

            return map;
        }

        private bool DoesBrickFitOnMap(SquareTypes current_type, SquareTypes[,] map, int start_x, int start_z,
            int size_x, int size_z)
        {
            if (map.GetLength(0) < (start_x + size_x))
                return false;

            if (map.GetLength(1) < (start_z + size_z))
                return false;

            for (int z = start_z; z < (start_z + size_z); z++)
            {
                for (int x = start_x; x < (start_x + size_x); x++)
                {
                    if (map[x, z] != current_type)
                        return false;
                }
            }

            return true;
        }

        private void ClearAreaOfMap(SquareTypes[,] map, int start_x, int start_z,
            int size_x, int size_z)
        {
            for (int z = start_z; z < (start_z + size_z); z++)
            {
                for (int x = start_x; x < (start_x + size_x); x++)
                {
                    map[x, z] = SquareTypes.Ignore;
                }
            }
        }
    }
}
