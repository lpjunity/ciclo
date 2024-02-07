using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prize : MonoBehaviour
{
    private event Action<GameObject> OnPrizeAdded;
    private event Action<GameObject, GameObject> OnPrizeConsumed;

    // Start is called before the first frame update
    void Start()
    {
        OnPrizeConsumed += (GameObject prize) => { GameManager.Instance.AddPrize(prize); };
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Monkey"))
        {
            OnMangoConsumed?.Invoke(gameObject, other.gameObject);
        }
    }
}
