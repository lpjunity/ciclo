using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stalker : MonoBehaviour
{
    [SerializeField] private RangeDetection _followRange;
    [SerializeField] private float _movementSpeed;
    private GameObject _player;
    private SpriteRenderer _sprite;
    private Rigidbody2D _rb;


    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _sprite = GetComponent<SpriteRenderer>();
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_followRange.InRange)
        {
            Vector3 positionToMoveTo =  Vector3.Slerp(transform.position, _player.transform.position, Time.deltaTime * _movementSpeed);

            if (_sprite)
            {
                _sprite.flipX = positionToMoveTo.x > transform.position.x;
            }

            _rb.MovePosition(positionToMoveTo);

        }
    }
}
