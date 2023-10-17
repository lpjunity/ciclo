using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private AudioClip _coinTaken;
    [SerializeField] private CoinPurse _coinPurse;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            AudioManager.Instance.PlayEffect(_coinTaken);
            _coinPurse.NumberOfCoins++;
            Destroy(gameObject);
        }

    }
}
