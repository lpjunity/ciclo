using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateArround : MonoBehaviour
{
    [SerializeField] private GameObject _pivotPoint;
    [SerializeField] private float _rotationAngle;

    // Start is called before the first frame update
    /*void Start()
    {

    }*/

    // Update is called once per frame
    void Update()
    {
        //Rotate arround is obsolete
         
        transform.RotateAround(_pivotPoint.transform.position, Vector3.up, _rotationAngle * Time.deltaTime);
    }
}
