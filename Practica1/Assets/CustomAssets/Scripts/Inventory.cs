using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private CoinPurse _coinPurse;
    [SerializeField] private CoinsUpdater _coinsUpdater;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    /*void Update()
    {
        
    }*/

    public void AddCoin()
    {
        _coinPurse.NumberOfCoins++;
        _coinsUpdater.UpdateNumberOfCoins();
    }

    public int GetNumberOfCoins() 
    {
        return _coinPurse.NumberOfCoins;
    }

    private void OnDisable()
    {
        _coinPurse.NumberOfCoins = 0;
    }
}
