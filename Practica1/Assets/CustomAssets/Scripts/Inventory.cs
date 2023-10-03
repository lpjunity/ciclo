using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private CoinPurse _coinPurse;

    // Start is called before the first frame update
    /*void Start()
    {
        
    }

    // Update is called once per frame
    /*void Update()
    {
        
    }*/
    
    public void AddCoin()
    {
        _coinPurse.NumberOfCoins++;
        GameManager.Instance.UpdateNumberOfCoins();
    }

    public int GetNumberOfCoins() 
    {
        return _coinPurse.NumberOfCoins;
    }

    public void OnDisable()
    {
        _coinPurse.NumberOfCoins = 0;
    }
}
