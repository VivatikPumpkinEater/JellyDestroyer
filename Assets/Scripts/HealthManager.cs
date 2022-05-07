using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class HealthManager : MonoBehaviour
{
    [SerializeField] protected int _Health = 1;
    [SerializeField] protected ParticleSystem _DeadEffect = null;
    [SerializeField] private Coin _coin = null;

    private bool _acceptDamage = true;
    private Coroutine _resetAccept = null;

    public virtual void Damage(int damageValue)
    {
        if (_acceptDamage)
        {
            _acceptDamage = false;

            _Health -= damageValue;

            Debug.Log("ChangeHp");

            if (_Health <= 0)
            {
                Die();
            }
            else
            {
                _resetAccept = StartCoroutine(ResetAcceptDamage());
                Debug.Log("ResetAccept");
            }
        }
    }

    protected IEnumerator ResetAcceptDamage()
    {
        yield return new WaitForSecondsRealtime(1f);

        _acceptDamage = true;
    }

    private void Die()
    {
        Destroy(this.gameObject, 0.2f);

        if (_coin != null)
        {
            for (int i = 0; i < Random.Range(3, 10); i++)
            {
                var coin = Instantiate(_coin,
                    new Vector3(transform.position.x + Random.Range(-0.1f, 0.1f),
                        transform.position.y + Random.Range(0f, 0.5f), 0), Quaternion.identity);

                coin.GetComponent<Rigidbody2D>().AddForce((Vector2.down + new Vector2(Random.Range(-1f, 1f), 0)) * 5,
                    ForceMode2D.Impulse);
            }
        }

        if (_DeadEffect != null)
        {
            Instantiate(_DeadEffect, transform.position, quaternion.identity);
        }
    }
}