using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class HealthManager : MonoBehaviour
{
    [SerializeField] private int _health = 1;
    [SerializeField] private ParticleSystem _blooSplash = null;
    [SerializeField] private Coin _coin = null;

    public void Damage(int damageValue)
    {
        _health -= damageValue;

        Debug.Log("ChangeHp");

        if (_health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(this.gameObject);

        for (int i = 0; i < Random.Range(3, 10); i++)
        {
            Instantiate(_coin,
                new Vector3(transform.position.x + Random.Range(-3f, 3f),
                    transform.position.y + Random.Range(0f, 0.5f), 0), Quaternion.identity);
        }

        Instantiate(_blooSplash, transform.position, quaternion.identity);
    }
}