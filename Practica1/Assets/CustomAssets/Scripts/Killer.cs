using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killer : MonoBehaviour
{
    [SerializeField] private AudioClip _killSound;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            AudioManager.Instance.PlayEffect(_killSound);
            Destroy(collision.gameObject);
            GameManager.Instance.HandleDefeat();
        }
    }
}
