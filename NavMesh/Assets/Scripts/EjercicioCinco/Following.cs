using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Following : MonoBehaviour
{
    [SerializeField] private GameObject _targetToFollow;
    private NavMeshAgent _agent;

    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.destination = _targetToFollow.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        _agent.destination = _targetToFollow.transform.position;

        float distanceToTarget = Vector3.Distance(_agent.transform.position, _targetToFollow.transform.position);
        if (distanceToTarget <= .5)
        {
            Destroy(_targetToFollow);
        }

    }

    public void Init(GameObject target)
    {
        _targetToFollow = target;
    }
}
