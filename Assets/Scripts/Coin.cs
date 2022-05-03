using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private CircleCollider2D _trigger = null;

    private void Start()
    {
        _trigger.isTrigger = false;

        Invoke("ActivateTrigger", 1f);
    }

    private void ActivateTrigger()
    {
        _trigger.isTrigger = true;
        gameObject.layer = LayerMask.NameToLayer("CoinUse");
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        var player = col.GetComponentInParent<Player>();

        if (player)
        {
            Destroy(gameObject);
        }
    }
}