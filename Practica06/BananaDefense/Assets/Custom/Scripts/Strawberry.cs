using System;
using UnityEngine;

public class Strawberry : MonoBehaviour
{
    private event Action<GameObject> OnStrawberryAdded;
    private event Action<GameObject, GameObject> OnStrawberryConsumed;

    // Start is called before the first frame update
    void Start()
    {
        OnStrawberryAdded += (GameObject strawberry) => { GameManager.Instance.AddStrawberry(strawberry); };
        OnStrawberryConsumed += (GameObject strawberry, GameObject monkey) => { GameManager.Instance.RemoveStrawberry(strawberry, monkey); };
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Buildeable"))
        {
            OnStrawberryAdded?.Invoke(gameObject);
        }

        if (collision.gameObject.CompareTag("Monkey"))
        {
            OnStrawberryConsumed?.Invoke(gameObject, collision.gameObject);
        }
    }
}
