using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

namespace BrickMapMaker
{
    public interface IBrickRepo
    {
        IList<DesignItem> GetBrickSizesForMaterialId(int  material_id);
        Brick GetBrick(DesignItem designitem, int material_id, int ref_id, float x_pos, float z_pos);
        bool IfBrickMaxUsageHasBeenReached(int material_id, int designID);
    }

    public class Brick
    {
        private string _xml;

        public Brick(int ref_id, string xml)
        {
            _xml = xml;
            RefId = ref_id;
        }

        public string ToXml()
        {
            return _xml;
        }

        public int GroupId { get; set; }
        public int RefId { get; }
    }

    public class BrickRepo : IBrickRepo
    {
        
        IList<DesignItem> _designs;
        IList<ElementItem> _elements;

        public BrickRepo(string source_file_path)
        {
            var reader = new ExcelFileReader(source_file_path);

            _designs = GetDesignItems(reader);
            _elements = GetElements(reader, _designs);

            reader.Close();
        }

        private IList<ElementItem> GetElements(IExcelFileReader reader, IList<DesignItem> designs)
        {
            var result = new List<ElementItem>();

            reader.SetActiveSheet("ElementData");

            while (reader.ReadRow())
            {
                if (reader.GetColumnCount() < 3)
                    continue;

                int material_id = 0;
                int design_id = 0;
                int max_usage = 0;

                if (!reader.ReadColumnAsInt(0, out material_id))
                    continue;

                if (!reader.ReadColumnAsInt(1, out design_id))
                    continue;

                if (!reader.ReadColumnAsInt(2, out max_usage))
                    continue;

                result.Add(new ElementItem()
                {
                    MaterialID = material_id,
                    MaxUsage = max_usage,
                    DesignAndOrientations = designs.Where(x => x.DesignID == design_id)
                });

            }

            return result;
        }

        private List<DesignItem> GetDesignItems(IExcelFileReader reader)
        {
            reader.SetActiveSheet("DesignData");

            var designs = new List<DesignItem>();

            while (reader.ReadRow())
            {
                if (reader.GetColumnCount() < 7)
                    continue;

                int design_id = 0;
                string blname = "";
                int size_x = 0;
                int size_z = 0;
                string transform = "";
                decimal offset_x = 0;
                decimal offset_z = 0;

                if (!reader.ReadColumnAsInt(0, out design_id))
                    continue;

                if (!reader.ReadColumnAsText(1, out blname))
                    continue;

                if (!reader.ReadColumnAsInt(2, out size_x))
                    continue;

                if (!reader.ReadColumnAsInt(3, out size_z))
                    continue;

                if (!reader.ReadColumnAsText(4, out transform))
                    continue;

                if (!reader.ReadColumnAsDecimal(5, out offset_x))
                    continue;

                if (!reader.ReadColumnAsDecimal(6, out offset_z))
                    continue;

                designs.Add(new DesignItem()
                {
                    DesignID = design_id,
                    BricklinkName = blname,
                    SizeX = size_x,
                    SizeZ = size_z,
                    Transform = transform,
                    OffsetX = (float) offset_x,
                    OffsetZ = (float) offset_z,
                });

                if (reader.GetColumnCount() >= 10)
                {
                    if (!reader.ReadColumnAsText(7, out transform))
                        continue;

                    if (!reader.ReadColumnAsDecimal(8, out offset_x))
                        continue;

                    if (!reader.ReadColumnAsDecimal(9, out offset_z))
                        continue;

                    designs.Add(new DesignItem()
                    {
                        DesignID = design_id,
                        BricklinkName = blname,
                        SizeX = size_z,
                        SizeZ = size_x, // Flipped 90 degree
                        Transform = transform,
                        OffsetX = (float) offset_x,
                        OffsetZ = (float) offset_z,
                    });
                }
            }

            return designs;
        }

        public IList<DesignItem> GetBrickSizesForMaterialId(int material_id)
        {
            var result = new List<DesignItem>();

            foreach(var element in _elements.Where(x => x.MaterialID == material_id))
            {
                result.AddRange(element.DesignAndOrientations);
            }

            return result.OrderByDescending(x => x.SizeZ * x.SizeX).ThenByDescending(x => x.SizeX).ToList();
        }

        public Brick GetBrick(DesignItem designitem, int material_id, int ref_id, float x_pos, float z_pos)
        {
            foreach(var element in _elements.Where(x => x.DesignAndOrientations.Any(y => y.DesignID == designitem.DesignID)))
            {
                element.MaxUsage--;
            }

            var transform = string.Format(designitem.Transform,
                ((x_pos * 0.8f) + designitem.OffsetX).ToString().Replace(",", "."),
                ((z_pos * 0.8f) + designitem.OffsetZ).ToString().Replace(",", "."));

            var xml = string.Format("<Brick refID=\"{0}\" designID=\"{3}\"><Part refID=\"{0}\" designID=\"{3}\" materials=\"{1}\"><Bone refID=\"{0}\" transformation=\"{2}\"></Bone></Part></Brick>"
                ,  ref_id, material_id, transform, designitem.DesignID
            );

            return new Brick(ref_id, xml);
        }

        public bool IfBrickMaxUsageHasBeenReached(int material_id, int designID)
        {
            var element = _elements.FirstOrDefault(x => x.MaterialID == material_id && 
                x.DesignAndOrientations.Any(y => y.DesignID == designID));

            if (element == null)
                return true;
            
            return (element.MaxUsage <= 0);
        }
    }

    public class DesignItem
    {
        public int DesignID { get; set; }
        public string BricklinkName { get; set; }
        public int SizeX { get; set; }
        public int SizeZ { get; set; }
        public string Transform { get; set; }
        public float OffsetX { get; set; }
        public float OffsetZ { get; set; }
    }

    public class ElementItem
    {
        public int MaterialID { get; set; }
        public int MaxUsage { get; set; }
        public IEnumerable<DesignItem> DesignAndOrientations { get; set; }
    }
}

