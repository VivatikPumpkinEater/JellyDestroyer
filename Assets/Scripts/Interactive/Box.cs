using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class Box : InteractiveProps
{
    [SerializeField] private ParticleSystem _destroyEffect = null;
    protected override void OnCollisionEnter2D(Collision2D col)
    {
        base.OnCollisionEnter2D(col);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            Destruction();
        }
    }

    public void Destruction()
    {
        //Debug.Log("Destroy");
        Destroy(this.gameObject);

        if (_destroyEffect != null)
        {
            var destroyEffect = Instantiate(_destroyEffect, transform.position, Quaternion.identity);
            destroyEffect.Play();
        }
        
        var destroyBox = Instantiate(_DestroyVariant, transform.position, quaternion.identity);
        var rbs = destroyBox.GetComponentsInChildren<Rigidbody2D>();
        
        Slam();
        
        for (int i = 0; i < rbs.Length; i++)
        {
            Vector2 direction = (rbs[i].transform.position - transform.position).normalized;
            
            rbs[i].AddForce(direction * _Force, ForceMode2D.Impulse);
        }
        
        for (int i = 0; i < Random.Range(3, 10); i++)
        {
            var coin = Instantiate(_Coin,
                new Vector3(transform.position.x + Random.Range(-0.1f, 0.1f),
                    transform.position.y + Random.Range(0f, 0.5f), 0), Quaternion.identity);

            coin.GetComponent<Rigidbody2D>().AddForce((Vector2.down + new Vector2(Random.Range(-1f, 1f), 0)) * 5,
                ForceMode2D.Impulse);
        }
    }

    public override void Hit()
    {
        base.Hit();
    }
}
