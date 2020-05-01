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

        public List<Brick> ParseList(int squaresX, int squaresZ, 
            int sub_part_max_x, int sub_part_max_z,
            List<MapSquare> map_squares)
        {
            var result = new List<Brick>();

            var maps = CreateMapsFromInput(squaresX, squaresZ, sub_part_max_x, sub_part_max_z, map_squares);

            var ref_counter = 0;
            var group_counter = 0;


            foreach(var map in maps)
            {
                foreach (SquareTypes current_type in (SquareTypes[])Enum.GetValues(typeof(SquareTypes)))
                {
                    var material_id = MapConfig.GetMaterialId(current_type);

                    var brick_sizes = _brick_repo.GetBrickSizesForMaterialId(material_id);

                    foreach (var brick_size in brick_sizes)
                    {
                        for (int z = 0; z < map.GetLength(1); z++)
                        {
                            for (int x = 0; x < map.GetLength(0); x++)
                            {
                                var map_square = map[x, z];

                                if (map_square.Type == SquareTypes.Ignore)
                                    continue;

                                if (map_square.Type != current_type)
                                    continue;

                                if (DoesBrickFitOnMap(current_type, map, x, z, brick_size.SizeX, brick_size.SizeZ))
                                {
                                    var new_brick = _brick_repo.GetBrick(brick_size, material_id, ref_counter, map_square.PositionX, map_square.PositionZ);
                                    new_brick.GroupId = group_counter;
                                    ref_counter++;

                                    result.Add(new_brick);

                                    ClearAreaOfMap(map, x, z, brick_size.SizeX, brick_size.SizeZ);
                                }
                            }
                        }
                    }
                }
                group_counter++;
            }

            return result;
        }

        private List<MapSquare[,]> CreateMapsFromInput(int squares_x, int squares_z,
            int sub_part_max_x, int sub_part_max_z, List<MapSquare> map_squares)
        {
            var result = new List<MapSquare[,]>();

            for (var start_x = 0; start_x < squares_x; start_x += sub_part_max_x)
            {
                for (var start_z = 0; start_z < squares_z; start_z += sub_part_max_z)
                {
                    var sub_part_x = sub_part_max_x;
                    var sub_part_z = sub_part_max_z;

                    if (start_x + sub_part_x > squares_x)
                        sub_part_x = squares_x - start_x;

                    if (start_z + sub_part_z > squares_z)
                        sub_part_z = squares_z - start_z;

                    var map = new MapSquare[sub_part_x, sub_part_z];

                    foreach (var square in map_squares.Where(x =>
                    x.PositionX >= start_x && x.PositionX < start_x + sub_part_x &&
                    x.PositionZ >= start_z && x.PositionZ < start_z + sub_part_z
                    ))
                    {
                        map[square.PositionX - start_x, square.PositionZ - start_z] = square;
                    }

                    result.Add(map);
                }
            }

            return result;
        }

        private bool DoesBrickFitOnMap(SquareTypes current_type, MapSquare[,] map, int start_x, int start_z,
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
                    if (map[x, z].Type != current_type)
                        return false;
                }
            }

            return true;
        }

        private void ClearAreaOfMap(MapSquare[,] map, int start_x, int start_z,
            int size_x, int size_z)
        {
            for (int z = start_z; z < (start_z + size_z); z++)
            {
                for (int x = start_x; x < (start_x + size_x); x++)
                {
                    map[x, z].Type = SquareTypes.Ignore;
                }
            }
        }
    }
}
