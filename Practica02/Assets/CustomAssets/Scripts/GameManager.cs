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
    [SerializeField] private TextMeshProUGUI _defeat;

    //Victory panels
    [SerializeField] private GameObject _victoryPanel;
    [SerializeField] private TextMeshProUGUI _victory;

    //Coin handling variables
    private int _coinsInLevel;
    [SerializeField] private TextMeshProUGUI _numberOfCoins;

    [SerializeField] private CollectibleStash _playerCoinPurse;
    private GameObject _player;
    private Camera _mainCamera;
    private PositionChange _cameraScript;

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
        _mainCamera = Camera.main;
        _cameraScript = _mainCamera.GetComponent<PositionChange>();
        _victory.text = "";
        _defeat.text = "";
        FindNumberOfCoinsInLevel();
        UpdateNumberOfCoins();
    }


    // Update is called once per frame
    void Update()
    {
        _seconds += Time.deltaTime;
        _timerText.text = ((int)_seconds).ToString();

        ChangeCameraMode();
        if(!_defeated) {
            CheckVictory();
        }
        
    }

    public void FindNumberOfCoinsInLevel()
    {
        _coinsInLevel = GameObject.FindGameObjectsWithTag("Coin").Length;
    }

    public void UpdateNumberOfCoins()
    {
        _numberOfCoins.text = _playerCoinPurse.NumberOfItems.ToString();
    }

    public void HandleDefeat()
    {
        Instantiate(_defeat, _defeatPanel.transform);
        _defeat.text = "Defeat";
        Time.timeScale = 0;
        _defeatPanel.SetActive(true);
        _defeated = true;
        AudioManager.Instance.PlayDefeat();
    }

    public void CheckVictory()
    {
        if (_playerCoinPurse.NumberOfItems == _coinsInLevel)
        {
            Instantiate(_victory, _victoryPanel.transform);
            _victory.text = "Congrats! You have spent: " + _timerText.text;
            Time.timeScale = 0;
            _victoryPanel.SetActive(true);
        }
    }

    public void ChangeCameraMode()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            _cameraScript.ChangeCameraMode();
        }

    }

    private void OnDestroy()
    {
        _playerCoinPurse.NumberOfItems = 0;
    }
}
