using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private int _availableBananas;
    private int _stolenBananas;

    [SerializeField] private GameObject _bananaPrefab;

    [SerializeField] private Transform[] _spawnPositions;

    public List<GameObject> _strawberriesOnMap;

    private GameObject _prize;

    private int _availableMoney;
    private int _monkeysSatisfied;

    [SerializeField] private int _initialMoney;
    [SerializeField] private int _monkeySatisfiedPrice;
    [SerializeField] private float _levelTime;

    public static event Action<List<GameObject>> OnStrawberryOnMap;

    public static event Action<GameObject> OnStrawberryShortage;

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
        _strawberriesOnMap = new List<GameObject>();
        _availableMoney = _initialMoney;
        UIManager.Instance.UpdateMoney(_availableMoney);
        UIManager.Instance.InitializeTime(_levelTime);
    }

    // Update is called once per frame
    void Update()
    {
        


    }

    /*public void TakePrize(GameObject prizeTaken)
    {
        if (prizeTaken.CompareTag("Prize"))
        {
            RemoveBanana();
            Destroy(prizeTaken);
        }
        else
        {
            
        }
        
    }*/

    private void RemoveBanana()
    {
        _stolenBananas++;
        //UIManager.Instance.UpdateBananaCounter(AvailableBananas);
        if(_stolenBananas >= _availableBananas)
        {
            GameOver();
        }
        else
        {
            Invoke("SpawnBanana", 3f);
        }
    }

    private void SpawnBanana()
    {
        int positionIndex = UnityEngine.Random.Range(0, _spawnPositions.Length);
        GameObject banana = Instantiate(_bananaPrefab, _spawnPositions[positionIndex]);


    }

    public void AddStrawberry(GameObject strawberry)
    {
        if (_strawberriesOnMap.Contains(strawberry))
        {
            return;
        }
        _strawberriesOnMap.Add(strawberry);
        OnStrawberryOnMap?.Invoke(_strawberriesOnMap);
    }

    public void RemoveStrawberry(GameObject strawberry, GameObject monkey)
    {
        monkey.GetComponent<Following>().Leave();
        _monkeysSatisfied++;
        _strawberriesOnMap.Remove(strawberry);
        Destroy(strawberry);
        EarnMoney(_monkeySatisfiedPrice);

        if(_strawberriesOnMap.Count > 0)
        {
            OnStrawberryOnMap?.Invoke(_strawberriesOnMap);
        }
        else
        {
            OnStrawberryShortage?.Invoke(FindPrizeOnMap());
        }
    }

    private GameObject FindPrizeOnMap()
    {
        return _prize ? _prize : GameObject.FindGameObjectWithTag("Prize");
    }

    public void GameOver()
    {
        string message = $"You made the monkeys go hungry, you lose! ";

        if (!AllBananasStolen())
        {
            int moneyEarned = _availableMoney + (_monkeysSatisfied * _monkeySatisfiedPrice);
            message = $"Congratz! You earned {moneyEarned} making happy monkeys :D";
        }
        
        UIManager.Instance.ShowGameOverMessage(message);
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

    public bool AllBananasStolen()
    {
        return _stolenBananas >= _availableBananas;
    }
}
