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
    public class MapConfigTests
    {
        [Test]
        public void CalculateSquareTypeWillDefaultToLand()
        {
            var square_configs = MapConfig.GetSquareConfigurations();

            square_configs.First(x => x.Type == SquareTypes.Forest).Count = 0;
            square_configs.First(x => x.Type == SquareTypes.Land).Count = 0;
            square_configs.First(x => x.Type == SquareTypes.Marsh).Count = 0;
            square_configs.First(x => x.Type == SquareTypes.Mountain).Count = 0;
            square_configs.First(x => x.Type == SquareTypes.Road).Count = 0;
            square_configs.First(x => x.Type == SquareTypes.Water).Count = 0;

            Assert.That(MapConfig.CalculateSquareType(square_configs), Is.EqualTo(SquareTypes.Land));
        }

        [Test]
        public void IfOnlyWaterCalculateSquareTypeToSea()
        {
            var square_configs = MapConfig.GetSquareConfigurations();

            square_configs.First(x => x.Type == SquareTypes.Forest).Count = 0;
            square_configs.First(x => x.Type == SquareTypes.Land).Count = 0;
            square_configs.First(x => x.Type == SquareTypes.Marsh).Count = 0;
            square_configs.First(x => x.Type == SquareTypes.Mountain).Count = 0;
            square_configs.First(x => x.Type == SquareTypes.Road).Count = 0;
            square_configs.First(x => x.Type == SquareTypes.Water).Count = 20;

            Assert.That(MapConfig.CalculateSquareType(square_configs), Is.EqualTo(SquareTypes.Sea));

            square_configs.First(x => x.Type == SquareTypes.Forest).Count = 0;
            square_configs.First(x => x.Type == SquareTypes.Land).Count = 0;
            square_configs.First(x => x.Type == SquareTypes.Marsh).Count = 0;
            square_configs.First(x => x.Type == SquareTypes.Mountain).Count = 0;
            square_configs.First(x => x.Type == SquareTypes.Road).Count = 0;
            square_configs.First(x => x.Type == SquareTypes.DarkForest).Count = 10;
            square_configs.First(x => x.Type == SquareTypes.Water).Count = 20;

            Assert.That(MapConfig.CalculateSquareType(square_configs), Is.EqualTo(SquareTypes.Water));
        }

        [Test]
        public void CalculateSquareTypeWillReturnTypeWithHighestCount()
        {
            var square_configs = MapConfig.GetSquareConfigurations();

            square_configs.First(x => x.Type == SquareTypes.Forest).Count = 5;
            square_configs.First(x => x.Type == SquareTypes.Land).Count = 11;
            square_configs.First(x => x.Type == SquareTypes.Marsh).Count = 10;
            square_configs.First(x => x.Type == SquareTypes.Mountain).Count = 100;
            square_configs.First(x => x.Type == SquareTypes.Road).Count = 0; // Must be zero
            square_configs.First(x => x.Type == SquareTypes.Water).Count = 0; // Must be zero

            Assert.That(MapConfig.CalculateSquareType(square_configs), Is.EqualTo(SquareTypes.Mountain));

            square_configs.First(x => x.Type == SquareTypes.Land).Count = 110;

            Assert.That(MapConfig.CalculateSquareType(square_configs), Is.EqualTo(SquareTypes.Land));

            square_configs.First(x => x.Type == SquareTypes.Forest).Count = 500;

            Assert.That(MapConfig.CalculateSquareType(square_configs), Is.EqualTo(SquareTypes.Forest));
        }
    }
}
