using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : HealthManager
{
    [SerializeField] private PlayerStats _playerStats = null;

    private static int _hpPlayer = 0;
    
    public System.Action<Vector2> ImpulseEvent;

    private static bool _start = false;
    private void Awake()
    {
        if (_playerStats != null && !_start)
        {
            _hpPlayer = _playerStats.Health;

            _start = true;
        }

        _Health = _hpPlayer;
    }

    public int Health
    {
        get => _Health;
    }

    public int MaxHealth
    {
        get => _playerStats.Health;
    }

    public override void Damage(int damageValue, Vector2 damagePosition)
    {
        base.Damage(damageValue, damagePosition);
        _hpPlayer = _Health;
        
        Camera.main.GetComponentInParent<Animator>().SetTrigger("Shake");
        
        if (_Health > 0)
        {
            ImpulseEvent?.Invoke(damagePosition);
        }
    }

    protected override void Die()
    {
        UIManager.Instance.GameOver();
        
        UIManager.Instance.StartGame = false;
        _start = false;
    }
}
