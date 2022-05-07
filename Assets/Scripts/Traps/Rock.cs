using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(CircleCollider2D))]
public class Rock : MonoBehaviour
{
    [Range(-20, 0)] [SerializeField] private float _targetVelocity = -5f;

    private Rigidbody2D _rigidbody2D = null;

    private Rigidbody2D _rb
    {
        get => _rigidbody2D = _rigidbody2D ?? GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (_rb.velocity.y <= _targetVelocity)
        {
            var sufferer = col.collider.GetComponent<HealthManager>() ??
                           col.collider.GetComponentInParent<HealthManager>();
            Debug.Log(sufferer);

            if (sufferer)
            {
                try
                {
                    sufferer.Damage(1);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }
    }
}