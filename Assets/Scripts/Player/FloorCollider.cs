using UnityEngine;

public class FloorCollider : MonoBehaviour
{
    public PlayerController player;
    private bool _onGround;

    private void Start()
    {
        _onGround = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Floor"))
        {
            Debug.Log("Now on ground");
            player.GetRigidBody().linearDamping = 5;
            _onGround = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Floor"))
        {
            Debug.Log("Now in the air");
            player.GetRigidBody().linearDamping = 1;
            _onGround = false;
        }
    }

    public bool IsOnGround()
    {
        return _onGround;
    }
}
