using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement velocity")]
    [SerializeField] private float movementVelocity = 5.0f;
    [SerializeField] private float walkVelocity = 5.0f;
    [SerializeField] private float crouchVelocity = 2.0f;
    
    [Header("Maximum velocities")]
    [SerializeField] private float maxMovementVelocity = 5.0f;
    [SerializeField] private float maxCrouchVelocity = 5.0f;
    [SerializeField] private float maxWalkVelocity = 5.0f;
    [SerializeField] private float maxSprintVelocity = 10.0f;
    [SerializeField] private Transform cameraTransform;
    
    private PlayerInput _inputActions;
    private Rigidbody _rb;
    private Vector2 _moveInput;
    private Camera _mainCamera;
    
    private void Awake()
    {
        _inputActions = new PlayerInput();
        _inputActions.Enable();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _mainCamera = Camera.main;
    }
    
    private void FixedUpdate()
    {
        SetMovementValues();
        MoveAndRotate();

        _rb.linearVelocity = Vector3.ClampMagnitude(_rb.linearVelocity, maxMovementVelocity);
    }

    private void SetMovementValues()
    {
        // Set current movement velocity and maxVelocity depending on if the player is crouching, walking, or sprinting
        // Check if crouching
        if (Mathf.Approximately(_inputActions.Player.Crouch.ReadValue<float>(), 1.0f))
        {
            movementVelocity = crouchVelocity;
            maxMovementVelocity = maxCrouchVelocity;
        }
        // Check if sprinting
        else if (Mathf.Approximately(_inputActions.Player.Sprint.ReadValue<float>(), 1.0f))
        {
            maxMovementVelocity = maxSprintVelocity;
        }
        // We are walking
        else
        {
            movementVelocity = walkVelocity;
            maxMovementVelocity = maxWalkVelocity;
        }
    }

    private void MoveAndRotate()
    {
        // Move the player relative to the camera's rotation and clamp the movement velocity
        Vector3 move = cameraTransform.forward * _moveInput.y + cameraTransform.right * _moveInput.x;
        _moveInput = _inputActions.Player.Move.ReadValue<Vector2>();
        move.y = 0.0f;
        _rb.AddForce(move.normalized * movementVelocity, ForceMode.VelocityChange);
        
        // Rotate the player to face the direction the camera is looking at
        transform.rotation =  Quaternion.AngleAxis(_mainCamera.transform.eulerAngles.y, Vector3.up);
    }
}
