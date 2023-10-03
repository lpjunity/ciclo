using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private AudioClip _coinTaken;

    // Start is called before the first frame update
    /*void Start()
    {

    }*/

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            AudioManager.Instance.PlayEffect(_coinTaken);
            Inventory playerInventory  = collision.gameObject.GetComponent<Inventory>();
            playerInventory.AddCoin();   
            Destroy(gameObject);
        }

    }

}
