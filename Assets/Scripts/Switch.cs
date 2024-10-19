using System.Collections;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;

public class Switch : MonoBehaviour, IInteractable
{
    [SerializeField] private float switchMovementTime = 2.0f;
    [SerializeField] private Vector3 rotationVector = new ( 0.0f, 0.0f, 45.0f );
    
    public GameObject Switchable;
    private PlayerController _player;
    private bool _isSwitchDown;
    private Transform _lever;
    
    private void Start()
    {
        _isSwitchDown = false;
        _player = FindAnyObjectByType<PlayerController>();
        _lever = GetComponentsInChildren<Transform>().First(k => k.gameObject.name == "Lever");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            _player.SetCurrentInteractable(this.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _player.SetCurrentInteractable(null);
        }
    }

    private IEnumerator MoveSwitch()
    {
        float timeElapsed = 0.0f;
        Quaternion endRotation;
        if (!_isSwitchDown)
        {
            endRotation = transform.rotation * Quaternion.Euler(rotationVector);
        }
        else
        {
            endRotation = transform.rotation * Quaternion.Inverse(quaternion.Euler(rotationVector));
        }

        while (timeElapsed < switchMovementTime)
        {
            _lever.rotation = Quaternion.Slerp(transform.rotation, endRotation, timeElapsed);
            timeElapsed += (timeElapsed + Time.deltaTime) / switchMovementTime;
            yield return null;
        }

        int i = _isSwitchDown ? 0 : 1;
        
        if (i == 0) { _isSwitchDown = false; }
        else { _isSwitchDown = true; }
        
        _lever.rotation = endRotation;
    }

    public void AttemptToInteract()
    {
        if (Switchable.TryGetComponent(out ISwitchable switchable))
        {
            StartCoroutine(MoveSwitch());
            switchable.Toggle();   
        }
    }
}
