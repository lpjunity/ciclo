using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyElement : MonoBehaviour
{
    [SerializeField] private GameObject _elementToDestroy;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(_elementToDestroy);
        }
    }

}
