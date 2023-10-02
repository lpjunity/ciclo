using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinsUpdater : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _numberOfCoins;
    [SerializeField] CoinPurse _playerCoinPurse;

    private void Start()
    {
        UpdateNumberOfCoins();
    }

    public void UpdateNumberOfCoins()
    {
        _numberOfCoins.text = _playerCoinPurse.NumberOfCoins.ToString();
    }
}
