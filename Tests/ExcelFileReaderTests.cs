using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using BrickMapMaker;
using System.Reflection;
using System.IO;

namespace Tests
{
    [TestFixture]
    public class ExcelFileReaderTests
    {
        [Test]
        public void CanCountColumnsOnFirstRow()
        {
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            var reader = new ExcelFileReader(path + "\\BrickRepoTests_BrickList01.xlsx");
            reader.SetActiveSheet("ElementData");
            reader.ReadRow();

            Assert.That(reader.GetColumnCount(), Is.EqualTo(5));
        }

        [Test]
        public void CanReadColumnsInFirstRow()
        {
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            var reader = new ExcelFileReader(path + "\\BrickRepoTests_BrickList01.xlsx");
            reader.SetActiveSheet("ElementData");
            reader.ReadRow();

            Assert.That(reader.GetColumnCount(), Is.EqualTo(5));

            string result = "";
            Assert.That(reader.ReadColumnAsText(0, out result), Is.True);
            Assert.That(result, Is.EqualTo("MaterialID"));
            Assert.That(reader.ReadColumnAsText(1, out result), Is.True);
            Assert.That(result, Is.EqualTo("DesignID"));
            Assert.That(reader.ReadColumnAsText(2, out result), Is.True);
            Assert.That(result, Is.EqualTo("MaxUsage"));
            Assert.That(reader.ReadColumnAsText(3, out result), Is.True);
            Assert.That(result, Is.EqualTo("ElementID"));
            Assert.That(reader.ReadColumnAsText(4, out result), Is.True);
            Assert.That(result, Is.EqualTo("ColorName"));
        }

        [Test]
        public void CanReadSeveralRows()
        {
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            var reader = new ExcelFileReader(path + "\\BrickRepoTests_BrickList01.xlsx");
            reader.SetActiveSheet("ElementData");
            reader.ReadRow();

            Assert.That(reader.GetColumnCount(), Is.EqualTo(5));

            string result = "";
            Assert.That(reader.ReadColumnAsText(0, out result), Is.True);
            Assert.That(result, Is.EqualTo("MaterialID"));

            reader.ReadRow();
            Assert.That(reader.ReadColumnAsText(0, out result), Is.True);
            Assert.That(result, Is.EqualTo("28"));

            reader.ReadRow();
            Assert.That(reader.ReadColumnAsText(0, out result), Is.True);
            Assert.That(result, Is.EqualTo("37"));
        }

        [Test]
        public void WillReturnFalseIfColumnDoesntExist()
        {
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            var reader = new ExcelFileReader(path + "\\BrickRepoTests_BrickList01.xlsx");
            reader.SetActiveSheet("ElementData");
            reader.ReadRow();

            Assert.That(reader.GetColumnCount(), Is.EqualTo(5));

            string result = "";
            Assert.That(reader.ReadColumnAsText(8, out result), Is.False);
            Assert.That(result, Is.EqualTo(""));
        }

        [Test]
        public void CanReadInt()
        {
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            var reader = new ExcelFileReader(path + "\\BrickRepoTests_BrickList01.xlsx");
            reader.SetActiveSheet("ElementData");
            reader.ReadRow();
            reader.ReadRow();

            int result = 0;
            Assert.That(reader.ReadColumnAsInt(0, out result), Is.True);
            Assert.That(result, Is.EqualTo(28));
            Assert.That(reader.ReadColumnAsInt(1, out result), Is.True);
            Assert.That(result, Is.EqualTo(3024));
            Assert.That(reader.ReadColumnAsInt(3, out result), Is.True);
            Assert.That(result, Is.EqualTo(302428));
        }

        [Test]
        public void CanReadDecimal()
        {
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            var reader = new ExcelFileReader(path + "\\BrickRepoTests_BrickList01.xlsx");
            reader.SetActiveSheet("DesignData");
            reader.ReadRow();
            reader.ReadRow();

            decimal result = 0;
            Assert.That(reader.ReadColumnAsDecimal(5, out result), Is.True);
            Assert.That(result, Is.EqualTo(0.4d));

            reader.ReadRow();
            Assert.That(reader.ReadColumnAsDecimal(5, out result), Is.True);
            Assert.That(result, Is.EqualTo(1.2d));
            Assert.That(reader.ReadColumnAsDecimal(6, out result), Is.True);
            Assert.That(result, Is.EqualTo(0.4d));
        }
    }
}
