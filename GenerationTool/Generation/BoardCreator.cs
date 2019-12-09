using IGenerationTool.Generation;
using IGenerationTool.Models;
using IGenerationTool.Utilities;
using UnityEngine;

namespace GenerationTool.Generation
{
    public class BoardCreator : IBoardGenerator {

        public int Columns = 100;
        public int Rows = 100;
        public IntRange NumRooms = new IntRange(15, 20);
        public IntRange RoomWidth = new IntRange(3, 10);
        public IntRange RoomHeight = new IntRange(3, 10);
        public IntRange CorridorLength = new IntRange(6, 10);
        public GameObject[] FloorTiles;
        public GameObject[] WallTiles;

        private readonly IProximityChecker _proximityChecker;
        private readonly ITileInstantiator _tileInstantiator;

        private TileType[][] _tiles;
        private Room[] _rooms;
        private Corridor[] _corridors;
        private GameObject _boardHolder;
        private readonly IRoomBuilder _roomBuilder;
        private readonly ICorridorBuilder _corridorBuilder;
        private readonly ITileLayoutCreator _tileLayoutCreator;

        public BoardCreator(IProximityChecker proximityChecker, ITileInstantiator tileInstantiator, IRoomBuilder roomBuilder, ICorridorBuilder corridorBuilder, ITileLayoutCreator tileLayoutCreator)
        {
            _proximityChecker = proximityChecker;
            _tileInstantiator = tileInstantiator;
            _roomBuilder = roomBuilder;
            _corridorBuilder = corridorBuilder;
            _tileLayoutCreator = tileLayoutCreator;
        }

        public void SetupPrefabs(GameObject[] floorTiles, GameObject[] wallTiles)
        {
            FloorTiles = floorTiles;
            WallTiles = wallTiles;
        }

        public void SetupSize(int columns, int rows, IntRange numRooms, IntRange roomWidth, IntRange roomHeight,
            IntRange corridorLength)
        {
            Columns = columns;
            Rows = rows;
            NumRooms = numRooms;
            RoomWidth = roomWidth;
            RoomHeight = roomHeight;
            CorridorLength = corridorLength;
        }
        
        private void Generate() {
            _boardHolder = new GameObject("BoardHolder");

            SetupTilesArray();

            CreateRoomsAndCorridors();

            _tileLayoutCreator.SetupRoomTiles(_rooms, ref _tiles);
            _tileLayoutCreator.SetupCorridorTiles(_corridors, ref _tiles);

            InstantiateTiles(_tiles);
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
            _rooms = new Room[NumRooms.Random];

            _corridors = new Corridor[_rooms.Length - 1];

            _rooms[0] = new Room();
            _corridors[0] = new Corridor();

            _roomBuilder.BuildRoom(_rooms[0], RoomWidth, RoomHeight, Columns, Rows);

            _corridorBuilder.BuildCorridor(_corridors[0], _rooms[0], CorridorLength, RoomWidth, RoomHeight, Columns, Rows, true);

            for (var i = 1; i < _rooms.Length; i++)
            {
                _rooms[i] = new Room();

                _roomBuilder.BuildRoom(_rooms[i], RoomWidth, RoomHeight, Columns, Rows, _corridors[i - 1]);

                if (i < _corridors.Length)
                {
                    _corridors[i] = new Corridor();

                    _corridorBuilder.BuildCorridor(_corridors[i], _rooms[i], CorridorLength, RoomWidth, RoomHeight, Columns, Rows);
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
                        _tileInstantiator.InstantiateFromArray(FloorTiles, i, j, _boardHolder.transform);
                    }
                    else if (_proximityChecker.CheckRoomProximity(i, j, _tiles))
                    {
                        _tileInstantiator.InstantiateFromArray(WallTiles, i, j, _boardHolder.transform);
                    }
                }
            }
        }
    }
}
