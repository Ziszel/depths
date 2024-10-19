using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Event delegates
    public Action<bool> OnPlayerLookingAtMonster;
    
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
    
    [SerializeField] private float fieldOfViewAngle = 60; // player's cone of vision
    
    // Components
    private PlayerInput _inputActions;
    private Vector2 _moveInput;
    private Rigidbody _rb;
    private Camera _mainCamera;
    private Monster _monster;
    
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
        _monster = FindAnyObjectByType<Monster>();
    }

    private void Update()
    {
        //IsPlayerLookingAtMonster();
    }

    private void FixedUpdate()
    {
        SetMovementValues();
        MoveAndRotate();

        _rb.linearVelocity = Vector3.ClampMagnitude(_rb.linearVelocity, maxMovementVelocity);
    }
    
    // Input events
    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("interact with object");
        }
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Debug.Log("pause the game");
        }
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

    public void IsPlayerLookingAtMonster()
    {
        Vector3 directionOfRay = (_monster.transform.position - transform.position).normalized;
        Ray ray = new Ray(transform.position, directionOfRay);

        if ((Vector3.Angle(directionOfRay, transform.forward)) < fieldOfViewAngle / 2)
        {
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
            {
                if (hit.collider.CompareTag("Monster"))
                {
                    OnPlayerLookingAtMonster?.Invoke(true);
                }
                else
                {
                    OnPlayerLookingAtMonster?.Invoke(false);
                }
            }
        }
    }

    private void OnDisable()
    {
        _inputActions.Player.Disable();
    }
}
