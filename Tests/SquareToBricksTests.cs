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
            var input_list = new List<SquareTypes>()
            {
                SquareTypes.Forest, SquareTypes.Forest,
                SquareTypes.Forest, SquareTypes.Forest,
            };

            var brick_repo = new Mock<IBrickRepo>();

            brick_repo.Setup(x => x.GetBrickSizesForMaterialId(It.IsAny<int>())).Returns(
                new List<DesignItem>() { new DesignItem() { SizeX = 2, SizeZ = 2 } });

            brick_repo.Setup(x => x.GetBrick(It.IsAny<DesignItem>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<float>(), It.IsAny<float>()))
                .Returns(new Brick(""));

            var s2b = new SquaresToBrickMaps(brick_repo.Object);
            var result = s2b.ParseList(2, 2, input_list);

            Assert.That(result.Count, Is.EqualTo(1));
        }

    }
}
