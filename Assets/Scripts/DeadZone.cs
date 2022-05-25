using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        var poolObject = col.GetComponent<PoolObject>();
        var enemy = col.GetComponent<HealthManager>();

        if (poolObject)
        {
            poolObject.ReturnToPool();
        }

        if (enemy)
        {
            enemy.Damage(100, transform.position);
        }
        
        Debug.Log("Dest " + col.name);
    }
}
