using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FlyEnemy : MonoBehaviour
{
    [SerializeField] private AnimationCurve _fly = null;
    [SerializeField] private LayerMask _enemyTarget;
    [SerializeField] private float _speed = 5f;

    [SerializeField] private Player _player = null;
    [SerializeField] private float _pause = 3f;

    private bool _visibleStatus = false;

    private OnVisibleObject _onVisibleObject = null;

    private OnVisibleObject _visibleObject
    {
        get => _onVisibleObject = _onVisibleObject ?? GetComponentInChildren<OnVisibleObject>();
    }

    private HealthManager _healthManager = null;

    private HealthManager _hp
    {
        get => _healthManager = _healthManager ?? GetComponent<HealthManager>();
    }

    private Rigidbody2D _rigidbody2D = null;

    private Rigidbody2D _rb
    {
        get => _rigidbody2D = _rigidbody2D ?? GetComponent<Rigidbody2D>();
    }
    
    private bool _damage = false;

    private void Awake()
    {
        _visibleObject.Visible += ChangeStatus;
    }

    private void ChangeStatus(bool visible)
    {
        _visibleStatus = visible;
        
        Debug.Log(_visibleStatus);
    }
    
    public void Damage()
    {
        if (!_damage)
        {
            _damage = true;
            _hp.Damage(1);
        }
    }


    public void Fly()
    {
        _rb.velocity = Vector2.up * _speed;
        
        if (_visibleStatus && Vector2.Distance(transform.position, _player.transform.position) > 3f)
        {
            Vector2 direction = (_player.transform.position - transform.position).normalized;
            _rb.AddForce(direction * _speed, ForceMode2D.Impulse);
        }
    }
}