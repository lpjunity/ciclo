using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float _rayDistance;
    private Camera _mainCamera;

    private bool _dragging;
    private Vector3 _draggingStart;
    private Vector3 _draggingCurrent;
    private Vector3 _heading;
    /*https://docs.unity3d.com/2019.3/Documentation/Manual/DirectionDistanceFromOneObjectToAnother.html*/
    //private float _draggingDistance;
    //private Vector3 _draggingDirectionNorm;

    void Update()
    {
        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, _rayDistance))
        {
            Debug.DrawRay(ray.origin, ray.direction * _rayDistance, Color.red);
            //Debug.Log(hit.transform.name);
            //Debug.Log($"hit: {hit.point}");

            _draggingCurrent = hit.point;

            if (Input.GetMouseButtonDown(0))
            {
                _dragging = true;
                _draggingStart = hit.point;
            }
            
        }
        
        if (Input.GetMouseButtonUp(0))
        {
            _dragging = false;
        }


    }

    private void FixedUpdate()
    {
        if (_dragging)
        {
            _heading = _draggingStart - _draggingCurrent;
            //_draggingDistance = _heading.magnitude;
            //_draggingDirectionNorm = _heading / _draggingDistance;
            //Debug.Log($"Heading to: {_heading}, Distance traveled: {_draggingDistance}, Direction normalized: {_draggingDirectionNorm}");

            _mainCamera.transform.Translate(_heading * Time.fixedDeltaTime);
        }
    }


    private void OnEnable()
    {
        _mainCamera = Camera.main;
        GameManager.Instance.ActivateCameraMode();
        _dragging = false;
    }

    private void OnDisable()
    {
        GameManager.Instance.DeactivateModes();
        _dragging = false;
    }

}
