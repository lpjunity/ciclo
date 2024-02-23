using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gumba : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Vector2 _movementPosition;
    private float _direction;
    [SerializeField] private Vector2 _startingPosition;
    [SerializeField] private float _movementSpeed;

    private Animator _animator;
    private static int _isDyingParameter = Animator.StringToHash("IsDying");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        _direction = 1;
    }

    // Start is called before the first frame update
    void Start()
    {
        _movementPosition = _startingPosition;
    }

    // Update is called once per frame
    void Update()
    {
        _movementPosition.x += _direction * _movementSpeed * Time.deltaTime;
    }

    private void FixedUpdate()
    {
        _rb.MovePosition(_movementPosition);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _direction = - _direction;
        }
    }

    public void Die()
    {
        _animator.SetBool(_isDyingParameter, true);
    }

    public void DestroyMyself()
    {
        Destroy(gameObject);
    }

}
