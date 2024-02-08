using UnityEngine;

public class Prize : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    /*void Update()
    {

    }*/


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Monkey"))
        {
            GameManager.Instance.ConsumePrize(gameObject, other.gameObject);
        }
    }
}
