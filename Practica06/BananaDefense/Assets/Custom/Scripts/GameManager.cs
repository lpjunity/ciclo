using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private int _availablePrize;
    private int _stolenPrize;

    [SerializeField] private GameObject _prizePrefab;

    [SerializeField] private Transform[] _spawnPositions;

    public List<GameObject> _mangosOnMap;

    private GameObject _prize;

    private int _availableMoney;
    private int _monkeysSatisfied;

    [SerializeField] private int _initialMoney;
    [SerializeField] private int _monkeySatisfiedPrice;
    [SerializeField] private float _levelTime;
    [SerializeField] private float _prizeSpawnCoolDown;

    public static event Action<List<GameObject>> OnMangoOnMap;
    public static event Action<GameObject> OnMangoShortage;

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

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
        _mangosOnMap = new List<GameObject>();
        SpawnPrize();
        _availableMoney = _initialMoney;
        UIManager.Instance.UpdateMoney(_availableMoney);
        UIManager.Instance.InitializeTime(_levelTime);
        UIManager.Instance.UpdateRemainingPrizes(_availablePrize);
    }

    public void StartGame()
    {
        UIManager.Instance.RemoveMainMenu();
        Time.timeScale = 1;
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    // Update is called once per frame
    /*void Update()
    {
        


    }*/

    private void SpawnPrize()
    {
        int positionIndex = UnityEngine.Random.Range(0, _spawnPositions.Length);
        GameObject banana = Instantiate(_prizePrefab, _spawnPositions[positionIndex].position, Quaternion.identity);
        _prize = banana;
    }

    public void AddMango(GameObject mango)
    {
        if (_mangosOnMap.Contains(mango))
        {
            return;
        }
        _mangosOnMap.Add(mango);
        OnMangoOnMap?.Invoke(_mangosOnMap);
    }

    public void RemoveMango(GameObject mango, GameObject monkey)
    {
        monkey.GetComponent<FollowingBehaviour>().Leave();
        _monkeysSatisfied++;
        _mangosOnMap.Remove(mango);
        Destroy(mango);
        EarnMoney(_monkeySatisfiedPrice);
        NotifyTheMonkeys();
    }

    private void NotifyTheMonkeys()
    {
        if (_mangosOnMap.Count > 0)
        {
            OnMangoOnMap?.Invoke(_mangosOnMap);
        }
        else
        {
            OnMangoShortage?.Invoke(FindPrizeOnMap());
        }
    }

    private GameObject FindPrizeOnMap()
    {
        return _prize ? _prize : GameObject.FindGameObjectWithTag("Prize");
    }

    public void GameOver()
    {
        string message = $"You made the monkeys go hungry, you lose! ";

        if (!AllPrizesStolen())
        {
            int moneyEarned = _availableMoney + (_monkeysSatisfied * _monkeySatisfiedPrice);
            message = $"Congratz! You earned {moneyEarned} making happy monkeys :D";
        }
        
        UIManager.Instance.ShowGameOverMessage(message);
        Time.timeScale = 0;
    }

    public bool HasMoneyForBuilding(int price)
    {
        return _availableMoney >= price;
    }

    public void EarnMoney(int price)
    {
        _availableMoney += price;
        UIManager.Instance.UpdateMoney(_availableMoney);
    }

    public void SpendMoney(int price)
    {
        _availableMoney -= price;
        UIManager.Instance.UpdateMoney(_availableMoney);
    }

    public bool AllPrizesStolen()
    {
        return _stolenPrize >= _availablePrize;
    }

    public void ConsumePrize(GameObject prize, GameObject monkey)
    {
        _stolenPrize++;

        if (_stolenPrize >= _availablePrize)
        {
            GameOver();
        }
        else
        {
            Destroy(prize);
            monkey.GetComponent<FollowingBehaviour>().Leave();
            Invoke("SpawnPrize", _prizeSpawnCoolDown);
            Invoke("NotifyTheMonkeys", _prizeSpawnCoolDown + .5f);
            UIManager.Instance.UpdateRemainingPrizes(_availablePrize - _stolenPrize);
        }
        
    }
}
