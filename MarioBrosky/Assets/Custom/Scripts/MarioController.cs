using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarioController : MonoBehaviour
{

    private Rigidbody2D _rb;
    private bool _isJumping;
    private Vector2 _movementPosition;
    [SerializeField] private Vector2 _startingPosition;
    [SerializeField] private float _jumpSpeed;
    [SerializeField] private float _movementSpeed;
    private float _inputHorizontal;

    private SpriteRenderer _spriteRendered;
    private Animator _animator;

    private static int _walkingParameter = Animator.StringToHash("MovingOnX");
    private static int _jumpingParameter = Animator.StringToHash("IsJumping");
    private static int _dyingParameter = Animator.StringToHash("IsDying");
    private float _direction;
    private float _maxHeight;
    [SerializeField] private float _jumpDistance;
    private bool _headbutt;
    private bool _isFalling;
    private bool _isBigMario;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _spriteRendered = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _movementPosition = _startingPosition; 
    }

    // Update is called once per frame
    void Update()
    {
        _inputHorizontal = Input.GetAxis("Horizontal");
        _animator.SetBool(_walkingParameter, _inputHorizontal != 0);

        if (_inputHorizontal < 0)
        {
            _spriteRendered.flipX = true;
        }

        if(_inputHorizontal > 0)
        {
            _spriteRendered.flipX = false;
        }

        if (Input.GetKeyDown(KeyCode.Space) && !_isJumping)
        {
            _isJumping = true;
            AudioManager.Instance.PlayJump();
            _animator.SetBool(_jumpingParameter, true);
            _maxHeight = transform.position.y + _jumpDistance;
        }
        _movementPosition.x += _inputHorizontal * _movementSpeed * Time.deltaTime;
    }

    private void FixedUpdate()
    {
        if(_isJumping || _isFalling) {
            if (_isJumping)
            {
                _direction = 1;
                if (_movementPosition.y >= _maxHeight || _headbutt)
                {
                    _isJumping = false;
                    _isFalling = true;
                }
            }
            if (_isFalling)
            {
                _direction = -1;
            }
            _movementPosition.y += (_direction * _jumpSpeed) * Time.deltaTime;
        }
        _rb.MovePosition(_movementPosition);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            if(_isJumping && _direction > 0)
            {
                _headbutt = true;
            }
            else
            {
                _headbutt = false;
                _isFalling = false;
                _animator.SetBool(_jumpingParameter, false);
            }
        }

        if (collision.gameObject.CompareTag("Mushroom"))
        {
            Grow();
            AudioManager.Instance.PlayMushroomPickup();
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (_isFalling) {
                //Debug.Log("EnemyKilled");
                collision.gameObject.GetComponent<Gumba>().Die();
            }
            else
            {
                if (_isBigMario)
                {
                    Shrink();
                }
                else
                {
                    Die();
                }
                
            }
        }
    }

    private void Shrink()
    {
        _animator.SetLayerWeight(1, 0f);
        _isBigMario = false;
    }

    private void Grow()
    {
        _animator.SetLayerWeight(1, 1f);
        _isBigMario = true;
    }

    private void Die()
    {
        _animator.SetBool(_dyingParameter, true);
    }

    public void DestroyMyself()
    {
        Destroy(gameObject);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") && !_isJumping)
        {
            _isFalling = true;
        }
    }

}
