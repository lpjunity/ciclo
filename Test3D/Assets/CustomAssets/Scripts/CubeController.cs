using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{
    private Rigidbody _rb;
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _jumpForce;

    private bool _isGrounded = true;
    private bool _wantsToJump;
    private float posX, posZ;

    [SerializeField] private float _rotationSpeed;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        posX = Input.GetAxis("Horizontal");
        posZ = Input.GetAxis("Vertical");

        if(_isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            _wantsToJump = true;
        }
        
    }

    private void FixedUpdate()
    {
        RotateBody();

        MoveBody();

    }

    private void RotateBody()
    {
        Quaternion targetRotation = transform.rotation;
        if (posX > 0)
        {
            targetRotation = Quaternion.LookRotation(Vector3.right);
        }
        else if (posX < 0)
        {
            targetRotation = Quaternion.LookRotation(Vector3.left);
        }
        else if (posZ < 0)
        {
            targetRotation = Quaternion.LookRotation(Vector3.back);
        }
        else
        {
            targetRotation = Quaternion.LookRotation(Vector3.forward);
        }

        transform.rotation = Quaternion.Slerp(_rb.rotation, targetRotation, _rotationSpeed * Time.fixedDeltaTime);
    }

    private void MoveBody()
    {
        float fwdSpeed = posZ * _movementSpeed * Time.fixedDeltaTime;

        Vector3 velocity = new Vector3(_rb.velocity.x, _rb.velocity.y, _rb.velocity.z);

        if (Mathf.Abs(transform.forward.x) > 0)
        {
            //Moving left or right
            velocity = new Vector3(fwdSpeed, _rb.velocity.y, _rb.velocity.z);
            Debug.Log($"Moving left or right: {velocity}");
        }
        else if(Mathf.Abs(transform.forward.z) > 0){
            //Moving forward or backwards
            velocity = new Vector3(_rb.velocity.x, _rb.velocity.y, fwdSpeed);
            Debug.Log($"Moving fwd or backwards: {velocity}");
        }

        _rb.velocity = velocity;

        Debug.Log(transform.forward * fwdSpeed);
        Debug.Log(velocity);

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
