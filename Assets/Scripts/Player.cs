using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _center = null;
    [SerializeField] private List<RigidbodyInfo> _rigidbodyInfos = new List<RigidbodyInfo>();

    private void Awake()
    {
        foreach (var info in _rigidbodyInfos)
        {
            info.Collision += Punch;
            info.Drag += Slam;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _center.velocity = new Vector2(0, -11);
            Slam();
        }
    }

    private void Punch(Enemy enemy)
    {
        enemy.Damage();
    }

    private void Slam()
    {
        if (_center.velocity.y < -10)
        {
            //var inRadius = Physics2D.OverlapCircleAll(_center.position, 4);
            var slamPunch = Physics2D.OverlapBoxAll(_center.position, new Vector2(3, 1), 0);

            for (int i = 0; i < slamPunch.Length; i++)
            {
                //var rb = inRadius[i].attachedRigidbody;
                var rb = slamPunch[i].attachedRigidbody;

                if (rb && !rb.GetComponent<RigidbodyInfo>() && !rb.gameObject.layer.Equals(LayerMask.GetMask("CoinUse")))
                {
                    if (rb.position.x > _center.position.x)
                    {
                        rb.AddForce((Vector2.up * 2 + Vector2.right) * 10, ForceMode2D.Impulse);
                    }
                    else
                    {
                        rb.AddForce((Vector2.up * 2 + Vector2.left) * 10, ForceMode2D.Impulse);
                    }
                    
                }
            }
        }
    }

    /*private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawCube(_center.position - new Vector2(0, 0.5f), new Vector2(3.5f, 0.5f));
    }*/
}