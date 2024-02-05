using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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
        if (_targetToFollow)
        {
            _agent.destination = _targetToFollow.transform.position;
            float distanceToTarget = Vector3.Distance(_agent.transform.position, _targetToFollow.transform.position);
            if (distanceToTarget <= .5)
            {
                GameManager.Instance.TakePrize(_targetToFollow);
            }
        }
        else
        {
            _agent.destination = gameObject.transform.position;
        }

    }

    public void Init(GameObject target)
    {
        _targetToFollow = target;
    }

    public void ChangeTarget(GameObject newTarget)
    {
        _targetToFollow = newTarget;
    }
}
