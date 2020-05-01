using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrickMapMaker
{
    public class SquaresToBrickMaps
    {
        //List<BrickType> _brick_types;

        /*class BrickType
        {
            public string XmlTemplate { get; set; }
            public float OffsetX { get; set; }
            public float OffsetZ { get; set; }
            public int SizeX { get; set; }
            public int SizeZ { get; set; }
        }*/

        public IBrickRepo _brick_repo;

        public SquaresToBrickMaps(IBrickRepo brick_repo)
        {
            _brick_repo = brick_repo;

            /*_brick_types = new List<BrickType>() {
                new BrickType() {SizeX = 16, SizeZ = 16, XmlTemplate = "<Brick refID=\"{0}\" designID=\"91405\"><Part refID=\"{0}\" designID=\"91405\" materials=\"{1}\"><Bone refID=\"{0}\" transformation=\"0,0,1,0,1,0,-1,0,0,{2},0,{3}\"></Bone></Part></Brick>", OffsetX = 0.4f, OffsetZ = 0.4f },
                new BrickType() {SizeX = 16, SizeZ = 8, XmlTemplate = "<Brick refID=\"{0}\" designID=\"92438\"><Part refID=\"{0}\" designID=\"92438\" materials=\"{1}\"><Bone refID=\"{0}\" transformation=\"1,0,0,0,1,0,0,0,1,{2},0,{3}\"></Bone></Part></Brick>", OffsetX = 0.4f, OffsetZ = 6f },
                new BrickType() {SizeX = 8, SizeZ = 16, XmlTemplate = "<Brick refID=\"{0}\" designID=\"92438\"><Part refID=\"{0}\" designID=\"92438\" materials=\"{1}\"><Bone refID=\"{0}\" transformation=\"0,0,1,0,1,0,-1,0,0,{2},0,{3}\"></Bone></Part></Brick>", OffsetX = 0.4f, OffsetZ = 0.4f },
                new BrickType() {SizeX = 16, SizeZ = 6, XmlTemplate = "<Brick refID=\"{0}\" designID=\"3027\"><Part refID=\"{0}\" designID=\"3027\" materials=\"{1}\"><Bone refID=\"{0}\" transformation=\"1,0,0,0,1,0,0,0,1,{2},0,{3}\"></Bone></Part></Brick>", OffsetX = 0.4f, OffsetZ = 4.4f },
                new BrickType() {SizeX = 6, SizeZ = 16, XmlTemplate = "<Brick refID=\"{0}\" designID=\"3027\"><Part refID=\"{0}\" designID=\"3027\" materials=\"{1}\"><Bone refID=\"{0}\" transformation=\"0,0,1,0,1,0,-1,0,0,{2},0,{3}\"></Bone></Part></Brick>", OffsetX = 0.4f, OffsetZ = 0.4f },
                new BrickType() {SizeX = 14, SizeZ = 6, XmlTemplate = "<Brick refID=\"{0}\" designID=\"3456\"><Part refID=\"{0}\" designID=\"3456\" materials=\"{1}\"><Bone refID=\"{0}\" transformation=\"1,0,0,0,1,0,0,0,1,{2},0,{3}\"></Bone></Part></Brick>", OffsetX = 0.4f, OffsetZ = 4.4f },
                new BrickType() {SizeX = 6, SizeZ = 14, XmlTemplate = "<Brick refID=\"{0}\" designID=\"3456\"><Part refID=\"{0}\" designID=\"3456\" materials=\"{1}\"><Bone refID=\"{0}\" transformation=\"0,0,1,0,1,0,-1,0,0,{2},0,{3}\"></Bone></Part></Brick>", OffsetX = 0.4f, OffsetZ = 0.4f },
                new BrickType() {SizeX = 12, SizeZ = 6, XmlTemplate = "<Brick refID=\"{0}\" designID=\"3028\"><Part refID=\"{0}\" designID=\"3028\" materials=\"{1}\"><Bone refID=\"{0}\" transformation=\"1,0,0,0,1,0,0,0,1,{2},0,{3}\"></Bone></Part></Brick>", OffsetX = 0.4f, OffsetZ = 4.4f },
                new BrickType() {SizeX = 6, SizeZ = 12, XmlTemplate = "<Brick refID=\"{0}\" designID=\"3028\"><Part refID=\"{0}\" designID=\"3028\" materials=\"{1}\"><Bone refID=\"{0}\" transformation=\"0,0,1,0,1,0,-1,0,0,{2},0,{3}\"></Bone></Part></Brick>", OffsetX = 0.4f, OffsetZ = 0.4f },
                new BrickType() {SizeX = 8, SizeZ = 8, XmlTemplate = "<Brick refID=\"{0}\" designID=\"41539\"><Part refID=\"{0}\" designID=\"41539\" materials=\"{1}\"><Bone refID=\"{0}\" transformation=\"0,0,1,0,1,0,-1,0,0,{2},0,{3}\"></Bone></Part></Brick>", OffsetX = 0.4f, OffsetZ = 0.4f },
                new BrickType() {SizeX = 10, SizeZ = 6, XmlTemplate = "<Brick refID=\"{0}\" designID=\"3033\"><Part refID=\"{0}\" designID=\"3033\" materials=\"{1}\"><Bone refID=\"{0}\" transformation=\"1,0,0,0,1,0,0,0,1,{2},0,{3}\"></Bone></Part></Brick>", OffsetX = 0.4f, OffsetZ = 4.4f },
                new BrickType() {SizeX = 6, SizeZ = 10, XmlTemplate = "<Brick refID=\"{0}\" designID=\"3033\"><Part refID=\"{0}\" designID=\"3033\" materials=\"{1}\"><Bone refID=\"{0}\" transformation=\"0,0,1,0,1,0,-1,0,0,{2},0,{3}\"></Bone></Part></Brick>", OffsetX = 0.4f, OffsetZ = 0.4f },
                new BrickType() {SizeX = 8, SizeZ = 6, XmlTemplate = "<Brick refID=\"{0}\" designID=\"3036\"><Part refID=\"{0}\" designID=\"3036\" materials=\"{1}\"><Bone refID=\"{0}\" transformation=\"1,0,0,0,1,0,0,0,1,{2},0,{3}\"></Bone></Part></Brick>", OffsetX = 0.4f, OffsetZ = 4.4f },
                new BrickType() {SizeX = 6, SizeZ = 8, XmlTemplate = "<Brick refID=\"{0}\" designID=\"3036\"><Part refID=\"{0}\" designID=\"3036\" materials=\"{1}\"><Bone refID=\"{0}\" transformation=\"0,0,1,0,1,0,-1,0,0,{2},0,{3}\"></Bone></Part></Brick>", OffsetX = 0.4f, OffsetZ = 0.4f },
                new BrickType() {SizeX = 10, SizeZ = 4, XmlTemplate = "<Brick refID=\"{0}\" designID=\"3030\"><Part refID=\"{0}\" designID=\"3030\" materials=\"{1}\"><Bone refID=\"{0}\" transformation=\"1,0,0,0,1,0,0,0,1,{2},0,{3}\"></Bone></Part></Brick>", OffsetX = 0.4f, OffsetZ = 2.8f },
                new BrickType() {SizeX = 4, SizeZ = 10, XmlTemplate = "<Brick refID=\"{0}\" designID=\"3030\"><Part refID=\"{0}\" designID=\"3030\" materials=\"{1}\"><Bone refID=\"{0}\" transformation=\"0,0,1,0,1,0,-1,0,0,{2},0,{3}\"></Bone></Part></Brick>", OffsetX = 0.4f, OffsetZ = 0.4f },
                new BrickType() {SizeX = 6, SizeZ = 6, XmlTemplate = "<Brick refID=\"{0}\" designID=\"3958\"><Part refID=\"{0}\" designID=\"3958\" materials=\"{1}\"><Bone refID=\"{0}\" transformation=\"0,0,1,0,1,0,-1,0,0,{2},0,{3}\"></Bone></Part></Brick>", OffsetX = 0.4f, OffsetZ = 0.4f },
                new BrickType() {SizeX = 8, SizeZ = 4, XmlTemplate = "<Brick refID=\"{0}\" designID=\"3035\"><Part refID=\"{0}\" designID=\"3035\" materials=\"{1}\"><Bone refID=\"{0}\" transformation=\"1,0,0,0,1,0,0,0,1,{2},0,{3}\"></Bone></Part></Brick>", OffsetX = 0.4f, OffsetZ = 2.8f },
                new BrickType() {SizeX = 4, SizeZ = 8, XmlTemplate = "<Brick refID=\"{0}\" designID=\"3035\"><Part refID=\"{0}\" designID=\"3035\" materials=\"{1}\"><Bone refID=\"{0}\" transformation=\"0,0,1,0,1,0,-1,0,0,{2},0,{3}\"></Bone></Part></Brick>", OffsetX = 0.4f, OffsetZ = 0.4f },
                new BrickType() {SizeX = 6, SizeZ = 4, XmlTemplate = "<Brick refID=\"{0}\" designID=\"3032\"><Part refID=\"{0}\" designID=\"3032\" materials=\"{1}\"><Bone refID=\"{0}\" transformation=\"1,0,0,0,1,0,0,0,1,{2},0,{3}\"></Bone></Part></Brick>", OffsetX = 0.4f, OffsetZ = 2.8f },
                new BrickType() {SizeX = 4, SizeZ = 6, XmlTemplate = "<Brick refID=\"{0}\" designID=\"3032\"><Part refID=\"{0}\" designID=\"3032\" materials=\"{1}\"><Bone refID=\"{0}\" transformation=\"0,0,1,0,1,0,-1,0,0,{2},0,{3}\"></Bone></Part></Brick>", OffsetX = 0.4f, OffsetZ = 0.4f },
                new BrickType() {SizeX = 12, SizeZ = 2, XmlTemplate = "<Brick refID=\"{0}\" designID=\"2445\"><Part refID=\"{0}\" designID=\"2445\" materials=\"{1}\"><Bone refID=\"{0}\" transformation=\"1,0,0,0,1,0,0,0,1,{2},0,{3}\"></Bone></Part></Brick>", OffsetX = 0.4f, OffsetZ = 1.2f },
                new BrickType() {SizeX = 2, SizeZ = 12, XmlTemplate = "<Brick refID=\"{0}\" designID=\"2445\"><Part refID=\"{0}\" designID=\"2445\" materials=\"{1}\"><Bone refID=\"{0}\" transformation=\"0,0,1,0,1,0,-1,0,0,{2},0,{3}\"></Bone></Part></Brick>", OffsetX = 0.4f, OffsetZ = 0.4f },
                new BrickType() {SizeX = 10, SizeZ = 2, XmlTemplate = "<Brick refID=\"{0}\" designID=\"3832\"><Part refID=\"{0}\" designID=\"3832\" materials=\"{1}\"><Bone refID=\"{0}\" transformation=\"1,0,0,0,1,0,0,0,1,{2},0,{3}\"></Bone></Part></Brick>", OffsetX = 0.4f, OffsetZ = 1.2f },
                new BrickType() {SizeX = 2, SizeZ = 10, XmlTemplate = "<Brick refID=\"{0}\" designID=\"3832\"><Part refID=\"{0}\" designID=\"3832\" materials=\"{1}\"><Bone refID=\"{0}\" transformation=\"0,0,1,0,1,0,-1,0,0,{2},0,{3}\"></Bone></Part></Brick>", OffsetX = 0.4f, OffsetZ = 0.4f },
                new BrickType() {SizeX = 4, SizeZ = 4, XmlTemplate = "<Brick refID=\"{0}\" designID=\"3031\"><Part refID=\"{0}\" designID=\"3031\" materials=\"{1}\"><Bone refID=\"{0}\" transformation=\"0,0,1,0,1,0,-1,0,0,{2},0,{3}\"></Bone></Part></Brick>", OffsetX = 0.4f, OffsetZ = 0.4f },
                new BrickType() {SizeX = 8, SizeZ = 2, XmlTemplate = "<Brick refID=\"{0}\" designID=\"3034\"><Part refID=\"{0}\" designID=\"3034\" materials=\"{1}\"><Bone refID=\"{0}\" transformation=\"1,0,0,0,1,0,0,0,1,{2},0,{3}\"></Bone></Part></Brick>", OffsetX = 0.4f, OffsetZ = 1.2f },
                new BrickType() {SizeX = 2, SizeZ = 8, XmlTemplate = "<Brick refID=\"{0}\" designID=\"3034\"><Part refID=\"{0}\" designID=\"3034\" materials=\"{1}\"><Bone refID=\"{0}\" transformation=\"0,0,1,0,1,0,-1,0,0,{2},0,{3}\"></Bone></Part></Brick>", OffsetX = 0.4f, OffsetZ = 0.4f },
                new BrickType() {SizeX = 6, SizeZ = 2, XmlTemplate = "<Brick refID=\"{0}\" designID=\"3795\"><Part refID=\"{0}\" designID=\"3795\" materials=\"{1}\"><Bone refID=\"{0}\" transformation=\"1,0,0,0,1,0,0,0,1,{2},0,{3}\"></Bone></Part></Brick>", OffsetX = 0.4f, OffsetZ = 1.2f },
                new BrickType() {SizeX = 2, SizeZ = 6, XmlTemplate = "<Brick refID=\"{0}\" designID=\"3795\"><Part refID=\"{0}\" designID=\"3795\" materials=\"{1}\"><Bone refID=\"{0}\" transformation=\"0,0,1,0,1,0,-1,0,0,{2},0,{3}\"></Bone></Part></Brick>", OffsetX = 0.4f, OffsetZ = 0.4f },
                new BrickType() {SizeX = 12, SizeZ = 1, XmlTemplate = "<Brick refID=\"{0}\" designID=\"60479\"><Part refID=\"{0}\" designID=\"60479\" materials=\"{1}\"><Bone refID=\"{0}\" transformation=\"-1,0,0,0,1,0,0,0,-1,{2},0,{3}\"></Bone></Part></Brick>", OffsetX = 9.2f, OffsetZ = 0.4f },
                new BrickType() {SizeX = 1, SizeZ = 12, XmlTemplate = "<Brick refID=\"{0}\" designID=\"60479\"><Part refID=\"{0}\" designID=\"60479\" materials=\"{1}\"><Bone refID=\"{0}\" transformation=\"0,0,-1,0,1,0,1,0,0,{2},0,{3}\"></Bone></Part></Brick>", OffsetX = 0.4f, OffsetZ = 9.2f },
                new BrickType() {SizeX = 10, SizeZ = 1, XmlTemplate = "<Brick refID=\"{0}\" designID=\"4477\"><Part refID=\"{0}\" designID=\"4477\" materials=\"{1}\"><Bone refID=\"{0}\" transformation=\"-1,0,0,0,1,0,0,0,-1,{2},0,{3}\"></Bone></Part></Brick>", OffsetX = 7.6f, OffsetZ = 0.4f },
                new BrickType() {SizeX = 1, SizeZ = 10, XmlTemplate = "<Brick refID=\"{0}\" designID=\"4477\"><Part refID=\"{0}\" designID=\"4477\" materials=\"{1}\"><Bone refID=\"{0}\" transformation=\"0,0,-1,0,1,0,1,0,0,{2},0,{3}\"></Bone></Part></Brick>", OffsetX = 0.4f, OffsetZ = 7.6f },
                new BrickType() {SizeX = 4, SizeZ = 2, XmlTemplate = "<Brick refID=\"{0}\" designID=\"3020\"><Part refID=\"{0}\" designID=\"3020\" materials=\"{1}\"><Bone refID=\"{0}\" transformation=\"1,0,0,0,1,0,0,0,1,{2},0,{3}\"></Bone></Part></Brick>", OffsetX = 0.4f, OffsetZ = 1.2f },
                new BrickType() {SizeX = 2, SizeZ = 4, XmlTemplate = "<Brick refID=\"{0}\" designID=\"3020\"><Part refID=\"{0}\" designID=\"3020\" materials=\"{1}\"><Bone refID=\"{0}\" transformation=\"0,0,1,0,1,0,-1,0,0,{2},0,{3}\"></Bone></Part></Brick>", OffsetX = 0.4f, OffsetZ = 0.4f },
                new BrickType() {SizeX = 8, SizeZ = 1, XmlTemplate = "<Brick refID=\"{0}\" designID=\"3460\"><Part refID=\"{0}\" designID=\"3460\" materials=\"{1}\"><Bone refID=\"{0}\" transformation=\"-1,0,0,0,1,0,0,0,-1,{2},0,{3}\"></Bone></Part></Brick>", OffsetX = 6f, OffsetZ = 0.4f },
                new BrickType() {SizeX = 1, SizeZ = 8, XmlTemplate = "<Brick refID=\"{0}\" designID=\"3460\"><Part refID=\"{0}\" designID=\"3460\" materials=\"{1}\"><Bone refID=\"{0}\" transformation=\"0,0,-1,0,1,0,1,0,0,{2},0,{3}\"></Bone></Part></Brick>", OffsetX = 0.4f, OffsetZ = 6f },
                new BrickType() {SizeX = 6, SizeZ = 1, XmlTemplate = "<Brick refID=\"{0}\" designID=\"3666\"><Part refID=\"{0}\" designID=\"3666\" materials=\"{1}\"><Bone refID=\"{0}\" transformation=\"-1,0,0,0,1,0,0,0,-1,{2},0,{3}\"></Bone></Part></Brick>", OffsetX = 4.4f, OffsetZ = 0.4f },
                new BrickType() {SizeX = 1, SizeZ = 6, XmlTemplate = "<Brick refID=\"{0}\" designID=\"3666\"><Part refID=\"{0}\" designID=\"3666\" materials=\"{1}\"><Bone refID=\"{0}\" transformation=\"0,0,-1,0,1,0,1,0,0,{2},0,{3}\"></Bone></Part></Brick>", OffsetX = 0.4f, OffsetZ = 4.4f },
                new BrickType() {SizeX = 3, SizeZ = 2, XmlTemplate = "<Brick refID=\"{0}\" designID=\"3021\"><Part refID=\"{0}\" designID=\"3021\" materials=\"{1}\"><Bone refID=\"{0}\" transformation=\"1,0,0,0,1,0,0,0,1,{2},0,{3}\"></Bone></Part></Brick>", OffsetX = 0.4f, OffsetZ = 1.2f },
                new BrickType() {SizeX = 2, SizeZ = 3, XmlTemplate = "<Brick refID=\"{0}\" designID=\"3021\"><Part refID=\"{0}\" designID=\"3021\" materials=\"{1}\"><Bone refID=\"{0}\" transformation=\"0,0,1,0,1,0,-1,0,0,{2},0,{3}\"></Bone></Part></Brick>", OffsetX = 0.4f, OffsetZ = 0.4f },
                new BrickType() {SizeX = 2, SizeZ = 2, XmlTemplate = "<Brick refID=\"{0}\" designID=\"3022\"><Part refID=\"{0}\" designID=\"3022\" materials=\"{1}\"><Bone refID=\"{0}\" transformation=\"0,0,1,0,1,0,-1,0,0,{2},0,{3}\"></Bone></Part></Brick>", OffsetX = 0.4f, OffsetZ = 0.4f },
                new BrickType() {SizeX = 4, SizeZ = 1, XmlTemplate = "<Brick refID=\"{0}\" designID=\"3710\"><Part refID=\"{0}\" designID=\"3710\" materials=\"{1}\"><Bone refID=\"{0}\" transformation=\"-1,0,0,0,1,0,0,0,-1,{2},0,{3}\"></Bone></Part></Brick>", OffsetX = 2.8f, OffsetZ = 0.4f },
                new BrickType() {SizeX = 1, SizeZ = 4, XmlTemplate = "<Brick refID=\"{0}\" designID=\"3710\"><Part refID=\"{0}\" designID=\"3710\" materials=\"{1}\"><Bone refID=\"{0}\" transformation=\"0,0,-1,0,1,0,1,0,0,{2},0,{3}\"></Bone></Part></Brick>", OffsetX = 0.4f, OffsetZ = 2.8f },
                new BrickType() {SizeX = 3, SizeZ = 1, XmlTemplate = "<Brick refID=\"{0}\" designID=\"3623\"><Part refID=\"{0}\" designID=\"3623\" materials=\"{1}\"><Bone refID=\"{0}\" transformation=\"-1,0,0,0,1,0,0,0,-1,{2},0,{3}\"></Bone></Part></Brick>", OffsetX = 2, OffsetZ = 0.4f },
                new BrickType() {SizeX = 1, SizeZ = 3, XmlTemplate = "<Brick refID=\"{0}\" designID=\"3623\"><Part refID=\"{0}\" designID=\"3623\" materials=\"{1}\"><Bone refID=\"{0}\" transformation=\"0,0,-1,0,1,0,1,0,0,{2},0,{3}\"></Bone></Part></Brick>", OffsetX = 0.4f, OffsetZ = 2 },
                new BrickType() {SizeX = 2, SizeZ = 1, XmlTemplate = "<Brick refID=\"{0}\" designID=\"3023\"><Part refID=\"{0}\" designID=\"3023\" materials=\"{1}\"><Bone refID=\"{0}\" transformation=\"-1,0,0,0,1,0,0,0,-1,{2},0,{3}\"></Bone></Part></Brick>", OffsetX = 1.2f, OffsetZ = 0.4f },
                new BrickType() {SizeX = 1, SizeZ = 2, XmlTemplate = "<Brick refID=\"{0}\" designID=\"3023\"><Part refID=\"{0}\" designID=\"3023\" materials=\"{1}\"><Bone refID=\"{0}\" transformation=\"0,0,-1,0,1,0,1,0,0,{2},0,{3}\"></Bone></Part></Brick>", OffsetX = 0.4f, OffsetZ = 1.2f },
                new BrickType() {SizeX = 1, SizeZ = 1, XmlTemplate = "<Brick refID=\"{0}\" designID=\"3024\"><Part refID=\"{0}\" designID=\"3024\" materials=\"{1}\"><Bone refID=\"{0}\" transformation=\"-1,0,0,0,1,0,0,0,-1,{2},0,{3}\"></Bone></Part></Brick>", OffsetX = 0.4f, OffsetZ = 0.4f },
            };*/
        }

        public List<Brick> ParseList(int squaresX, int squaresZ, List<SquareTypes> input)
        {
            var result = new List<Brick>();

            var map = CreateMapFromList(squaresX, squaresZ, input);

            //_brick_repo.GetBrickSizesForMaterialId()
            //_brick_repo.GetBrick()

            foreach(SquareTypes type in (SquareTypes[])Enum.GetValues(typeof(SquareTypes)))
            {
                var counter = 0;

                var material_id = MapConfig.GetMaterialId(type);

                var brick_sizes = _brick_repo.GetBrickSizesForMaterialId(material_id);

                foreach (var brick_size in brick_sizes)
                {
                    for (int z = 0; z < squaresZ; z++)
                    {
                        for (int x = 0; x < squaresX; x++)
                        {
                            if (map[x, z] == SquareTypes.Ignore)
                                continue;

                            if (DoesBrickFitOnMap(map, x, z, brick_size.SizeX, brick_size.SizeZ))
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

        private bool DoesBrickFitOnMap(SquareTypes[,] map, int start_x, int start_z,
            int size_x, int size_z)
        {
            if (map.GetLength(0) < (start_x + size_x))
                return false;

            if (map.GetLength(1) < (start_z + size_z))
                return false;

            var current_type = map[start_x, start_z];

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
