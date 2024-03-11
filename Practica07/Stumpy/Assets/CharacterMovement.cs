using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float speed = 5.0f; // La velocidad a la que se va a mover el personaje
    public float turnSpeed = 300.0f; // La velocidad de giro

    private static int _walkingSpeed = Animator.StringToHash("Velocidad");
    private Animator _animatorController;
    private CharacterController _characterController;

    // Start is called before the first frame update
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _animatorController = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Rotamos en el eje Y
        transform.Rotate(0, horizontal * turnSpeed * Time.deltaTime, 0);
        Vector3 move = transform.forward * vertical;
        _characterController.Move(move * speed * Time.deltaTime);

        _animatorController.SetFloat(_walkingSpeed, System.Math.Abs(vertical));
    }
}
