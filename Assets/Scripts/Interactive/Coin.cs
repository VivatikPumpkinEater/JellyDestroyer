using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : PickUp
{
    private CoinsCounter _coinsCount = null;

    private CoinsCounter _coinsCounter
    {
        get => _coinsCount = _coinsCount ?? UIManager.Instance.CoinsCounter;
    }
    protected override void Start()
    {
        base.Start();
    }

    protected override void ActivateTrigger()
    {
        base.ActivateTrigger();
    }

    protected override void OnTriggerEnter2D(Collider2D col)
    {
        base.OnTriggerEnter2D(col);
    }

    protected override void GoToUI()
    {
        base.GoToUI();
        
        _coinsCounter.AddCoin();
    }
}