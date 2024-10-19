using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour
{
    private PlayerController _player;
    private NavMeshAgent _agent;
    private bool _isPlayerLookingAtMe;
    private bool _alwaysChase;
    
    private void Start()
    {
        _isPlayerLookingAtMe = false;
        _alwaysChase = false;
        _agent = GetComponent<NavMeshAgent>();
        _player = FindAnyObjectByType<PlayerController>();
        
        // subscribe to events
        _player.OnPlayerLookingAtMonster += SetIsPlayerLooking;
    }
    
    private void Update()
    {
        // If the monster should not always chase, check if conditions exist to allow the monster to chase
        if (!_alwaysChase)
        {
            if (CanMonsterSeePlayer())
            {
                _agent.destination = _player.transform.position;
            }
        }
        else
        {
            _agent.destination = _player.transform.position;
        }
    }

    private bool CanMonsterSeePlayer()
    {
        // Check if player is very close to monster
        if (Vector3.Distance(_player.transform.position, transform.position) < 5)
        {
            return true;
        }

        // Check if the player is looking at the monster
        if (!_isPlayerLookingAtMe)
        {
            return false;
        }
        
        // Check if player is in line of sight of the monster
        Vector3 directionOfRay = (_player.transform.position - transform.position).normalized;
        Ray ray = new Ray(transform.position, directionOfRay);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
        {
            if (hit.collider.CompareTag("Player"))
            {
                return true;
            }
        }
        return false;
    }

    private void SetIsPlayerLooking(bool newIsPlayerLooking)
    {
        _isPlayerLookingAtMe = newIsPlayerLooking;
    }

    private void OnDisable()
    {
        _player.OnPlayerLookingAtMonster -= SetIsPlayerLooking;
    }
}
