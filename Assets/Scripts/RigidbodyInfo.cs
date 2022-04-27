using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidbodyInfo : MonoBehaviour
{
    public System.Action<Enemy> Collision;
    public System.Action Drag;
    
    private void OnCollisionEnter2D(Collision2D col)
    {
        var enemy = col.gameObject.GetComponentInParent<Enemy>();

        if (enemy)
        {
            Collision?.Invoke(enemy);
        }
        else
        {
            Drag?.Invoke();
        }
    }
}
