using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class CreacionObstaculos : MonoBehaviour
{
    private NavMeshAgent _agent;
    private bool _wantToMove;
    private RaycastHit _hit;
    private LayerMask _mask;
    [SerializeField] private float _rayDistance;
    [SerializeField] private BoxCollider _obstaclePrefab;
    private bool _wantToBuild;

    // Start is called before the first frame update
    void Start()
    {
        _agent = GameObject.FindGameObjectWithTag("Player")?.GetComponent<NavMeshAgent>();
        _mask = ~(1 << LayerMask.NameToLayer("Ground"));
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out _hit, _rayDistance))
        {
            Debug.DrawRay(ray.origin, ray.direction * _rayDistance, Color.red);

            bool elementInPath = Physics.CheckBox(_hit.point, _obstaclePrefab.size / 2, _obstaclePrefab.transform.rotation, _mask);

            /* TODO: Lanzar un rayo, e instanciar un obstáculo en el punto donde se golpee */
            /* Hay que reconstruir la superficie, busca un método que se encargue de ello */
            //The prefab already has de Carve checkbox active.
            if (Input.GetMouseButtonDown(0))
            {
                if (!elementInPath)
                {
                    _wantToBuild = true;
                }
            }
        }

    }

    private void FixedUpdate()
    {
        if (_wantToBuild)
        {
            Instantiate(_obstaclePrefab.gameObject, _hit.point, _obstaclePrefab.transform.rotation);
            _wantToBuild = false;
        }
        if (_wantToMove)
        {
            Debug.Log($"Hit point: {_hit.point}");
            _agent.destination = _hit.point;
            _wantToMove = false;
        }
    }

}
