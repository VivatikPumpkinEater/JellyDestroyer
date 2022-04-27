using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGeneration : MonoBehaviour
{
    [SerializeField] private int _placeCount = 4;
    [SerializeField] private Vector2Int _minSize, _maxSize;
    [SerializeField] private int _offset = 20;
    [SerializeField] private int _seed = 0;
    [SerializeField] private int _zoom = 0;
    
    [SerializeField] private RuleTile _tileRule = null;
    [SerializeField] private Tilemap _map = null;
    
    private Vector2Int _sizeMap;

    private void Awake()
    {
        _sizeMap = new Vector2Int(_maxSize.x * _placeCount + (_offset * 2), _maxSize.y * _placeCount + (_offset * 2));
        
        PerlinNoiseGenerate();
    }

    private void FirstGeneration()
    {
        /*for (int y = 0; y < _sizeMap.y; y++)
        {
            for (int x = 0; x < _sizeMap.x; x++)
            {
                _map.SetTile(new Vector3Int(x,y,0), _tileRule);
            }
        }*/
    }
    
    
    private void PerlinNoiseGenerate()
    {
        for (int x = 0; x < _sizeMap.x; x++)
        {
            for (int y = 0; y < _sizeMap.y; y++)
            {
                var perlin = Mathf.PerlinNoise((x + _seed) / _zoom, (y + _seed) / _zoom);

                if (perlin <= 0.5f)
                {
                    _map.SetTile(new Vector3Int(x,y,0), null);
                }
                else
                {
                    _map.SetTile(new Vector3Int(x,y,0), _tileRule);
                }
            }
        }
        
        //BuildWall();
    }
}
