using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int AvailableBananas;
    private int _stolenBananas;

    [SerializeField] private GameObject _bananaPrefab;

    [SerializeField] private Transform[] _spawnPositions;

    private List<GameObject> _strawberriesOnMap;

    private GameObject _prize;

    //public static event Action<GameObject> OnStrawberryAdded;

    //public static event Action<GameObject> OnStrawberryConsumed;

    public static event Action<List<GameObject>> OnStrawberryOnMap;

    public static event Action<GameObject> OnStrawberryShortage;


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
        _strawberriesOnMap = new List<GameObject>();
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
        UIManager.Instance.UpdateBananaCounter(AvailableBananas);
        if(_stolenBananas >= AvailableBananas)
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
        _strawberriesOnMap.Add(strawberry);
        OnStrawberryOnMap?.Invoke(_strawberriesOnMap);
    }

    public void RemoveStrawberry(GameObject strawberry, GameObject monkey)
    {
        _strawberriesOnMap.Remove(strawberry);
        Destroy(strawberry);
        monkey.GetComponent<Following>().Leave();
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

    private void GameOver()
    {
        Debug.Log("Game Over");
    }
}
