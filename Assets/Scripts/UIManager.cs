using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance = null;

    [SerializeField] private CoinsCounter _coinsCounter = null;
    [SerializeField] private Animator _transitionAnimator = null;

    [SerializeField] private GameObject _gameOverScreen = null;
    [SerializeField] private Button _restartButton = null;

    [SerializeField] private GameObject _pauseScreen = null;
    [SerializeField] private Button _menu = null;

    [Header("Game Over")] [SerializeField] private Text _arenaCount = null;
    [SerializeField] private Text _coins = null;
    [SerializeField] private Text _bestArena = null;

    [SerializeField] private DontDestroy _dontDestroy = null;
    [SerializeField] private SlowMo _slowMo = null;

    private float _timeScaleGameOver = 25f;
    
    private static int _actualArena = 1;

    private static bool _start = false;

    public bool StartGame
    {
        get => _start;
        set => _start = value;
    }

    public int ActualArena
    {
        get => _actualArena;
    }

    public CoinsCounter CoinsCounter
    {
        get => _coinsCounter;
    }

    private void Awake()
    {
        Debug.Log(_actualArena);
        
        _restartButton.onClick.AddListener(Restart);
        _menu.onClick.AddListener(Menu);

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
        _actualArena++;
    }

    private void Restart()
    {
        SceneManager.LoadScene(2);
        _slowMo.DisableSlowMotion(_timeScaleGameOver);
    }

    public void GameOver()
    {
        _gameOverScreen.SetActive(true);
        _slowMo.OnSlowMotion(_timeScaleGameOver);

        _arenaCount.text = _actualArena.ToString();

        if (PlayerPrefs.HasKey("BestArena"))
        {
            if (PlayerPrefs.GetInt("BestArena") < _actualArena)
            {
                PlayerPrefs.SetInt("BestArena", _actualArena);
            }
        }
        else
        {
            PlayerPrefs.SetInt("BestArena", _actualArena);
        }
        
        _bestArena.text = PlayerPrefs.GetInt("BestArena").ToString();

        _actualArena = 1;
    }

    private void Menu()
    {
        SceneManager.LoadScene(0);
        
        _slowMo.DisableSlowMotion();
        _dontDestroy.DestroyAll();
    }

    public void Pause(bool status)
    {
        _pauseScreen.SetActive(status);
    }

    public void SetArenaCounter(Text arenaCounter)
    {
        arenaCounter.text = _actualArena.ToString();
        _transitionAnimator.SetTrigger("Exit");
    }
}