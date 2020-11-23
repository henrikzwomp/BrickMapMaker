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
            var rm = new RiverMaker(_brick_repo, new BrickRiverPartSelector());

            var result = new List<Brick>();

            var big_map = CreateMapFromInput(squaresX, squaresZ, map_squares);

            // ToDo Fix coasts.

            var group_counter = 0;

            CoastFixer.Go(big_map);

            rm.CreateBrickRivers(big_map, group_counter, result);

            //var maps = CreateMapsFromInput(squaresX, squaresZ, sub_part_max_x, sub_part_max_z, map_squares);
            var maps = SplitMap(big_map, sub_part_max_x, sub_part_max_z);

            var ref_counter = result.Count;
            group_counter++;


            var square_configs = MapConfig.GetSquareConfigurations();

            foreach(var map in maps)
            {

                foreach (SquareTypes current_type in (SquareTypes[])Enum.GetValues(typeof(SquareTypes)))
                {
                    if (current_type == SquareTypes.Ignore)
                        continue;

                    var material_id = square_configs.First(x => x.Type ==  current_type).MaterialId;

                    var brick_designs = _brick_repo.GetBrickSizesForMaterialId(material_id);

                    foreach (var brick_design in brick_designs)
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

                                if(_brick_repo.IfBrickMaxUsageHasBeenReached(material_id, brick_design.DesignID))
                                    continue;

                                if (DoesBrickFitOnMap(current_type, map, x, z, brick_design.SizeX, brick_design.SizeZ))
                                {
                                    var new_brick = _brick_repo.GetBrick(brick_design, material_id, ref_counter, map_square.PositionX, map_square.PositionZ);
                                    new_brick.GroupId = group_counter;
                                    ref_counter++;

                                    result.Add(new_brick);

                                    ClearAreaOfMap(map, x, z, brick_design.SizeX, brick_design.SizeZ);
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

        private MapSquare[,] CreateMapFromInput(int squares_x, int squares_z, List<MapSquare> map_squares)
        {
            var result = new MapSquare[squares_x, squares_z];

            for (var start_x = 0; start_x < squares_x; start_x++)
            {
                for (var start_z = 0; start_z < squares_z; start_z++)
                {
                    result[start_x, start_z] = map_squares[start_x + (squares_x * start_z)];
                }
            }

            return result;
        }

        private List<MapSquare[,]> SplitMap(MapSquare[,] map, int sub_part_max_x, int sub_part_max_z)
        {
            var squares_x = map.GetLength(0);
            var squares_z = map.GetLength(1);

            var map_squares = new List<MapSquare>();
            for (int z = 0; z < map.GetLength(1); z++)
            {
                for (int x = 0; x < map.GetLength(0); x++)
                {
                    map_squares.Add(map[x, z]);
                }
            }



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

                    var sub_map = new MapSquare[sub_part_x, sub_part_z];

                    foreach (var square in map_squares.Where(x =>
                    x.PositionX >= start_x && x.PositionX < start_x + sub_part_x &&
                    x.PositionZ >= start_z && x.PositionZ < start_z + sub_part_z
                    ))
                    {
                        sub_map[square.PositionX - start_x, square.PositionZ - start_z] = square;
                    }

                    result.Add(sub_map);
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
