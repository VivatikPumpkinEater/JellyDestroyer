using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : HealthManager
{
    [SerializeField] private PlayerStats _playerStats = null;
    
    
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
    public override void Damage(int damageValue)
    {
        base.Damage(damageValue);
        
    }
}
