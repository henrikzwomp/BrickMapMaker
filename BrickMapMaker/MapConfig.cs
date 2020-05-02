using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace BrickMapMaker
{
    public class SquareConfiguration
    {
        public SquareTypes Type;
        public int MaterialId;
        public Color InputColor;
        public int Count;
        public Color OutputColor;
    }

    public enum SquareTypes
    {
        Mountain, Forest, Marsh, Water, Land, Sea, Road, Ignore
    }

    public class MapConfig
    {
        static IList<SquareConfiguration> _configs;

        public static IList<SquareConfiguration> GetSquareConfigurations()
        {
            if (_configs != null)
                return _configs;

            _configs = new List<SquareConfiguration>()
            {
                new SquareConfiguration()
                {
                    Type = SquareTypes.Forest,
                    InputColor = Color.FromArgb(116, 164, 92),
                    OutputColor = Color.FromArgb(0, 69, 26), // DarkGreen, 141
                    MaterialId = 141, 
                },
                new SquareConfiguration()
                {
                    Type = SquareTypes.Land,
                    InputColor = Color.FromArgb(188, 228, 180),
                    OutputColor = Color.FromArgb(0, 133, 43), // Green, 28
                    MaterialId = 28, 
                },
                new SquareConfiguration()
                {
                    Type = SquareTypes.Marsh,
                    InputColor = Color.FromArgb(156, 196, 148),
                    OutputColor = Color.FromArgb(112, 142, 124),  //SandGreen, 151
                    MaterialId = 151, 
                },
                new SquareConfiguration()
                {
                    Type = SquareTypes.Mountain,
                    InputColor = Color.FromArgb(172, 172, 172),
                    OutputColor = Color.FromArgb(160, 165, 169), // Lbg, 194
                    MaterialId = 194, 
                },
                new SquareConfiguration()
                {
                    Type = SquareTypes.Road,
                    InputColor = Color.FromArgb(212, 164, 92),
                    OutputColor = Color.FromArgb(160, 140, 114), // DarkTan, 138
                    MaterialId = 138,
                },
                new SquareConfiguration()
                {
                    Type = SquareTypes.Sea,
                    OutputColor = Color.FromArgb(25, 50, 90), // DarkBlue, 140
                    MaterialId = 140, 
        },
                new SquareConfiguration()
                {
                    Type = SquareTypes.Water,
                    InputColor = Color.FromArgb(76, 164, 212),
                    OutputColor = Color.FromArgb(0, 85, 191), // Blue, 23
                    MaterialId = 23,
                },
            };

            return _configs;
        }

        internal static void ResetCount()
        {
            if (_configs == null)
                return;

            foreach(var config in _configs)
            {
                config.Count = 0;
            }
        }

        public static SquareTypes CalculateSquareType(IList<SquareConfiguration> square_configs)
        {
            int forest_count = square_configs.First(x => x.Type == SquareTypes.Forest).Count;
            int land_count = square_configs.First(x => x.Type == SquareTypes.Land).Count;
            int marsh_count = square_configs.First(x => x.Type == SquareTypes.Marsh).Count;
            int mountain_count = square_configs.First(x => x.Type == SquareTypes.Mountain).Count;
            int road_count = square_configs.First(x => x.Type == SquareTypes.Road).Count;
            int water_count = square_configs.First(x => x.Type == SquareTypes.Water).Count;

            SquareTypes square_type = SquareTypes.Land;

            if (forest_count == 0 && land_count == 0 && marsh_count == 0
                                && mountain_count == 0 && road_count == 0 && water_count > 0)
                square_type = SquareTypes.Sea;
            else if (road_count > 0)
                square_type = SquareTypes.Road;
            else if (water_count > 0)
                square_type = SquareTypes.Water;
            else if (mountain_count > land_count && mountain_count > forest_count
                && mountain_count > marsh_count)
                square_type = SquareTypes.Mountain;
            else if (forest_count > land_count && forest_count > marsh_count)
                square_type = SquareTypes.Forest;
            else if (marsh_count > land_count)
                square_type = SquareTypes.Marsh;
            return square_type;
        }
    }

}
