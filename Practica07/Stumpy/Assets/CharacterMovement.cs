using System;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float speed = 5.0f; // La velocidad a la que se va a mover el personaje
    public float turnSpeed = 300.0f; // La velocidad de giro

    private static int _walkingSpeed = Animator.StringToHash("speed");
    private Animator _animatorController;
    private CharacterController _characterController;

    private static int _croachingSpeed = Animator.StringToHash("croachingSpeed");
    private bool _isCroaching;
    private float _croachingTime; 
    private float _croachingCoef;
    private bool _croached;

    [SerializeField] private Door _door;

    private static int _isScared = Animator.StringToHash("isScared");
    private LayerMask _mask;
    [SerializeField] private Transform _targetingOrigin;
    private bool _wasScared;

    // Start is called before the first frame update
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _animatorController = GetComponent<Animator>();

        _mask = LayerMask.GetMask("Enemy", "Obstacle");
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log("Trying to open the door");
            _door.OpenDoor();
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if (_croached)
            {
                Uncroach();
            }
            else
            {
                Croach();
            }

        }

        if (!_isCroaching)
        {
            // Rotamos en el eje Y
            transform.Rotate(0, horizontal * turnSpeed * Time.deltaTime, 0);
            Vector3 move = transform.forward * vertical;
            _characterController.Move(move * speed * Time.deltaTime);
            _animatorController.SetFloat(_walkingSpeed, System.Math.Abs(vertical));
        }
        else
        {
            _croachingTime += Time.deltaTime * _croachingCoef;

            _animatorController.SetFloat(_croachingSpeed, _croachingTime);
            if (_croachingTime > 1)
            {
                _croached = true;
            }
            if (_croachingTime < 0)
            {
                _croached = false;
                _isCroaching = false;
            }

        }

        //Enemy detection
        RaycastHit hit;
        if (Physics.Raycast(_targetingOrigin.position, transform.forward, out hit, 300f, _mask))
        {
            if (hit.collider.CompareTag("Enemy") && !_wasScared)
            {
                _wasScared = true;
                _animatorController.SetTrigger(_isScared);
            }
            //Debug.DrawRay(_targetingOrigin.position, transform.forward * 300f, Color.red);
            //Debug.Log(hit.collider.name);
        }
        //}

    }

    private void Uncroach()
    {
        _croachingTime = 1f;
        _croachingCoef = -.5f;
    }

    private void Croach()
    {
        _animatorController.SetFloat(_walkingSpeed, 0);
        _croachingTime = .1f;
        _croachingCoef = 2f;
        _isCroaching = true;
    }



}


