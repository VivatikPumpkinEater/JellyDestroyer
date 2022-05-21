using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance = null;

    [SerializeField] private CoinsCounter _coinsCounter = null;
    [SerializeField] private Animator _transitionAnimator = null;

    public CoinsCounter CoinsCounter
    {
        get => _coinsCounter;
    }

    private void Awake()
    {
        CheckInstance();
    }

    private void CheckInstance()
    {
        if (Instance != null)
        {
            Destroy(Instance.gameObject);
            Instance = null;
        }

        Instance = this;
    }

    public void StartTransition()
    {
        _transitionAnimator.SetTrigger("Start");
    }
}
