using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidbodyInfo : MonoBehaviour
{
    public System.Action<HealthManager, Rigidbody2D> Collision;
    public System.Action Drag;
    
    private void OnCollisionEnter2D(Collision2D col)
    {
        var sufferer = col.collider.GetComponent<HealthManager>() ??
                       col.collider.GetComponentInParent<HealthManager>();

        if (sufferer && !sufferer.GetComponent<Player>())
        {
            Collision?.Invoke(sufferer, this.gameObject.GetComponent<Rigidbody2D>());
        }
        else
        {
            Drag?.Invoke();
        }
    }
}
