using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinsCounter : MonoBehaviour
{
    [SerializeField] private Text _coinsTxt = null;

    private int _coins = 0;

    private Coroutine _minusCoroutine = null;
    
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

    public void MinusCoins(int cost)
    {
        if (_minusCoroutine != null)
        {
            StopCoroutine(_minusCoroutine);
        }
        
        _minusCoroutine = StartCoroutine(MinusCoroutine(cost));
    }

    private IEnumerator MinusCoroutine(int iteration)
    {
        for (int i = 0; i < iteration / 5; i++)
        {
            _coins -= 5;
            UpdateCoinsInfo();

            yield return null;
        }
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
