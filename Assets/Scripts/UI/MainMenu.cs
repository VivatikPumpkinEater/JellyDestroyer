using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private PlayerSaveData _playerSaveData = null;
    [SerializeField] private Transform _decorationPosition = null;

    [SerializeField] private Button _characters = null;
    [SerializeField] private Button _start = null;

    private void Awake()
    {
        _characters.onClick.AddListener(LoadCharacterChanger);
        _start.onClick.AddListener(LoadGame);
        
        SpawnDecoration();
    }

    private void SpawnDecoration()
    {
        var decoration = Instantiate(_playerSaveData.ActiveCharacter.PlayerDecoration, _decorationPosition);
        decoration.transform.position = _decorationPosition.position;
    }

    private void LoadCharacterChanger()
    {
        SceneManager.LoadScene(1);
    }

    private void LoadGame()
    {
        SceneManager.LoadScene(2);
    }
}