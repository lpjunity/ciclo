using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MouseChecker : MonoBehaviour
{
    private GameObject _player;
    private Rigidbody _rb;
    private Quaternion _targetRotation;
    [SerializeField] private float _rotationCoef;
    [SerializeField] private Animator _animator;

    private GameObject[] _interactables;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _rb = _player.GetComponent<Rigidbody>();
        _interactables = GameObject.FindGameObjectsWithTag("Interactable");
    }

    // Update is called once per frame
    void Update()
    {
        //if(Input.GetMouseButtonDown(0))
        //{
        RaycastHit hit;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            //Debug.Log(hit.point.ToString());
            Debug.DrawRay(ray.origin, ray.direction * 200f, Color.red);
            Vector3 direction = new Vector3(hit.point.x, _player.transform.position.y, hit.point.z) - _player.transform.position;
            Quaternion rotation = Quaternion.LookRotation(direction.normalized, Vector3.up);

            _targetRotation = Quaternion.Lerp(_rb.rotation, rotation, _rotationCoef * Time.deltaTime);

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                _player.transform.position = new Vector3(hit.point.x, _player.transform.position.y, hit.point.z);
            }

            if(Input.GetKeyDown(KeyCode.Mouse1))
            {
                if (hit.collider.gameObject.CompareTag("Interactable"))
                {
                    hit.collider.gameObject.GetComponent<Renderer>().material.color = Color.red;
                    if (CheckOtherHats())
                    {
                        _animator.SetBool("isDoorOpen", true);
                    }
                }
            }

        } 
        //}

    }

    private bool CheckOtherHats()
    {
        bool allHatsActive = true;
        foreach(var interactive in _interactables)
        {
            allHatsActive = allHatsActive && interactive.GetComponent<Renderer>().material.color == Color.red;
        }
        return allHatsActive;
    }

    void FixedUpdate()
    {
        _rb.MoveRotation(_targetRotation.normalized);
    }

    private void OnDrawGizmos()
    {
        //Gizmos.DrawSphere(Camera.main.transform.position, .1f);

    }
}
