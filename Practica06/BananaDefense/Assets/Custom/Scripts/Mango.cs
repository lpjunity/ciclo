using System;
using UnityEngine;

public class Mango : MonoBehaviour
{
    private event Action<GameObject> OnMangoAdded;
    private event Action<GameObject, GameObject> OnMangoConsumed;

    // Start is called before the first frame update
    void Start()
    {
        OnMangoAdded += (GameObject mango) => { GameManager.Instance.AddMango(mango); };
        OnMangoConsumed += (GameObject mango, GameObject monkey) => { GameManager.Instance.RemoveMango(mango, monkey); };
    }

    // Update is called once per frame
    /*void Update()
    {
        
    }*/

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Buildeable"))
        {
            OnMangoAdded?.Invoke(gameObject);
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Monkey"))
        {
            OnMangoConsumed?.Invoke(gameObject, other.gameObject);
        }   
    }

}
