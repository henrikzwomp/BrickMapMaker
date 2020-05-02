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

    [Obsolete]
    public class OutputColors
    {
        public Color Lbg = Color.FromArgb(160, 165, 169); // 194
        public Color DarkGreen = Color.FromArgb(0, 69, 26); // 141
        public Color SandGreen = Color.FromArgb(112, 142, 124); // 151
        public Color Blue = Color.FromArgb(0, 85, 191); // 23
        public Color Green = Color.FromArgb(0, 133, 43); // 28
        public Color DarkBlue = Color.FromArgb(25, 50, 90); // 140
        public Color DarkTan = Color.FromArgb(160, 140, 114); // 138
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
                },
                new SquareConfiguration()
                {
                    Type = SquareTypes.Land,
                    InputColor = Color.FromArgb(188, 228, 180),
                },
                new SquareConfiguration()
                {
                    Type = SquareTypes.Marsh,
                    InputColor = Color.FromArgb(156, 196, 148),
                },
                new SquareConfiguration()
                {
                    Type = SquareTypes.Mountain,
                    InputColor = Color.FromArgb(172, 172, 172),
                },
                new SquareConfiguration()
                {
                    Type = SquareTypes.Road,
                    InputColor = Color.FromArgb(212, 164, 92),
                },
                new SquareConfiguration()
                {
                    Type = SquareTypes.Sea
                },
                new SquareConfiguration()
                {
                    Type = SquareTypes.Water,
                    InputColor = Color.FromArgb(76, 164, 212),
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

        [Obsolete]
        public static int GetMaterialId(SquareTypes square_type)
        {
            var color = 21;

            if (square_type == SquareTypes.Forest)
                color = 141;
            else if (square_type == SquareTypes.Land)
                color = 28;
            else if (square_type == SquareTypes.Marsh)
                color = 151;
            else if (square_type == SquareTypes.Mountain)
                color = 194;
            else if (square_type == SquareTypes.Sea)
                color = 140;
            else if (square_type == SquareTypes.Water)
                color = 23;
            else if (square_type == SquareTypes.Road)
                color = 138;

            return color;
        }

        [Obsolete]
        public static Color GetOutputColor(OutputColors oc, SquareTypes square)
        {
            if (square == SquareTypes.Sea)
                return oc.DarkBlue;
            else if (square == SquareTypes.Water)
                return oc.Blue;
            else if (square == SquareTypes.Mountain)
                return oc.Lbg;
            else if (square == SquareTypes.Forest)
                return oc.DarkGreen;
            else if (square == SquareTypes.Marsh)
                return oc.SandGreen;
            else if (square == SquareTypes.Road)
                return oc.DarkTan;

            return oc.Green;
        }
    }

}
