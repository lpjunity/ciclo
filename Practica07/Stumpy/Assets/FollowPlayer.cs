using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Animations;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class FollowPlayer : MonoBehaviour
{
    private LayerMask _mask;
    [SerializeField] private Transform _targetingOrigin;
    private static int _isFollowing = Animator.StringToHash("isFollowing");
    [SerializeField] private Animator _animatorController;
    private Rigidbody _rb;
    // Start is called before the first frame update

    private void Awake()
    {
        //_animatorController = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();
        _mask = LayerMask.GetMask("Player", "Obstacle");
    }

    /*void Start()
    {
        
    }*/

    // Update is called once per frame
    void Update()
    {

        //Player detection
        RaycastHit hit;
        if (Physics.Raycast(_targetingOrigin.position, transform.forward, out hit, 300f, _mask))
        {
            if (hit.collider.CompareTag("Player"))
            {
                _animatorController.SetBool(_isFollowing, true);
                Vector3 position = hit.collider.gameObject.transform.position;
                position.y = 0.219f;
                Debug.Log(position);
                transform.LookAt(position);

                //Vector3 dir = (hit.collider.transform.position - transform.position).normalized;
                //_animatorController.rootRotation = Quaternion.LookRotation(dir);
                //_rb.MoveRotation(hit.collider.transform);

            }
            Debug.DrawRay(_targetingOrigin.position, transform.forward * 300f, Color.red);
            Debug.Log(hit.collider.name);
        }
        //}
    }
}
