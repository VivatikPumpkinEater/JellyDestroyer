using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;

public class CharacterChange : MonoBehaviour
{
    [SerializeField] private PlayerSaveData _playerSave = null;

    [SerializeField] private List<PlayerStats> _charcters = new List<PlayerStats>();

    [SerializeField] private Button _left = null;
    [SerializeField] private Button _right = null;
    [SerializeField] private Button _buy = null;
    [SerializeField] private Button _select = null;
    [SerializeField] private Button _home = null;

    [SerializeField] private List<Text> _nameTextComponents = new List<Text>();
    [SerializeField] private List<Text> _costTextComponents = new List<Text>();

    private Vector2 _startPoint = Vector2.zero;
    private int _offset = 5;

    private int _actualIndex = 0;
    private bool _accept = true;

    private void Awake()
    {
        _left.onClick.AddListener(PreviousCharacter);
        _right.onClick.AddListener(NextCharacter);
        _buy.onClick.AddListener(BuyCharacter);
        _select.onClick.AddListener(SelectCharacter);
        _home.onClick.AddListener(BackToMainMenu);
        
        SpawnCharacter();
        UpdateName();
    }

    private void SpawnCharacter()
    {
        for (int i = 0; i < _charcters.Count; i++)
        {
            var character = Instantiate(_charcters[i].PlayerDecoration, _startPoint, Quaternion.identity);

            _startPoint += new Vector2(_offset, 0);
        }
    }

    private void NextCharacter()
    {
        if (_actualIndex + 1 != _charcters.Count && _accept)
        {
            _accept = false;

            var end = Camera.main.transform.position + new Vector3(_offset, 0, 0);

            Camera.main.transform.DOMove(end, 1f).OnComplete(() => _accept = true);

            _actualIndex++;
            UpdateName();
        }
    }

    private void PreviousCharacter()
    {
        if (_actualIndex - 1 >= 0 && _accept)
        {
            _accept = false;

            var end = Camera.main.transform.position - new Vector3(_offset, 0, 0);

            Camera.main.transform.DOMove(end, 1f).OnComplete(() => _accept = true);

            _actualIndex--;
            UpdateName();
        }
    }

    private void BuyCharacter()
    {
        if(_playerSave.Money - _charcters[_actualIndex].Cost >= 0)
        {
            _playerSave.Money -= _charcters[_actualIndex].Cost;
            
            _charcters[_actualIndex].Acquired = true;
            _playerSave.AcquiredSkinsList.Add(_charcters[_actualIndex]);

            UpdateName();
        }
    }

    private void SelectCharacter()
    {
        _playerSave.ActiveCharacter = _charcters[_actualIndex];
    }

    private void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    private void UpdateName()
    {
        bool acquired = false;

        switch (_charcters[_actualIndex].Acquired)
        {
            case true:
                acquired = true;
                _costTextComponents[0].gameObject.SetActive(false);

                _buy.gameObject.SetActive(false);
                _select.gameObject.SetActive(true);
                break;
            case false:
                _costTextComponents[0].gameObject.SetActive(true);

                _buy.gameObject.SetActive(true);
                _select.gameObject.SetActive(false);
                break;
        }

        for (int i = 0; i < _nameTextComponents.Count; i++)
        {
            _nameTextComponents[i].text = _charcters[_actualIndex].Name;

            if (!acquired)
            {
                _costTextComponents[i].text = _charcters[_actualIndex].Cost.ToString();
            }
        }
    }
}