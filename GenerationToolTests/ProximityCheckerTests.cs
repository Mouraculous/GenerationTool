using GenerationTool.Utilities;
using IGenerationTool.Utilities;
using NUnit.Framework;

namespace GenerationToolTests
{
    [TestFixture]
    public class ProximityCheckerTests
    {
        private ProximityChecker _proximityChecker;
        private TileType[][] _boardTiles;

        [SetUp]
        public void SetUp()
        {
            _proximityChecker = new ProximityChecker();
            _boardTiles = new TileType[3][];

            for (var index = 0; index < _boardTiles.Length; index++)
            {
                _boardTiles[index] = new TileType[3];
            }

            _boardTiles[1][1] = TileType.Floor;
        }

        [TestCase(0, 1)]
        [TestCase(1, 0)]
        [TestCase(1, 2)]
        [TestCase(2, 1)]
        public void WhenGivenCoordsPointToFloorProximity_CheckProximity_ShouldReturnTrue(int x, int y)
        {
            // Arrange

            // Act
            var result = _proximityChecker.CheckRoomProximity(x, y, _boardTiles);

            // Assert
            Assert.That(result, Is.True);

        }

        [Test]
        public void WhenGivenCoordsDoNotPointToFloorProximity_CheckProximity_ShouldReturnFalse()
        {
            // Arrange
            _boardTiles[1][1] = TileType.Wall;

            // Act
            var result = _proximityChecker.CheckRoomProximity(1, 1, _boardTiles);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void WhenGivenCoordsPointToFloor_CheckProximity_ShouldReturnFalse()
        {
            // Arrange

            // Act

            // Assert

        }
    }
}
