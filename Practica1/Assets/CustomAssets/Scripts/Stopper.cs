using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stopper : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) 
        {
            GameObject player = collision.gameObject;
            SpriteRenderer playerSprite = player.GetComponent<SpriteRenderer>();
            playerSprite.color = Color.red;
            player.gameObject.GetComponent<Movement>().BlockMovement();
        }
    }
}
