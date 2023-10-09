using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraverseRepeat : MonoBehaviour
{
    [SerializeField] Transform _beginning;
    [SerializeField] Transform _ending;

    private Rigidbody2D _rb;
    private Vector3 _positionLerped;

    private float _journeyTime = 1f;
    private float _timePassed;
    private float _lerpedPercentage;
    private bool _isMovingRight;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _isMovingRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        _timePassed += Time.deltaTime;
        _lerpedPercentage = _timePassed / _journeyTime;

        if (_isMovingRight)
        {
            _positionLerped = Vector3.Slerp(_beginning.position, _ending.position, _lerpedPercentage);
        }
        else
        {
            _positionLerped = Vector3.Slerp(_ending.position, _beginning.position, _lerpedPercentage);
        }
        ChangeDirectionIfReachedEnd();

        _rb.MovePosition(_positionLerped);

    }

    private void ChangeDirectionIfReachedEnd()
    {
        if (_lerpedPercentage >= 1f)
        {
            _isMovingRight = !_isMovingRight;
            _timePassed = 0;
        }
    }
}
