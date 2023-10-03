using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTransform : MonoBehaviour
{
    [SerializeField] private Transform _followTarget;


    // Update is called once per frame
    void Update()
    {
        if (_followTarget)
        {
            Vector3 position = new Vector3(_followTarget.position.x, _followTarget.position.y, -1);
            transform.SetPositionAndRotation(position, _followTarget.rotation);
        }
    }
}
