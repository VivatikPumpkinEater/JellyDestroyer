using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : PickUp
{
    protected override void ActivateTrigger()
    {
        base.ActivateTrigger();
    }

    protected override void OnTriggerEnter2D(Collider2D col)
    {
        base.OnTriggerEnter2D(col);
    }
}