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
        Mountain, 
        Forest, 
        Marsh, 
        Water, 
        Land, 
        Sea, 
        Road, 
        Ignore,
        DarkForest,
        OldForest,
        YellowForest,
        GrassLand,
        DarkMountain,
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
                    Type = SquareTypes.DarkForest,
                    InputColor = Color.FromArgb(127, 0, 0),
                    OutputColor = Color.FromArgb(114, 0, 18), // Dark Red, 141
                    MaterialId = 154,
                },
                new SquareConfiguration()
                {
                    Type = SquareTypes.DarkMountain,
                    InputColor = Color.FromArgb(128, 128, 128),
                    OutputColor = Color.FromArgb(126, 156, 149), // Dark Gray, 199
                    MaterialId = 199,
                },
                new SquareConfiguration()
                {
                    Type = SquareTypes.Forest,
                    InputColor = Color.FromArgb(116, 164, 92),
                    OutputColor = Color.FromArgb(0, 69, 26), // DarkGreen, 141
                    MaterialId = 141, 
                },
                new SquareConfiguration()
                {
                    Type = SquareTypes.GrassLand,
                    InputColor = Color.FromArgb(45, 255, 45),
                    OutputColor = Color.FromArgb(88, 171, 65), // Bright Green
                    MaterialId = 37,
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
                    Type = SquareTypes.OldForest,
                    InputColor = Color.FromArgb(59, 91, 43),
                    OutputColor = Color.FromArgb(135, 128, 76), // Olive Green, 330
                    MaterialId = 330,
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
                new SquareConfiguration()
                {
                    Type = SquareTypes.YellowForest,
                    InputColor = Color.FromArgb(255, 216, 0),
                    OutputColor = Color.FromArgb(252, 172, 0), // Flame Yellowing Orange, 191
                    MaterialId = 191,
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
            if (!square_configs.Any(x => x.Count > 0))
                return SquareTypes.Land;

            int road_count = square_configs.First(x => x.Type == SquareTypes.Road).Count;
            int water_count = square_configs.First(x => x.Type == SquareTypes.Water).Count;
            int non_water_count = square_configs.Count(x => x.Type != SquareTypes.Water && x.Count > 0);

            if (non_water_count == 0 && water_count > 0)
                return SquareTypes.Sea;

            if (road_count > 0)
                return SquareTypes.Road;
            
            else if (water_count > 0)
                return SquareTypes.Water;

            return square_configs.OrderByDescending(x => x.Count).First().Type;
        }
    }

}
