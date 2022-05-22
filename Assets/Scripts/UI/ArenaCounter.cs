using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArenaCounter : MonoBehaviour
{
    [SerializeField] private Text _arenaCounter = null;
    
    private void Start()
    {
        _arenaCounter.text = UIManager.Instance.ActualArena.ToString();
    }

    public void UpdateArena()
    {
        UIManager.Instance.SetArenaCounter(_arenaCounter);
    }
}
