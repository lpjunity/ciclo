using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDispenser : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPrefab;

    // Start is called before the first frame update
    /*void Start()
    {
        
    }*/

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //Best to use pooling but...
            Instantiate(_bulletPrefab, transform);
        }


    }
}
