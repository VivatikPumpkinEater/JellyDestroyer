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
        Destroy(this.gameObject, 0.2f);

        for (int i = 0; i < Random.Range(3, 10); i++)
        {
            var coin = Instantiate(_coin,
                new Vector3(transform.position.x + Random.Range(-0.1f, 0.1f),
                    transform.position.y + Random.Range(0f, 0.5f), 0), Quaternion.identity);

            coin.GetComponent<Rigidbody2D>().AddForce((Vector2.down + new Vector2(Random.Range(-1f, 1f), 0)) * 5,
                ForceMode2D.Impulse);
        }

        if (_blooSplash != null)
        {
            Instantiate(_blooSplash, transform.position, quaternion.identity);
        }
    }
}