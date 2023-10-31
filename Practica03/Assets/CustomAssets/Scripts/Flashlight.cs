using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class Flashlight : MonoBehaviour
{
    private Light _light;

    // Start is called before the first frame update
    void Start()
    {
        _light = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            _light.enabled = !_light.enabled;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Vector2 fuerzo = collision.transform.position;


    }



}
