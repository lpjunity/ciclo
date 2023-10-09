using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScale : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.transform.localScale = new Vector3(Random.Range(0.5f, 3f), Random.Range(0.5f, 3f), transform.localScale.z);
        }
    }

}
