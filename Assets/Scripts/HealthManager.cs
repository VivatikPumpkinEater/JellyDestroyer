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

    public System.Action DamageEvent;
    public System.Action<EnemyBase> EnemyDie;
    
    private bool _acceptDamage = true;
    private Coroutine _resetAccept = null;

    private RipplePostProcessor _ripplePostProcessor = null;

    protected RipplePostProcessor _RippleEffect
    {
        get => Camera.main.GetComponent<RipplePostProcessor>();
    }

    public virtual void Damage(int damageValue, Vector2 damagePosition)
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
                DamageEvent?.Invoke();
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

    protected virtual void Die()
    {
        if (GetComponent<EnemyBase>())
        {
            EnemyDie?.Invoke(this.GetComponent<EnemyBase>());
        }

        if (Vector2.Distance(Player.Instance.transform.position, transform.position) <= 4)
        {
            _RippleEffect.RippleEffect(transform.position);
        }
        
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