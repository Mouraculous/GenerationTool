using GenerationTool.Generation;
using GenerationTool.Utilities;
using IGenerationTool.Utilities;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

namespace GenerationToolTests
{
    [TestFixture]
    public class BoardCreatorTests
    {
        private BoardCreator _boardCreator;
        private Mock<ProximityChecker> _proximityChecker;
        private Mock<TileInstantiator> _tileInstantiator;
        private Mock<RoomBuilder> _roomBuilder;
        private Mock<CorridorBuilder> _corridorBuilder;
        private Mock<TileLayoutCreator> _tileLayoutCreator;
        private Mock<EnemyInstantiator> _enemyInstantiator;
        private TileType[][] _boardTiles;

        [SetUp]
        public void SetUp()
        {
            _proximityChecker = new Mock<ProximityChecker>();
            _tileInstantiator = new Mock<TileInstantiator>();
            _roomBuilder = new Mock<RoomBuilder>();
            _corridorBuilder = new Mock<CorridorBuilder>();
            _tileLayoutCreator = new Mock<TileLayoutCreator>();
            _enemyInstantiator = new Mock<EnemyInstantiator>();
            _boardTiles = new TileType[3][];

            for (var index = 0; index < _boardTiles.Length; index++)
            {
                _boardTiles[index] = new TileType[3];
            }

            _boardTiles[1][1] = TileType.Floor;

            _boardCreator = new BoardCreator(_proximityChecker.Object, _tileInstantiator.Object, _roomBuilder.Object, _corridorBuilder.Object, _tileLayoutCreator.Object, _enemyInstantiator.Object);
        }


        [Test]
        public void IfInstantiatingAGameBoard_InstantiateFromArray_ShouldCallTileInstantiator()
        {
            //Arrange
            _proximityChecker
                .Setup(x => x.CheckRoomProximity(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<TileType[][]>()))
                .Returns(true);

            //Act
            _boardCreator.InstantiateTiles(_boardTiles);

            //Assert
            _tileInstantiator.Verify(
                x => x.InstantiateFromArray(It.IsAny<List<GameObject>>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<Transform>()),
                Times.AtLeastOnce);
        }
    }
}
