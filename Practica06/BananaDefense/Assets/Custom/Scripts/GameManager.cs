using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int AvailableBananas;
    private int _stolenBananas;

    [SerializeField] private GameObject _bananaPrefab;

    [SerializeField] private Transform[] _spawnPositions;

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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakePrize(GameObject prizeTaken)
    {
        RemoveBanana();
        
        Destroy(prizeTaken);
    }

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

    private void GameOver()
    {
        Debug.Log("Game Over");
    }
}
