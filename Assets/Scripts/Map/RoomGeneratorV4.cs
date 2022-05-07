using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class RoomGeneratorV4 : MonoBehaviour
{
    public static RoomGeneratorV4 Instance = null;

    [Header("Spawn")] [SerializeField] private GameObject _player = null;

    [SerializeField] private List<EnemyBase> _enemy = new List<EnemyBase>();
    [SerializeField] private int _enemyInOneRoom = 4;

    [Header("Random settings")] [SerializeField]
    private int _seed = 0;

    [SerializeField] private bool _randomize = false;

    [Header("Map settings")] [SerializeField]
    private Tilemap _groundMap = null;

    [SerializeField] private Tilemap _bgMap = null;

    [SerializeField] private RuleTile _groundTile = null;
    [SerializeField] private RuleTile _bgTile = null;
    [SerializeField] private TileBase _border = null;

    [Header("Room settings")] [SerializeField]
    private int _roomCount = 5;

    [SerializeField] private int _minRoomSize = 10;
    [SerializeField] private int _maxRoomSize = 20;
    [SerializeField] private int _offset = 5;
    [SerializeField] private int _widthTunnel = 2;

    [Header("Props settings")] [SerializeField]
    private List<InteractiveProps> _props = new List<InteractiveProps>();

    /*#region Testing

    [SerializeField] private Tile _centerTest = null;

    #endregion*/

    private int _chunkCount = 3;

    private List<Vector2Int> _chunks = new List<Vector2Int>(); // Chunk center == room center
    private List<Vector2Int> _rooms = new List<Vector2Int>(); // Active chunks for room generation
    private List<Vector2Int> _roomsInfo = new List<Vector2Int>(); // latter i replace this List to Dictionary =)

    private List<EnemyBase> _enemies = new List<EnemyBase>();

    public List<EnemyBase> EnemiesAll
    {
        get => _enemies;
    }

    public System.Action<List<EnemyBase>> Enemies;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance.gameObject);
            Instance = null;
        }

        Instance = this;

        Initialize();
    }

    private void Initialize()
    {
        if (_randomize)
        {
            _seed = Random.Range(0, int.MaxValue);
        }

        Random.seed = _seed;

        ChunkFormation();
    }

    private void ChunkFormation()
    {
        if (Mathf.Pow(_chunkCount, 2) < _roomCount)
        {
            while (Mathf.Pow(_chunkCount, 2) < _roomCount)
            {
                _chunkCount++;
            }
        }

        MapGeneration();
    }

    private void MapGeneration()
    {
        Vector2Int mapSize = RecalculateMapSize();
        Vector2Int centerChunk = new Vector2Int(mapSize.x / _chunkCount / 2, mapSize.y / _chunkCount / 2);
        int startX = centerChunk.x;

        for (int y = 0; y < mapSize.y; y++)
        {
            for (int x = 0; x < mapSize.x; x++)
            {
                if (y == 0 || y == mapSize.y - 1 || x == 0 || x == mapSize.x - 1)
                {
                    _groundMap.SetTile(new Vector3Int(x, y, 1), _border);
                }

                _groundMap.SetTile(new Vector3Int(x, y, 0), _groundTile);

                if (x == centerChunk.x && y == centerChunk.y)
                {
                    _chunks.Add(centerChunk);

                    centerChunk += new Vector2Int(_maxRoomSize + _offset * 2, 0);
                }
            }

            if (y == centerChunk.y)
            {
                centerChunk = new Vector2Int(startX, centerChunk.y + (_maxRoomSize + _offset * 2));
            }
        }

        RoomFormation();
    }

    private void RoomFormation()
    {
        ListClone(_chunks, _rooms);

        ShuffleList(_rooms);

        //StartCoroutine(TunnelFormationV2());
        TunnelFormationV3();

        for (int i = 0; i < _roomCount; i++)
        {
            //_map.SetTile(new Vector3Int(_chunks[i].x, _chunks[i].y, 1), _centerTest);

            RoomGeneration(_rooms[i]);
        }

        SetSpawnPoint();
    }

    private Vector2Int RecalculateMapSize()
    {
        int size = (_maxRoomSize + (_offset * 2)) * _chunkCount;

        return new Vector2Int(size, size);
    }

    private void RoomGeneration(Vector2Int center)
    {
        Vector2Int roomSize = new Vector2Int(Random.Range(_minRoomSize, _maxRoomSize),
            Random.Range(_minRoomSize, _maxRoomSize));

        _roomsInfo.Add(roomSize);

        for (int y = 0; y < roomSize.y; y++)
        {
            for (int x = 0; x < roomSize.x; x++)
            {
                _groundMap.SetTile(new Vector3Int(center.x + x - (roomSize.x / 2), center.y + y - (roomSize.y / 2), 0),
                    null);

                if (_bgTile != null)
                {
                    _bgMap.SetTile(
                        new Vector3Int(center.x + x - (roomSize.x / 2), center.y + y - (roomSize.y / 2), 0),
                        _bgTile
                    );
                }
            }
        }
    }

    private void SetSpawnPoint()
    {
        //First room in gen == spawn character

        try
        {
            _player.transform.position = (Vector3Int)_rooms[0];

            for (int i = 1; i < _roomCount; i++)
            {
                for (int j = 0; j < _enemyInOneRoom; j++)
                {
                    var enemy = Instantiate(_enemy[Random.Range(0, _enemy.Count)]);
                    enemy.transform.position =
                        (Vector3Int)new Vector2Int(
                            Random.Range(_rooms[i].x - (_roomsInfo[i].x / 2), _rooms[i].x + (_roomsInfo[i].x / 2)),
                            Random.Range(_rooms[i].y - (_roomsInfo[i].y / 2), _rooms[i].y + (_roomsInfo[i].y / 2))
                        );

                    _enemies.Add(enemy);
                }
            }

            Debug.Log(_enemies.Count);
            Enemies?.Invoke(_enemies);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private void TunnelFormationV3()
    {
        for (int i = 0; i < _roomCount - 1; i++)
        {
            Vector2Int nextRoom = new Vector2Int(_rooms[i + 1].x, _rooms[i + 1].y);

            Vector2Int distance = nextRoom - _rooms[i];

            var tilePos = _rooms[i];

            while (tilePos.x != nextRoom.x)
            {
                for (int j = 0; j < _widthTunnel; j++)
                {
                    _groundMap.SetTile(
                        new Vector3Int(tilePos.x, tilePos.y + j, 0),
                        null);

                    if (_bgTile != null)
                    {
                        _bgMap.SetTile(
                            new Vector3Int(tilePos.x, tilePos.y + j, 0),
                            _bgTile);
                    }
                }

                if (distance.x > 0)
                {
                    tilePos += Vector2Int.right;
                }
                else
                {
                    tilePos += Vector2Int.left;
                }
            }

            while (tilePos.y != nextRoom.y)
            {
                for (int j = 0; j < _widthTunnel; j++)
                {
                    _groundMap.SetTile(
                        new Vector3Int(tilePos.x + j, tilePos.y, 0),
                        null);

                    if (_bgTile != null)
                    {
                        _bgMap.SetTile(
                            new Vector3Int(tilePos.x + j, tilePos.y, 0),
                            _bgTile);
                    }
                }

                if (distance.y > 0)
                {
                    tilePos += Vector2Int.up;
                }
                else
                {
                    tilePos += Vector2Int.down;
                }
            }
        }

        Debug.Log("===END tunnel v3===");
    }

    public void MapClear()
    {
        //use JobSystem
    }

    private void ShuffleList<T>(List<T> list)
    {
        System.Random rand = new System.Random();

        for (int i = list.Count - 1; i >= 1; i--)
        {
            int j = rand.Next(i + 1);

            (list[j], list[i]) = (list[i], list[j]);
        }
    }

    private void ShuffleList<T>(T[] array)
    {
        System.Random rand = new System.Random();

        for (int i = array.Length - 1; i >= 1; i--)
        {
            int j = rand.Next(i + 1);

            (array[j], array[i]) = (array[i], array[j]);
        }
    }

    private void ListClone<T>(List<T> origin, List<T> clone)
    {
        foreach (var i in origin)
        {
            clone.Add(i);
        }
    }
}