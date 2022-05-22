using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ChangeableUI : MonoBehaviour
{
    [SerializeField] private Image _heart = null;
    [SerializeField] private Sprite _heartEmpty = null;
    [SerializeField] private GameObject _healthStats = null;

    private List<Image> _health = new List<Image>();

    private void Start()
    {
        Player.Instance.PlayerHealth.DamageEvent += MinusHealth;

        FillHealth();
        for (int i = 0; i < Player.Instance.PlayerHealth.MaxHealth - Player.Instance.PlayerHealth.Health; i++)
        {
            Debug.Log("MinusNew");

            MinusHealth();
        }
    }

    private void FillHealth()
    {
        for (int i = 0; i < Player.Instance.PlayerHealth.MaxHealth; i++)
        {
            _health.Add(Instantiate(_heart, _healthStats.transform));
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            MinusHealth();
        }
    }

    private void MinusHealth()
    {
        _health[_health.Count - 1].sprite = _heartEmpty;
        _health.RemoveAt(_health.Count - 1);
    }
}