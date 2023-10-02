using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FinishCondition : MonoBehaviour
{
    [SerializeField] private GameObject _victoryPanel;
    [SerializeField] private TextMeshProUGUI _completeVictory;
    [SerializeField] private TextMeshProUGUI _victory;
    [SerializeField] private TextMeshProUGUI _sadVictory;

    private int _coinsInLevel;

    private Timer _timer;

    // Start is called before the first frame update
    void Start()
    {
        _coinsInLevel = GameObject.FindGameObjectsWithTag("Coin").Length;    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
           GameObject player = collision.gameObject;
           Inventory playerInventory = player.GetComponent<Inventory>();
           
            if (playerInventory.GetNumberOfCoins() == _coinsInLevel) {
                //TODO: Max number of coins
                Instantiate(_completeVictory, _victoryPanel.transform);
            }
            else if(playerInventory.GetNumberOfCoins() == 0)
            {
                //TODO: No coins
                Instantiate(_sadVictory, _victoryPanel.transform);
            }
            else
            {
                //TODO: Any coin
                Instantiate(_victory, _victoryPanel.transform);
            }
        }
        Time.timeScale = 0;
        _victoryPanel.SetActive(true);
    }
}
