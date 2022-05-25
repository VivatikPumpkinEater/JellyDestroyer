using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;
    
    [SerializeField]private Camera _camera;
    [SerializeField] private CinemachineVirtualCamera _vc = null;

    [SerializeField] private RoomGeneratorV4 _mapGen = null;
    
    [SerializeField] private PlayerSaveData _playerSaveData = null;
    
    private List<EnemyBase> _enemies = new List<EnemyBase>();

    public System.Action WinGame;
    
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance.gameObject);
            Instance = null;
        }

        Instance = this;
        
        SpawnPlayer();
        
        //_camera = Camera.main;
    }

    private void SpawnPlayer()
    {
        var player = Instantiate(_playerSaveData.ActiveCharacter.PlayerPrefab, transform);
        player.transform.position = transform.position;

        _mapGen.Player = player.gameObject;
        
        _vc.Follow = player.GetComponent<Player>().Center;
    }

    private void Start()
    {
        GetEnemies(RoomGeneratorV4.Instance.EnemiesAll);
    }

    private void SubscribeEnemies()
    {
        Debug.Log(_enemies.Count);
        
        foreach (var enemy in _enemies)
        {
            enemy.GetComponent<HealthManager>().EnemyDie += DeleteEnemy;
        }
    }

    private void GetEnemies(List<EnemyBase> enemies)
    {
        Debug.Log("Enemy done");
        
        ListClone(enemies, _enemies);
        
        SubscribeEnemies();
    }

    private void DeleteEnemy(EnemyBase enemy)
    {
        for (int i = 0; i < _enemies.Count; i++)
        {
            if (_enemies[i].Equals(enemy))
            {
                _enemies.RemoveAt(i);
                break;
            }
        }

        if (_enemies.Count <= 0)
        {
            Debug.Log("Win");
            WinGame?.Invoke();
        }
    }

    public Vector2 PropsToUI()
    {
        Vector2 screen = new Vector2(Camera.main.pixelWidth, Camera.main.pixelHeight);

        return _camera.ScreenToWorldPoint(screen);
    }
    
    private void ListClone<T>(List<T> origin, List<T> clone)
    {
        foreach (var i in origin)
        {
            clone.Add(i);
        }
    }
}
