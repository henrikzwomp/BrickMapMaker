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
    }
}
