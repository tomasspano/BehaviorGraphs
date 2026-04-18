using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    
    [Header("Movement")] 
    [SerializeField] private float movementSpeed;
    private float rotationVelocity;
    private float rotationSmoothFactor = 0.3f;

    private CharacterController controller;

    private Vector2 inputVector; 
    private Vector3 horizontalMovement; 

    private PlayerInput input;
    private Camera cam;
    private Animator anim;
    [SerializeField] private float movementSmoothFactor;
    private float currentSpeed;
    private float targetSpeed;
    private float speedVelocity;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        cam = Camera.main;
        input = GetComponent<PlayerInput>();
        anim = GetComponentInChildren<Animator>();
        //bloquea el mouse y lo esconde
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnEnable()
    {
        //.started: cuando empieza a hacerse la acción
        //.performed: se dispara cuando hay cambios de valor
        //.canceled: cuando esa acción se cancela
        input.actions["Move"].performed += UpdateMovement;
        input.actions["Move"].canceled += UpdateMovement;
    }

    private void UpdateMovement(InputAction.CallbackContext obj)
    {
        inputVector = obj.ReadValue<Vector2>();
    }

    private void OnDisable()
    {
        input.actions["Move"].performed -= UpdateMovement;
        input.actions["Move"].canceled -= UpdateMovement;
    }
    void Update()
    {
        MoveAndRotate();
    }

    private void MoveAndRotate()
    {
        //calcula la velocidad objetivo para teclado y joystick
        targetSpeed = movementSpeed * inputVector.magnitude;
        currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedVelocity, movementSmoothFactor);
        
        if (inputVector.sqrMagnitude > 0) //el jugador se mueve por input
        {
            float angleToRotate = Mathf.Atan2(inputVector.x, inputVector.y) * Mathf.Rad2Deg + cam.transform.eulerAngles.y;
            //multiplicar un cuaternión por un vector es rotar el vector
            horizontalMovement = (Quaternion.Euler(0, angleToRotate, 0) * Vector3.forward) * movementSpeed;

            float smoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, angleToRotate,
                ref rotationVelocity,
                rotationSmoothFactor);
            
            transform.rotation = Quaternion.Euler(0, smoothAngle, 0);
        }
        else horizontalMovement = Vector3.zero;

        anim.SetFloat("Blend", currentSpeed / movementSpeed);
        controller.Move(horizontalMovement * Time.deltaTime);
    }

}