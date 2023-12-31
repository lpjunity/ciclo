using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    //Timer handling variables
    [SerializeField] private float _seconds;
    [SerializeField] private TextMeshProUGUI _timerText;

    //Defeat panels
    [SerializeField] private GameObject _defeatPanel;
    [SerializeField] private TextMeshProUGUI _defeatByTime;
    [SerializeField] private TextMeshProUGUI _defeatByEnemy;

    //Victory panels
    [SerializeField] private GameObject _victoryPanel;
    [SerializeField] private TextMeshProUGUI _completeVictory;
    [SerializeField] private TextMeshProUGUI _victory;
    [SerializeField] private TextMeshProUGUI _sadVictory;

    //Coin handling variables
    private int _coinsInLevel;
    [SerializeField] private TextMeshProUGUI _numberOfCoins;
    //We could always search this one, but...

    [SerializeField] private CoinPurse _playerCoinPurse;
    private GameObject _player;

    //Victory/Defeat states
    private bool _defeated;
    private bool _won;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        FindNumberOfCoinsInLevel();
        UpdateNumberOfCoins();
    }


    // Update is called once per frame
    void Update()
    {
        _seconds -= Time.deltaTime;
        _timerText.text = ((int)_seconds).ToString();
        if (_seconds < 0 && !_defeated)
        {
            HandleDefeat();
        }
    }

    public void FindNumberOfCoinsInLevel()
    {
        _coinsInLevel = GameObject.FindGameObjectsWithTag("Coin").Length;
    }

    public void UpdateNumberOfCoins()
    {
        _numberOfCoins.text = _playerCoinPurse.NumberOfCoins.ToString();
    }

    public void HandleDefeat()
    {
        
        if (_player.activeSelf)
        {
            Instantiate(_defeatByTime, _defeatPanel.transform);
        }
        else
        {
            Instantiate(_defeatByEnemy, _defeatPanel.transform);
        }
        Time.timeScale = 0;
        _defeatPanel.SetActive(true);
        _defeated = true;
        AudioManager.Instance.PlayDefeat();
    }

    public void HandleVictory()
    {
        Inventory playerInventory = _player?.GetComponent<Inventory>();

        if (playerInventory.GetNumberOfCoins() == _coinsInLevel)
        {
            Instantiate(_completeVictory, _victoryPanel.transform);
        }
        else if (playerInventory.GetNumberOfCoins() == 0)
        {
            Instantiate(_sadVictory, _victoryPanel.transform);
        }
        else
        {
            Instantiate(_victory, _victoryPanel.transform);
        }
        Time.timeScale = 0;
        _victoryPanel.SetActive(true);
    }

}
