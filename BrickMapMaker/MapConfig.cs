using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace BrickMapMaker
{

    public enum SquareTypes
    {
        Mountain, Forest, Marsh, Water, Land, Sea, Road, Ignore
    }

    /*class SquareConfig
    {
        public SquareTypes Type;
        public int MaterialId;
        public Color InputColor;
        public int Count;
        public Color OutputColor;
    }*/

    class InputColors
    {
        public Color Mountain = Color.FromArgb(172, 172, 172);
        public Color Forest = Color.FromArgb(116, 164, 92);
        public Color Marsh = Color.FromArgb(156, 196, 148);
        public Color Water = Color.FromArgb(76, 164, 212);
        public Color Land = Color.FromArgb(188, 228, 180);
        public Color Road = Color.FromArgb(212, 164, 92);
    }

    struct InputSquareData
    {
        public int Mountain;
        public int Forest;
        public int Marsh;
        public int Water;
        public int Land;
        public int Road;
    }

    class OutputColors
    {
        public Color Lbg = Color.FromArgb(160, 165, 169); // 194
        public Color DarkGreen = Color.FromArgb(0, 69, 26); // 141
        public Color SandGreen = Color.FromArgb(112, 142, 124); // 151
        public Color Blue = Color.FromArgb(0, 85, 191); // 23
        public Color Green = Color.FromArgb(0, 133, 43); // 28
        public Color DarkBlue = Color.FromArgb(25, 50, 90); // 140
        public Color DarkTan = Color.FromArgb(160, 140, 114); // 138
    }

    class MapConfig
    {
        public static SquareTypes GetSquareType(SquareTypes square_type, InputSquareData square)
        {
            if (square.Forest == 0 && square.Land == 0 && square.Marsh == 0
                                && square.Mountain == 0 && square.Road == 0 && square.Water > 0)
                square_type = SquareTypes.Sea;
            else if (square.Road > 0)
                square_type = SquareTypes.Road;
            else if (square.Water > 0)
                square_type = SquareTypes.Water;
            else if (square.Mountain > square.Land && square.Mountain > square.Forest
                && square.Mountain > square.Marsh)
                square_type = SquareTypes.Mountain;
            else if (square.Forest > square.Land && square.Forest > square.Marsh)
                square_type = SquareTypes.Forest;
            else if (square.Marsh > square.Land)
                square_type = SquareTypes.Marsh;
            return square_type;
        }

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
