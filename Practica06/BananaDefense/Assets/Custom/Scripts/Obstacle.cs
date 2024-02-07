using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : Building
{
    [SerializeField] private float _timeToLive;


    // Start is called before the first frame update
    void Start()
    {
        Invoke("Disappear", _timeToLive);
    }

    private void Disappear()
    {
        Destroy(gameObject);
    }

}
