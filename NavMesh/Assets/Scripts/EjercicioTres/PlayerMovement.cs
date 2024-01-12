using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float _inputX;
    private float _inputZ;
    private Rigidbody _rb;

    public float _speed;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        _inputX = Input.GetAxis("Horizontal");
        _inputZ = Input.GetAxis("Vertical");
        Vector3 force = new Vector3(_inputX, 0, _inputZ);

        _rb.AddForce(force * _speed, ForceMode.Force);

    }

    private void OnCollisionEnter(Collision collision)
    {
        /*if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
            SpawnManager.Instance.RegisterMonsterKilled();
        }*/
    }
}
