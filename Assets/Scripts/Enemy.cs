using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(HealthManager))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private float _jumpPower = 5;

    [SerializeField] private float _time = 3f;

    [SerializeField] private List<Rigidbody2D> _rb = new List<Rigidbody2D>();
    private float _timer = float.MinValue;
    
    private HealthManager _healthManager = null;

    private HealthManager _hp
    {
        get => _healthManager = _healthManager ?? GetComponent<HealthManager>();
    }

    private bool _damage = false;

    private void Awake()
    {
        _timer = _time;
    }

    private void Update()
    {
        _timer -= Time.deltaTime;
        if (_timer < 0)
        {
            _timer = _time;

            switch (Random.Range(0,2))
            {
                case 0:
                    Jump(Vector2.up * 2 + Vector2.left);
                    break;
                case 1:
                    Jump(Vector2.up * 2 + Vector2.right);
                    break;
            }
        }
    }
    
    private void Jump(Vector2 dir)
    {
        
        for (int i = 0; i < _rb.Count; i++)
        {
            _rb[i].velocity = Vector2.zero;
            _rb[i].angularVelocity = 0;

            _rb[i].velocity = new Vector2(_rb[i].velocity.x, 0);
            _rb[i].velocity += dir * (_jumpPower / 2);
        }
    }
}
