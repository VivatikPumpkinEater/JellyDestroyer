using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button _characters = null;
    [SerializeField] private Button _start = null;

    private void Awake()
    {
        _characters.onClick.AddListener(LoadCharacterChanger);
        _start.onClick.AddListener(LoadGame);
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
