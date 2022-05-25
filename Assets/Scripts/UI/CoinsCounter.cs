using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinsCounter : MonoBehaviour
{
    [SerializeField] private Text _coinsTxt = null;

    private int _coins = 0;
    
    private void Awake()
    {
        Debug.Log(PlayerPrefs.HasKey("Coin"));
        
        if (PlayerPrefs.HasKey("Coin"))
        {
            _coins = PlayerPrefs.GetInt("Coin");
            UpdateCoinsInfo();
        }
    }

    public void AddCoin()
    {
        _coins++;
        UpdateCoinsInfo();
    }

    private void UpdateCoinsInfo()
    {
        _coinsTxt.text = _coins.ToString();
        
        SaveCoins();
    }

    public void UpdateCoinsInfo(int money)
    {
        _coins = money;
        
        UpdateCoinsInfo();
    }

    private void OnDestroy()
    {
        SaveCoins();
    }

    private void SaveCoins()
    {
        PlayerPrefs.SetInt("Coin", _coins);
    }

    private void MinusMoney(int cost)
    {
        _coins -= cost;
        
        UpdateCoinsInfo();
    }
}
