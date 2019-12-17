using IGenerationTool.Generation;
using IGenerationTool.Models;
using IGenerationTool.Utilities;
using UnityEngine;

namespace GenerationTool.Generation
{
    public class BoardCreator : IBoardGenerator {

        public int Columns = 100;
        public int Rows = 100;
        public IntRange RoomsNumber = new IntRange(15, 20);
        public IntRange RoomWidth = new IntRange(3, 10);
        public IntRange RoomLength = new IntRange(3, 10);
        public IntRange CorridorLength = new IntRange(6, 10);
        public GameObject[] FloorPrefabs;
        public GameObject[] WallPrefabs;
        public GameObject[] EnemyPrefabs;
        public IntRange EnemiesNumber = new IntRange(15, 30);
        public int Seed = 0;

        private readonly IProximityChecker _proximityChecker;
        private readonly ITileInstantiator _tileInstantiator;

        private TileType[][] _tiles;
        private Room[] _rooms;
        private Corridor[] _corridors;
        private GameObject _boardHolder;
        private readonly IRoomBuilder _roomBuilder;
        private readonly ICorridorBuilder _corridorBuilder;
        private readonly ITileLayoutCreator _tileLayoutCreator;
        private readonly IObjectInstantiator _enemyInstantiator;

        public BoardCreator(IProximityChecker proximityChecker, ITileInstantiator tileInstantiator, IRoomBuilder roomBuilder, ICorridorBuilder corridorBuilder, ITileLayoutCreator tileLayoutCreator, IObjectInstantiator enemyInstantiator)
        {
            _proximityChecker = proximityChecker;
            _tileInstantiator = tileInstantiator;
            _roomBuilder = roomBuilder;
            _corridorBuilder = corridorBuilder;
            _tileLayoutCreator = tileLayoutCreator;
            _enemyInstantiator = enemyInstantiator;
        }

        public void SetupPrefabs(GameObject[] floorTiles, GameObject[] wallTiles, GameObject[] enemyPrefabs)
        {
            FloorPrefabs = floorTiles;
            WallPrefabs = wallTiles;
            EnemyPrefabs = enemyPrefabs;
        }

        public void SetupSize(int columns, int rows, int seed, IntRange numRooms, IntRange roomWidth, IntRange roomHeight,
            IntRange corridorLength, IntRange enemiesNumber)
        {
            Columns = columns;
            Rows = rows;
            Seed = seed;
            RoomsNumber = numRooms;
            RoomWidth = roomWidth;
            RoomLength = roomHeight;
            CorridorLength = corridorLength;
            EnemiesNumber = enemiesNumber;
        }
        
        private void Generate() {
            _boardHolder = new GameObject("BoardHolder");

            SetupTilesArray();

            CreateRoomsAndCorridors();

            _tileLayoutCreator.SetupRoomTiles(_rooms, ref _tiles);
            _tileLayoutCreator.SetupCorridorTiles(_corridors, ref _tiles);

            InstantiateTiles(_tiles);
            _enemyInstantiator.InstantiateObject(EnemyPrefabs, _boardHolder);
        }
        public void Regenerate()
        {
            var unfinished = GameObject.FindGameObjectsWithTag("Unfinished");
            var begin = unfinished[0];
            var end = unfinished[1];

            var xSize = Mathf.RoundToInt(Mathf.Abs(begin.transform.position.x - end.transform.position.x)) + 1;
            var ySize = Mathf.RoundToInt(Mathf.Abs(begin.transform.position.y - end.transform.position.y)) + 1;
            Debug.Log("x = " + xSize + "; y = " + ySize);

            CreateShapingArray(xSize, ySize, begin, end);
        }

        private void InstantiateWithOffset(int xOffset, int yOffset)
        {
            for (int i = 0; i < _tiles.Length; i++)
            {
                for (int j = 0; j < _tiles[i].Length; j++)
                {
                    if (_tiles[i][j] == TileType.Floor)
                    {
                        _tileInstantiator.InstantiateFromArray(FloorPrefabs, i + xOffset, j + yOffset, _boardHolder.transform);
                    }
                    else if (_proximityChecker.CheckRoomProximity(i, j, _tiles))
                    {
                        _tileInstantiator.InstantiateFromArray(WallPrefabs, i + xOffset, j + yOffset, _boardHolder.transform);
                    }
                }
            }
        }

        private void CreateShapingArray(int xSize, int ySize, GameObject begin, GameObject end)
        {
            var beginX = (int)begin.transform.position.x;
            var beginY = (int)begin.transform.position.y;
            var endX = (int)end.transform.position.x;
            var endY = (int)end.transform.position.y;

            var tiles = new TileType[xSize + 2][];
            for (var i = 0; i < tiles.Length; i++)
            {
                tiles[i] = new TileType[ySize + 2];
            }

            if (beginX > endX)
            {
                Debug.Log("beg x > end x");
                if (beginY > endY)
                {
                    Debug.Log("1");
                    _tiles = InitShapingArray(tiles, xSize - 1, 1);
                    InstantiateWithOffset(endX, endY - 1);
                }
                else if (beginY < endY)
                {
                    Debug.Log("2");
                    _tiles = InitShapingArray(tiles, 1, 1);
                    InstantiateWithOffset(endX - 1, beginY - 1);
                }
                else
                {
                    Debug.Log("3");
                    _tiles = InitShapingArray(tiles, -1, 1);
                    InstantiateWithOffset(endX, endY - 1);
                }
            }
            else if (beginX < endX)
            {
                Debug.Log("beg x < end x");
                if (beginY > endY)
                {
                    Debug.Log("1");
                    _tiles = InitShapingArray(tiles, xSize - 1, ySize - 1);
                    InstantiateWithOffset(beginX, endY);
                }
                else if (beginY < endY)
                {
                    Debug.Log("2");
                    _tiles = InitShapingArray(tiles, 1, ySize - 1);
                    InstantiateWithOffset(beginX - 1, beginY);
                }
                else
                {
                    Debug.Log("3");
                    _tiles = InitShapingArray(tiles, -1, 1);
                    InstantiateWithOffset(beginX - 1, endY - 1);
                }
            }
            else
            {
                Debug.Log("beg x = end x");
                if (beginY != endY)
                {
                    _tiles = InitShapingArray(tiles, 1, -1);
                    InstantiateWithOffset(beginX - 1, endY - 1);
                }
            }
        }

        private TileType[][] InitShapingArray(TileType[][] tiles, int x, int y)
        {
            for (int i = 0; i < tiles.Length; i++)
            {
                for (int j = 0; j < tiles[i].Length; j++)
                {
                    if (i == x || j == y)
                    {
                        tiles[i][j] = TileType.Floor;
                    }
                }
            }
            return tiles;
        }

        private void SetupTilesArray()
        {
            _tiles = new TileType[Columns][];

            for (var i = 0; i < _tiles.Length; i++)
            {
                _tiles[i] = new TileType[Rows];
            }
        }

        private void CreateRoomsAndCorridors()
        {
            _rooms = new Room[RoomsNumber.Random];

            _corridors = new Corridor[_rooms.Length - 1];

            _rooms[0] = new Room();
            _corridors[0] = new Corridor();

            _roomBuilder.BuildRoom(_rooms[0], RoomWidth, RoomLength, Columns, Rows);

            _corridorBuilder.BuildCorridor(_corridors[0], _rooms[0], CorridorLength, RoomWidth, RoomLength, Columns, Rows, true);

            for (var i = 1; i < _rooms.Length; i++)
            {
                _rooms[i] = new Room();

                _roomBuilder.BuildRoom(_rooms[i], RoomWidth, RoomLength, Columns, Rows, _corridors[i - 1]);

                if (i < _corridors.Length)
                {
                    _corridors[i] = new Corridor();

                    _corridorBuilder.BuildCorridor(_corridors[i], _rooms[i], CorridorLength, RoomWidth, RoomLength, Columns, Rows);
                }
            }
        }

        public void InstantiateTiles(TileType[][] tiles)
        {
            for (var i = 0; i < _tiles.Length; i++)
            {
                for (var j = 0; j < _tiles[i].Length; j++)
                {
                    if (_tiles[i][j] == TileType.Floor)
                    {
                        _tileInstantiator.InstantiateFromArray(FloorPrefabs, i, j, _boardHolder.transform);
                    }
                    else if (_proximityChecker.CheckRoomProximity(i, j, _tiles))
                    {
                        _tileInstantiator.InstantiateFromArray(WallPrefabs, i, j, _boardHolder.transform);
                    }
                }
            }
        }
    }
}
