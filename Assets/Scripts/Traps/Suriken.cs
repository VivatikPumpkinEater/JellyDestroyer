using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Suriken : MonoBehaviour
{
    [SerializeField] private int _damage = 1;
    private void OnCollisionEnter2D(Collision2D col)
    {
        var sufferer = col.collider.GetComponent<HealthManager>() ??
                       col.collider.GetComponentInParent<HealthManager>();

        if (sufferer)
        {
            sufferer.Damage(_damage, this.transform.position);
        }
    }
}
