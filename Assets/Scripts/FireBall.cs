using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    [SerializeField] private int _damage = 1;
    [SerializeField] private float _speed = 3f;

    [SerializeField] private ParticleSystem _hitEffect = null;

    [SerializeField] private CircleCollider2D _trigger = null;

    private void Start()
    {
        Invoke("SetTrigger", 0.1f);
    }

    private void SetTrigger()
    {
        _trigger.isTrigger = true;
    }

    private void Update()
    {
        transform.position += (transform.up * _speed) * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        var sufferer = col.GetComponent<HealthManager>() ??
                       col.GetComponentInParent<HealthManager>();

        var props = col.GetComponent<InteractiveProps>();

        if (sufferer)
        {
            sufferer.Damage(_damage, this.transform.position);
        }

        if (props)
        {
            props.Hit();
        }
        
        Destroy(gameObject);

        var boom = Instantiate(_hitEffect, transform.position, Quaternion.identity);
        boom.Play();
    }
}
