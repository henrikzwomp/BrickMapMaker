using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrickMapMaker
{
    public interface IBrickRiverPartSelector
    {
        DesignItem GetTopLeftCornerPart();
        DesignItem GetTopRightCornerPart();
        DesignItem GetBottomLeftCornerPart();
        DesignItem GetBottomRightCornerPart();
    }

    public class RiverMaker
    {
        private IBrickRepo _brick_repo;
        private SquareConfiguration _water_config;
        private SquareTypes _sea_type;
        IBrickRiverPartSelector _part_selector;

        public RiverMaker(IBrickRepo brick_repo, IBrickRiverPartSelector part_selector)
        {
            _brick_repo = brick_repo;
            _part_selector = part_selector;
            _water_config = MapConfig.GetSquareConfigurations().First(x => x.Type == SquareTypes.Water);
            _sea_type = SquareTypes.Sea;
        }

        public void CreateBrickRivers(MapSquare[,] map, int group_counter, List<Brick> result)
        {
            var water = _water_config.Type;

            // Find all corners
            var found_corners = new List<Corner>();

            bool top_water = false;
            bool bottom_water = false;
            bool left_water = false;
            bool right_water = false;

            int x_length = map.GetLength(0);
            int z_length = map.GetLength(1);

            for (int z = 0; z < z_length; z++)
            {
                for (int x = 0; x < x_length; x++)
                {
                    var map_square = map[x, z];

                    if (map_square.Type != _water_config.Type)
                        continue;

                    top_water = false;
                    bottom_water = false;
                    left_water = false;
                    right_water = false;

                    if (x > 0 && isWater(map[x - 1, z].Type))
                        left_water = true;

                    if (x < (x_length - 1) && isWater(map[x + 1, z].Type))
                        right_water = true;

                    if (z > 0 && isWater(map[x, z - 1].Type))
                        top_water = true;

                    if (z < (z_length - 1) && isWater(map[x, z + 1].Type))
                        bottom_water = true;

                    var result_corner_type = CornerType.NotCorner;

                    if (top_water == false && left_water == false && bottom_water == true && right_water == true)
                        result_corner_type = CornerType.TopLeft;
                    else if (top_water == false && left_water == true && bottom_water == true && right_water == false)
                        result_corner_type = CornerType.TopRight;
                    else if (top_water == true && left_water == false && bottom_water == false && right_water == true)
                        result_corner_type = CornerType.BottomLeft;
                    else if (top_water == true && left_water == true && bottom_water == false && right_water == false)
                        result_corner_type = CornerType.BottomRight;

                    if (result_corner_type != CornerType.NotCorner)
                        found_corners.Add(new Corner() { Type = result_corner_type,
                            ArrayPos0 = x, ArrayPos1 = z, 
                            PositionX = map_square.PositionX, PositionZ = map_square.PositionZ });
                }
            }

            // Remove corner squares from map and Add bricks to result.
            int ref_counter = result.Count();

            foreach (var corner in found_corners)
            {
                map[corner.ArrayPos0, corner.ArrayPos1].Type = SquareTypes.Ignore;

                var brick_design = new DesignItem();
                if (corner.Type == CornerType.TopLeft)
                    brick_design = _part_selector.GetTopLeftCornerPart();
                else if (corner.Type == CornerType.TopRight)
                    brick_design = _part_selector.GetTopRightCornerPart();
                else if (corner.Type == CornerType.BottomLeft)
                    brick_design = _part_selector.GetBottomLeftCornerPart();
                else if (corner.Type == CornerType.BottomRight)
                    brick_design = _part_selector.GetBottomRightCornerPart();

                var new_brick = _brick_repo.GetBrick(brick_design, _water_config.MaterialId, ref_counter, corner.PositionX, corner.PositionZ);
                new_brick.GroupId = group_counter;
                ref_counter++;

                result.Add(new_brick);
            }

        }

        private bool isWater(SquareTypes intype)
        {
            return (intype == _water_config.Type); //  || intype == _sea_type
        }

        private struct Corner
        {
            public CornerType Type { get; set; }
            public int ArrayPos0 { get; set; }
            public int ArrayPos1 { get; set; }
            public int PositionX { get; set; }
            public int PositionZ { get; set; }
        }

        private enum CornerType
        {
            TopLeft, TopRight,
            BottomLeft, BottomRight,
            NotCorner
        }
        /*
         * TopLeft   TopRight
         *       xx xx
         *      xxx xxx
         *      xxx xxx
         * 
         *      xxx xxx
         *      xxx xxx
         *       xx xx
         *  B.Left   B.Right
         */
    }
}
