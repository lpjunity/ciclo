using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefeatOnCollision : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //collision.gameObject.SetActive(false);
            GameManager.Instance.HandleDefeat();
            //Destroy(_player);
        }
    }
}
