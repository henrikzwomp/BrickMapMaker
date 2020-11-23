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
    public class RiverMakerTests
    {
        [Test]
        public void CanGetCornersFromSimpleMap()
        {
            var water_config = MapConfig.GetSquareConfigurations().First(x => x.Type == SquareTypes.Water);

            var brick_repo_mock = new Mock<IBrickRepo>();
            brick_repo_mock.Setup(x => x.GetBrick(It.IsAny<DesignItem>(), It.IsAny<int>(), It.IsAny<int>(),
                It.IsAny<float>(), It.IsAny<float>()))
                .Returns<DesignItem, int, int, float, float>( (design, mat_id, ref_id, z, v) => 
                    new Brick(ref_id, design.DesignID.ToString() + ":" + mat_id.ToString()));

            var part_selector_mock = new Mock<IBrickRiverPartSelector>();
            part_selector_mock.Setup(x => x.GetTopLeftCornerPart()).Returns(new DesignItem() { DesignID = 1111 });
            part_selector_mock.Setup(x => x.GetTopRightCornerPart()).Returns(new DesignItem() { DesignID = 2222 });
            part_selector_mock.Setup(x => x.GetBottomLeftCornerPart()).Returns(new DesignItem() { DesignID = 3333 });
            part_selector_mock.Setup(x => x.GetBottomRightCornerPart()).Returns(new DesignItem() { DesignID = 4444 });

            var rm = new RiverMaker(brick_repo_mock.Object, part_selector_mock.Object);

            var map = new MapSquare[2,2];

            // 1

            map[0, 0] = new MapSquare() { Type = SquareTypes.Water, PositionX = 0, PositionZ = 0 };
            map[0, 1] = new MapSquare() { Type = SquareTypes.Water, PositionX = 0, PositionZ = 1 };
            map[1, 0] = new MapSquare() { Type = SquareTypes.Water, PositionX = 1, PositionZ = 0 };
            map[1, 1] = new MapSquare() { Type = SquareTypes.Land, PositionX = 1, PositionZ = 1 };

            var result = new List<Brick>();

            rm.CreateBrickRivers(map, 0, result);

            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result[0].ToXml(), Is.EqualTo("1111:" + water_config.MaterialId));

            // 2

            map[0, 0] = new MapSquare() { Type = SquareTypes.Water, PositionX = 0, PositionZ = 0 };
            map[0, 1] = new MapSquare() { Type = SquareTypes.Land, PositionX = 0, PositionZ = 1 };
            map[1, 0] = new MapSquare() { Type = SquareTypes.Water, PositionX = 1, PositionZ = 0 };
            map[1, 1] = new MapSquare() { Type = SquareTypes.Water, PositionX = 1, PositionZ = 1 };

            result = new List<Brick>();

            rm.CreateBrickRivers(map, 0, result);

            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result[0].ToXml(), Is.EqualTo("2222:" + water_config.MaterialId));

            // 3

            map[0, 0] = new MapSquare() { Type = SquareTypes.Water, PositionX = 0, PositionZ = 0 };
            map[0, 1] = new MapSquare() { Type = SquareTypes.Water, PositionX = 0, PositionZ = 1 };
            map[1, 0] = new MapSquare() { Type = SquareTypes.Land, PositionX = 1, PositionZ = 0 };
            map[1, 1] = new MapSquare() { Type = SquareTypes.Water, PositionX = 1, PositionZ = 1 };

            result = new List<Brick>();

            rm.CreateBrickRivers(map, 0, result);

            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result[0].ToXml(), Is.EqualTo("3333:" + water_config.MaterialId));

            // 4

            map[0, 0] = new MapSquare() { Type = SquareTypes.Land, PositionX = 0, PositionZ = 0 };
            map[0, 1] = new MapSquare() { Type = SquareTypes.Water, PositionX = 0, PositionZ = 1 };
            map[1, 0] = new MapSquare() { Type = SquareTypes.Water, PositionX = 1, PositionZ = 0 };
            map[1, 1] = new MapSquare() { Type = SquareTypes.Water, PositionX = 1, PositionZ = 1 };

            result = new List<Brick>();

            rm.CreateBrickRivers(map, 0, result);

            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result[0].ToXml(), Is.EqualTo("4444:" + water_config.MaterialId));
        }

        /*
         *  LLLL
         *  WWWW
         *  SSSS
         * **/
    }
}
