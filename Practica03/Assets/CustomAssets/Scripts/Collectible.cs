using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class Collectible : MonoBehaviour
{
    [SerializeField] private AudioClip _collectibleTaken;
    [SerializeField] private CollectibleStash _collectibleStash;
    private ParticleSystem _particleOnDestroy;

    void Start()
    {
        _particleOnDestroy = GetComponent<ParticleSystem>();   
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            AudioManager.Instance.PlayEffect(_collectibleTaken);
            _collectibleStash.NumberOfItems++;
            _particleOnDestroy.Play();
            Invoke("Destroy", _particleOnDestroy.main.duration);
            //Destroy(gameObject, _particleOnDestroy.main.duration);
        }

    }
}
