using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowBehaviour : MonoBehaviour
{
    private GameObject _player;
    private Collider[] _colliders;
    [SerializeField] private float _movementSpeed;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

        //transform.LookAt(_player.transform.position);
        //transform.position = Vector3.MoveTowards(transform.position, _player.transform.position, _movementSpeed / 100);

        //Fire a ray
        //Get position from ray

        LayerMask layerMask = LayerMask.GetMask("Player");
        _colliders = Physics.OverlapSphere(transform.position, 20f, layerMask);
        
        foreach(var collider in _colliders)
        {
            if (collider.CompareTag("Player"))
            {
                transform.LookAt(_player.transform.position);
                transform.position = Vector3.MoveTowards(transform.position, _player.transform.position, _movementSpeed / 100);
            }
        }

    }
}
