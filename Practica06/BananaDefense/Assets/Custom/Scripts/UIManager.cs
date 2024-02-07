using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI _moneyText;
    [SerializeField] private GameObject _noMoneyWarning;

    [SerializeField] private TextMeshProUGUI _timeText;
    private float _currentTime;

    [SerializeField] private GameObject _gameOverPanel;

    void Awake()
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

    void Update()
    {
        _currentTime -= Time.deltaTime;
        _timeText.text = _currentTime.ToString("N2");

        if(_currentTime <= 0)
        {
            GameManager.Instance.GameOver();
        }
    }

    public void InitializeTime(float time)
    {
        _currentTime = time;
    }

    public void UpdateMoney(int availableMoney)
    {
        _moneyText.text = availableMoney.ToString();
    }

    public void ShowNotEnoughMoneyWarning()
    {
        _noMoneyWarning.SetActive(true);
        Invoke("DisableWarning", 1.2f);
    }

    private void DisableWarning()
    {
        _noMoneyWarning.SetActive(false);
    }

    public void ShowGameOverMessage(string message)
    {
        _gameOverPanel.SetActive(true);
        _gameOverPanel.GetComponentInChildren<TextMeshProUGUI>().text = message;
    }
}
