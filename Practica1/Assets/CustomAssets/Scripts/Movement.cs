using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float _movementSpeed;
    private bool _isBlocked = false;

    private Rigidbody2D _rb;
    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_isBlocked) return;

        float xInput = Input.GetAxis("Horizontal");
        float yInput = Input.GetAxis("Vertical");

        Vector2 force = new Vector2(xInput * _movementSpeed * Time.deltaTime, yInput * _movementSpeed * Time.deltaTime);

        //transform.Translate(xInput * _movementSpeed * Time.deltaTime, yInput * _movementSpeed * Time.deltaTime, 0);
        _rb.AddForce(force, ForceMode2D.Force);
    }

    public void BlockMovement(float seconds = 2f)
    {
        _isBlocked = true;
        Invoke("AllowMovement", seconds);
    }

    private void AllowMovement()
    {
        _isBlocked = false;
        _spriteRenderer.color = Color.white;
    }
}