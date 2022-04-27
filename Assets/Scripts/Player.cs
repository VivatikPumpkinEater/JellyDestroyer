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
            Debug.Log(_center.velocity.y);
            var inRadius = Physics2D.OverlapCircleAll(_center.position, 4);

            for (int i = 0; i < inRadius.Length; i++)
            {
                var rb = inRadius[i].attachedRigidbody;

                if (rb && !rb.GetComponent<RigidbodyInfo>())
                {
                    rb.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                }
            }
        }
    }
}
