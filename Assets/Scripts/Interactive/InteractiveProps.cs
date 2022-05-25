using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveProps : MonoBehaviour
{
    [SerializeField] protected float _Force = 5f;
    [SerializeField] protected GameObject _DestroyVariant = null;
    [SerializeField] protected Coin _Coin = null;

    protected bool _Collision = false;
    private Animator _animator = null;

    protected Animator _Anim
    {
        get => _animator = _animator ?? GetComponent<Animator>();
    }

    protected virtual void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("Collision");
        var player = col.gameObject.GetComponentInParent<Player>();

        if (!_Collision && player)
        {
            _Collision = true;

            Hit();
        }
    }

    public virtual void Hit()
    {
        _Anim.SetTrigger("Hit");
    }

    protected virtual void Slam()
    {
        var slamPunch = Physics2D.OverlapBoxAll(transform.position, new Vector2(3, 1), 0);

        for (int i = 0; i < slamPunch.Length; i++)
        {
            //var rb = inRadius[i].attachedRigidbody;
            var rb = slamPunch[i].attachedRigidbody;

            if (rb && !rb.gameObject.layer.Equals(LayerMask.GetMask("CoinUse")))
            {
                if (rb.position.x > transform.position.x)
                {
                    rb.AddForce((Vector2.up * 2 + Vector2.right) * _Force, ForceMode2D.Impulse);
                }
                else
                {
                    rb.AddForce((Vector2.up * 2 + Vector2.left) * _Force, ForceMode2D.Impulse);
                }
            }
        }
    }
}