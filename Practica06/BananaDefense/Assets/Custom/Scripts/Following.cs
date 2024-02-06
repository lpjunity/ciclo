using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class Following : MonoBehaviour
{
    [SerializeField] private Transform _leavePosition;
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
        /*if (_targetToFollow)
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
        }*/

    }

    public void Init(GameObject target, Transform leaveArea)
    {
        _targetToFollow = target;
        _leavePosition = leaveArea;
        GameManager.OnStrawberryOnMap += ChangeTarget;
        GameManager.OnStrawberryShortage += ChangeToPrize;
    }

    private void ChangeToPrize(GameObject prize)
    {
        _targetToFollow = prize;
        _agent.destination = prize.transform.position;
    }

    private void ChangeTarget(List<GameObject> possibleTargets)
    {
        float distanceToTarget = 1000f;
        GameObject newTarget = null;
        foreach (GameObject target in possibleTargets)
        {
            float tmpDistance = Vector3.Distance(transform.position, target.transform.position);
            if (tmpDistance <= distanceToTarget)
            {
                distanceToTarget = tmpDistance;
                newTarget = target;
            }
        }

        _targetToFollow = newTarget;
        _agent.destination = newTarget.transform.position;
    }

    public void Leave()
    {
        _targetToFollow = null;
        _agent.destination = _leavePosition.position;
        GameManager.OnStrawberryOnMap -= ChangeTarget;
        GameManager.OnStrawberryShortage -= ChangeToPrize;
    }
}
