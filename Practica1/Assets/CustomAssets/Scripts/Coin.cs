using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private AudioClip _takeCoin;

    /*private void Start()
    {
        

    }*/

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            AudioManager.Instance.PlayEffect(_takeCoin);
            Inventory playerInventory  = collision.gameObject.GetComponent<Inventory>();
            playerInventory.AddCoin();
            Destroy(gameObject);
        }
    }

}
