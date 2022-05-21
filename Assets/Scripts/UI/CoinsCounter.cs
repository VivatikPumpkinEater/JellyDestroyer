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
    }

    private void OnDestroy()
    {
        SaveCoins();
    }

    private void SaveCoins()
    {
        PlayerPrefs.SetInt("Key", _coins);
    }
}
