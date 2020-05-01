using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrickMapMaker;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class BrickRepoTests
    {
        [Test]
        public void CanGetSizes()
        {
            var repo = new BrickRepo(TestHelper.AssemblyDirectory + "BrickRepoTests_BrickList01.xlsx");

            var sizes = repo.GetBrickSizesForMaterialId(28);

            Assert.That(sizes.Count, Is.EqualTo(6));
            Assert.That(sizes.Count(a => a.SizeX == 1 && a.SizeZ == 1), Is.EqualTo(1));
            Assert.That(sizes.Count(a => a.SizeX == 2 && a.SizeZ == 1), Is.EqualTo(1));
            Assert.That(sizes.Count(a => a.SizeX == 1 && a.SizeZ == 2), Is.EqualTo(1));
            Assert.That(sizes.Count(a => a.SizeX == 4 && a.SizeZ == 4), Is.EqualTo(1));
            Assert.That(sizes.Count(a => a.SizeX == 4 && a.SizeZ == 10), Is.EqualTo(1));
            Assert.That(sizes.Count(a => a.SizeX == 10 && a.SizeZ == 4), Is.EqualTo(1));
        }

        [Test]
        public void CanGetSizesInRightOrder()
        {
            var repo = new BrickRepo(TestHelper.AssemblyDirectory + "BrickRepoTests_BrickList01.xlsx");

            var sizes = repo.GetBrickSizesForMaterialId(28);

            Assert.That(sizes.Count, Is.EqualTo(6));
            Assert.That(sizes[0].SizeX, Is.EqualTo(10));
            Assert.That(sizes[0].SizeZ, Is.EqualTo(4));
            Assert.That(sizes[1].SizeX, Is.EqualTo(4));
            Assert.That(sizes[1].SizeZ, Is.EqualTo(10));
            Assert.That(sizes[2].SizeX, Is.EqualTo(4));
            Assert.That(sizes[2].SizeZ, Is.EqualTo(4));
            Assert.That(sizes[3].SizeX, Is.EqualTo(2));
            Assert.That(sizes[3].SizeZ, Is.EqualTo(1));
            Assert.That(sizes[4].SizeX, Is.EqualTo(1));
            Assert.That(sizes[4].SizeZ, Is.EqualTo(2));
            Assert.That(sizes[5].SizeX, Is.EqualTo(1));
            Assert.That(sizes[5].SizeZ, Is.EqualTo(1));
        }

        [Test]
        public void CanGetBrick()
        {
            var repo = new BrickRepo(TestHelper.AssemblyDirectory + "BrickRepoTests_BrickList01.xlsx");

            var size = new DesignItem()
            {
                DesignID = 3030,
                SizeX = 10,
                SizeZ = 4,
                OffsetX = 0.4f,
                OffsetZ = 2.8f,
                Transform = "1,0,0,0,1,0,0,0,1,{0},0,{1}"
            };

            var brick = repo.GetBrick(size, 28, 399, 0, 0);

            Assert.That(brick.ToXml(), Is.EqualTo("<Brick refID=\"399\" designID=\"3030\"><Part refID=\"399\" designID=\"3030\" materials=\"28\"><Bone refID=\"399\" transformation=\"1,0,0,0,1,0,0,0,1,0.4,0,2.8\"></Bone></Part></Brick>"));
        }
    }
}
