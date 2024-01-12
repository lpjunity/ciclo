using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ComportamientoCamara : MonoBehaviour
{
    private NavMeshAgent _agent;
    private bool _wantToMove;
    private RaycastHit _hit;
    private LayerMask _mask;
    [SerializeField] private float _rayDistance;
    [SerializeField] private BoxCollider _obstaclePrefab;
    //private bool _wantToBuild;

    void Start()
    {
        _agent = GameObject.FindGameObjectWithTag("Player")?.GetComponent<NavMeshAgent>();
        _mask = ~(1 << LayerMask.NameToLayer("Ground"));
    }

    // Update is called once per frame
    void Update()
    {
        /* TODO: Lanzar un rayo al pulsar el botón, y modificar el destino de nuestro agente */
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out _hit, _rayDistance))
        {
            Debug.DrawRay(ray.origin, ray.direction * _rayDistance, Color.red);
            
            bool elementInPath = Physics.CheckBox(_hit.point, _obstaclePrefab.size / 2, _obstaclePrefab.transform.rotation, _mask);

            if (Input.GetMouseButtonDown(0))
            {
                _wantToMove = true;
            }
            /*if(Input.GetMouseButtonDown(1))
            {
                if (!elementInPath)
                {
                    _wantToBuild = true; 
                }
            }*/
        }
    }

    private void FixedUpdate()
    {
        /*if(_wantToBuild)
        {
            Instantiate(_obstaclePrefab.gameObject, _hit.point, _obstaclePrefab.transform.rotation);
            _wantToBuild = false;
        }*/
        if (_wantToMove)
        {
            Debug.Log($"Hit point: {_hit.point}");
            _agent.destination = _hit.point;
            _wantToMove = false;
        }
    }
}
