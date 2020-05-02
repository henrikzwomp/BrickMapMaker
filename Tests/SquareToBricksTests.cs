using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrickMapMaker;
using NUnit.Framework;
using Moq;

namespace Tests
{
    [TestFixture]
    public class SquareToBricksTests
    {
        [Test]
        public void CanParseList()
        {
            var input_list = new List<MapSquare>()
            {
                new MapSquare() { Type = SquareTypes.Forest, PositionX = 0, PositionZ = 0 },
                new MapSquare() { Type = SquareTypes.Forest, PositionX = 0, PositionZ = 1 },
                new MapSquare() { Type = SquareTypes.Forest, PositionX = 1, PositionZ = 0 },
                new MapSquare() { Type = SquareTypes.Forest, PositionX = 1, PositionZ = 1 },
            };

            var brick_repo = new Mock<IBrickRepo>();

            brick_repo.Setup(x => x.GetBrickSizesForMaterialId(It.IsAny<int>())).Returns(
                new List<DesignItem>() { new DesignItem() { SizeX = 2, SizeZ = 2 } });

            brick_repo.Setup(x => x.GetBrick(It.IsAny<DesignItem>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<float>(), It.IsAny<float>()))
                .Returns(new Brick(0, ""));

            var s2b = new SquaresToBrickMaps(brick_repo.Object);
            var result = s2b.ParseList(2, 2, 10, 10, input_list);

            Assert.That(result.Count, Is.EqualTo(1));
        }

        [Test]
        public void WillUseMaxUsageValueWhenCreatingBrickMap()
        {
            var input_list = new List<MapSquare>()
            {
                new MapSquare() { Type = SquareTypes.Land, PositionX = 0, PositionZ = 0 },
                new MapSquare() { Type = SquareTypes.Land, PositionX = 0, PositionZ = 1 },
                new MapSquare() { Type = SquareTypes.Land, PositionX = 1, PositionZ = 0 },
                new MapSquare() { Type = SquareTypes.Land, PositionX = 1, PositionZ = 1 },
            };

            var brick_repo = new BrickRepo(TestHelper.AssemblyDirectory + "BrickRepoTests_BrickList01.xlsx");

            var s2b = new SquaresToBrickMaps(brick_repo);
            var result = s2b.ParseList(2, 2, 10, 10, input_list);

            Assert.That(result.Count, Is.EqualTo(3));
            Assert.That(result.Count(x => x.ToXml().Contains("3023")), Is.EqualTo(1));
            Assert.That(result.Count(x => x.ToXml().Contains("3024")), Is.EqualTo(2));
        }

    }
}
