using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMove : MonoBehaviour
{
    private Rigidbody _rb;
    [SerializeField] private float _fwdForce;
    [SerializeField] private float _rotationCoef;

    [SerializeField] private float _jumpForce;
    private bool _isGrounded = true;
    private bool _wantsToJump;

    private float _inputX;
    private float _inputZ;
    private Quaternion _targetRotation;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _inputX = Input.GetAxis("Horizontal");
        _inputZ = Input.GetAxis("Vertical");

        _targetRotation = transform.rotation;

        if(_inputX > 0)
        {
            _targetRotation = Quaternion.LookRotation(transform.right);
        }else if (_inputX < 0)
        {
            _targetRotation = Quaternion.LookRotation(transform.right * -1);
        }else if (_inputZ < 0)
        {
            _targetRotation = Quaternion.LookRotation(transform.right * -1);
        }else if(_inputZ > 0)
        {
            _targetRotation = Quaternion.LookRotation(transform.forward);
        }

        _targetRotation = Quaternion.Slerp(_rb.rotation, _targetRotation, Time.deltaTime * _rotationCoef);

        if (_isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            _wantsToJump = true;
        }
    }

    private void FixedUpdate()
    {
        Vector3 force = transform.forward * _inputZ * _fwdForce;

        if(_inputZ > 0)
        {
            Debug.Log($"Force applied: {force}");
            _rb.AddForce(force, ForceMode.Force);
        }

        _rb.MoveRotation(_targetRotation.normalized);
        
        if (_wantsToJump)
        {
            _rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
            _wantsToJump = false;
            _isGrounded = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            _isGrounded = true;
        }
    }
}
