using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : HealthManager
{
    [SerializeField] private PlayerStats _playerStats = null;

    public System.Action<Vector2> ImpulseEvent;
    
    private void Awake()
    {
        if (_playerStats != null)
        {
            _Health = _playerStats.Health;
        }
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
        
        if (_Health > 0)
        {
            ImpulseEvent?.Invoke(damagePosition);
        }
    }
}
