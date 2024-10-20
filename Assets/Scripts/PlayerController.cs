using System;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Event delegates
    public Action<bool> OnPlayerLookingAtMonster;
    public Action OnPlayerDeath;
    
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
    [SerializeField] private GameObject head;
    [SerializeField] private GameObject crouch;

    [Header("DEBUG, Disable all on ship")]
    private bool _monsterNotInScene;
    
    // Components
    private PlayerInput _inputActions;
    private Vector2 _moveInput;
    private Rigidbody _rb;
    private Camera _mainCamera;
    private CinemachineCamera _cinemachineCamera;
    private Monster _monster;
    private GameObject _interactable;
    private FloorCollider _floorCollider;
    
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
        _floorCollider = GetComponentInChildren<FloorCollider>();
        _cinemachineCamera = FindAnyObjectByType<CinemachineCamera>(); 
        _interactable = null;
        
        // Hook up events
        _monster.OnPlayerWithinKillDistance += OnKillPlayer;
    }

    private void Update()
    {
        IsPlayerLookingAtMonster();
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
        if (context.started)
        {
            // Safety check against null
            if (_interactable == null)
            {
                Debug.Log("No object currently interactable");
                return;
            }
            
            if(_interactable.TryGetComponent(out IInteractable interactable))
            {
                interactable.AttemptToInteract();
            }
        }
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Debug.Log("pause the game");
            // Pause the game if we're not paused
            if (Time.timeScale == 1f)
            {
                GameManager.instance.Pause();
            }
            // Unpause the game if we are paused
            else
            {
                GameManager.instance.Unpause();
            }
            
        }
    }

    public void OnKillPlayer()
    {
        DisableInputActions();
        _rb.constraints = RigidbodyConstraints.None;
        OnPlayerDeath?.Invoke();
    }

    private void SetMovementValues()
    {
        // Set current movement velocity and maxVelocity depending on if the player is crouching, walking, or sprinting
        // Check if crouching
        if (Mathf.Approximately(_inputActions.Player.Crouch.ReadValue<float>(), 1.0f))
        {
            if (_floorCollider.IsOnGround())
            {
                _cinemachineCamera.Target.TrackingTarget = crouch.transform;
                movementVelocity = crouchVelocity;
                maxMovementVelocity = maxCrouchVelocity;
            }
        }
        // Check if sprinting
        else if (Mathf.Approximately(_inputActions.Player.Sprint.ReadValue<float>(), 1.0f))
        {
            _cinemachineCamera.Target.TrackingTarget = head.transform;
            maxMovementVelocity = maxSprintVelocity;
        }
        // We are walking
        else
        {
            _cinemachineCamera.Target.TrackingTarget = head.transform;
            movementVelocity = walkVelocity;
            maxMovementVelocity = maxWalkVelocity;
        }
    }

    private void MoveAndRotate()
    {
        if (_floorCollider.IsOnGround())
        {
            // Move the player relative to the camera's rotation and clamp the movement velocity
            Vector3 move = cameraTransform.forward * _moveInput.y + cameraTransform.right * _moveInput.x;
            _moveInput = _inputActions.Player.Move.ReadValue<Vector2>();
            move.y = 0.0f;
            _rb.AddForce(move.normalized * movementVelocity, ForceMode.VelocityChange);
        }
        
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

    public Rigidbody GetRigidBody()
    {
        return _rb;
    }

    public void SetCurrentInteractable(GameObject interactable)
    {
        _interactable = interactable;
    }

    public void EnableInputActions()
    {
        _inputActions.Enable();
    }
    public void DisableInputActions()
    {
        _inputActions.Disable();
    }

    private void OnDisable()
    {
        _inputActions.Player.Disable();
        _monster.OnPlayerWithinKillDistance -= OnKillPlayer;
    }
}
