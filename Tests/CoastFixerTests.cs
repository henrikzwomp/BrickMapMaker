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
    public class CoastFixerTests
    {
        [Test]
        public void CanHandleLandAboveLeft()
        {
            var map = GetAllSeaMap();
            map[0, 0].Type = SquareTypes.Land;

            CoastFixer.Go(map);

            Assert.That(map[0, 0].Type, Is.EqualTo(SquareTypes.Land));
            Assert.That(map[0, 1].Type, Is.EqualTo(SquareTypes.Water)); // Below
            Assert.That(map[0, 2].Type, Is.EqualTo(SquareTypes.Sea));
            Assert.That(map[1, 0].Type, Is.EqualTo(SquareTypes.Water)); // Left
            Assert.That(map[1, 1].Type, Is.EqualTo(SquareTypes.Water)); // Below left
            Assert.That(map[1, 2].Type, Is.EqualTo(SquareTypes.Sea));
            Assert.That(map[2, 0].Type, Is.EqualTo(SquareTypes.Sea));
            Assert.That(map[2, 1].Type, Is.EqualTo(SquareTypes.Sea));
            Assert.That(map[2, 2].Type, Is.EqualTo(SquareTypes.Sea));
        }

        [Test]
        public void CanHandleLandAbove()
        {
            var map = GetAllSeaMap();
            map[1, 0].Type = SquareTypes.Land;

            CoastFixer.Go(map);

            Assert.That(map[0, 0].Type, Is.EqualTo(SquareTypes.Water)); // Left  
            Assert.That(map[0, 1].Type, Is.EqualTo(SquareTypes.Water)); // Below left
            Assert.That(map[0, 2].Type, Is.EqualTo(SquareTypes.Sea));
            Assert.That(map[1, 0].Type, Is.EqualTo(SquareTypes.Land));
            Assert.That(map[1, 1].Type, Is.EqualTo(SquareTypes.Water)); // Below
            Assert.That(map[1, 2].Type, Is.EqualTo(SquareTypes.Sea));
            Assert.That(map[2, 0].Type, Is.EqualTo(SquareTypes.Water)); // Right
            Assert.That(map[2, 1].Type, Is.EqualTo(SquareTypes.Water)); // Below right
            Assert.That(map[2, 2].Type, Is.EqualTo(SquareTypes.Sea));
        }

        [Test]
        public void CanHandleLandCenter()
        {
            var map = GetAllSeaMap();
            map[1, 1].Type = SquareTypes.Land;

            CoastFixer.Go(map);

            Assert.That(map[0, 0].Type, Is.EqualTo(SquareTypes.Water));
            Assert.That(map[0, 1].Type, Is.EqualTo(SquareTypes.Water));
            Assert.That(map[0, 2].Type, Is.EqualTo(SquareTypes.Water));
            Assert.That(map[1, 0].Type, Is.EqualTo(SquareTypes.Water));
            Assert.That(map[1, 1].Type, Is.EqualTo(SquareTypes.Land));
            Assert.That(map[1, 2].Type, Is.EqualTo(SquareTypes.Water));
            Assert.That(map[2, 0].Type, Is.EqualTo(SquareTypes.Water));
            Assert.That(map[2, 1].Type, Is.EqualTo(SquareTypes.Water));
            Assert.That(map[2, 2].Type, Is.EqualTo(SquareTypes.Water));
        }

        [Test]
        public void CanHandleLandBelow()
        {
            var map = GetAllSeaMap();
            map[1, 2].Type = SquareTypes.Land;

            CoastFixer.Go(map);

            Assert.That(map[0, 0].Type, Is.EqualTo(SquareTypes.Sea));
            Assert.That(map[0, 1].Type, Is.EqualTo(SquareTypes.Water));
            Assert.That(map[0, 2].Type, Is.EqualTo(SquareTypes.Water));
            Assert.That(map[1, 0].Type, Is.EqualTo(SquareTypes.Sea));
            Assert.That(map[1, 1].Type, Is.EqualTo(SquareTypes.Water));
            Assert.That(map[1, 2].Type, Is.EqualTo(SquareTypes.Land));
            Assert.That(map[2, 0].Type, Is.EqualTo(SquareTypes.Sea));
            Assert.That(map[2, 1].Type, Is.EqualTo(SquareTypes.Water));
            Assert.That(map[2, 2].Type, Is.EqualTo(SquareTypes.Water));
        }

        [Test]
        public void WillSetSurroundedWaterToSe()
        {
            var map = GetAllSeaMap();
            map[0, 2].Type = SquareTypes.Land;
            map[1, 2].Type = SquareTypes.Land;
            map[2, 2].Type = SquareTypes.Land;
            map[0, 1].Type = SquareTypes.Water;
            map[1, 1].Type = SquareTypes.Water;
            map[2, 1].Type = SquareTypes.Water;
            map[1, 0].Type = SquareTypes.Water;

            CoastFixer.Go(map);

            Assert.That(map[0, 0].Type, Is.EqualTo(SquareTypes.Sea));
            Assert.That(map[0, 1].Type, Is.EqualTo(SquareTypes.Water));
            Assert.That(map[0, 2].Type, Is.EqualTo(SquareTypes.Land));
            Assert.That(map[1, 0].Type, Is.EqualTo(SquareTypes.Sea));
            Assert.That(map[1, 1].Type, Is.EqualTo(SquareTypes.Water));
            Assert.That(map[1, 2].Type, Is.EqualTo(SquareTypes.Land));
            Assert.That(map[2, 0].Type, Is.EqualTo(SquareTypes.Sea));
            Assert.That(map[2, 1].Type, Is.EqualTo(SquareTypes.Water));
            Assert.That(map[2, 2].Type, Is.EqualTo(SquareTypes.Land));
        }

        private MapSquare[,] GetAllSeaMap()
        {
            var map = new MapSquare[3, 3];
            map[0, 0] = new MapSquare() { Type = SquareTypes.Sea, PositionX = 0, PositionZ = 0 };
            map[0, 1] = new MapSquare() { Type = SquareTypes.Sea, PositionX = 0, PositionZ = 1 };
            map[0, 2] = new MapSquare() { Type = SquareTypes.Sea, PositionX = 0, PositionZ = 1 };
            map[1, 0] = new MapSquare() { Type = SquareTypes.Sea, PositionX = 1, PositionZ = 0 };
            map[1, 1] = new MapSquare() { Type = SquareTypes.Sea, PositionX = 1, PositionZ = 1 };
            map[1, 2] = new MapSquare() { Type = SquareTypes.Sea, PositionX = 1, PositionZ = 2 };
            map[2, 0] = new MapSquare() { Type = SquareTypes.Sea, PositionX = 2, PositionZ = 0 };
            map[2, 1] = new MapSquare() { Type = SquareTypes.Sea, PositionX = 2, PositionZ = 1 };
            map[2, 2] = new MapSquare() { Type = SquareTypes.Sea, PositionX = 2, PositionZ = 2 };
            return map;
        }
    }
}
