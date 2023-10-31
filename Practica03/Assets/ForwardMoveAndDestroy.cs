using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForwardMoveAndDestroy : MonoBehaviour
{
    private float _timePassed;
    private float _lifeTime;
    private Rigidbody _rb;

    // Start is called before the first frame update
    void Start()
    {
        _lifeTime = 15f;
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        _timePassed += Time.deltaTime;
        _rb.AddForce(transform.forward, ForceMode.Impulse);
        if(_timePassed > _lifeTime)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject, .1f);
        }
        Destroy(gameObject, .1f);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Coin"))
        {
            Destroy(collider.gameObject, .1f);
        }
        Destroy(gameObject, .1f);
    }

}
