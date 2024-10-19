using UnityEngine;

public class Switch : MonoBehaviour, IInteractable
{
    public GameObject Switchable;
    private PlayerController _player;
    
    private void Start()
    {
        _player = FindAnyObjectByType<PlayerController>();
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

    public void AttemptToInteract()
    {
        Debug.Log("Attempting to interact with Switch...");
        if (Switchable.TryGetComponent(out ISwitchable switchable))
        {
            switchable.Toggle();   
        }
    }
}
