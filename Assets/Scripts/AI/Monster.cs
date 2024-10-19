using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour
{
    public enum MonsterState
    {
        None, // no state, do nothing
        ChasePath, // Follow a path but allow for chasing the player
        Chase,
        SafePath // Follow a path but do NOT allow for chasing the player
    }
    
    private PlayerController _player;
    private NavMeshAgent _agent;
    private bool _isPlayerLookingAtMe;
    private MonsterState _monsterState;
    
    // path node logic
    private int _currentNodeIndicator;
    private int _previousNodeIndicator;
    private List<Vector3> _pathNodes;
    private readonly float _minimumDistanceToNode = 5.0f;
    
    // Chase helpers
    [SerializeField] private float minimumChaseTime = 2.0f;
    private float _currentChaseTime;
    
    private void Start()
    {
        _monsterState = MonsterState.None;
        _isPlayerLookingAtMe = false;
        _agent = GetComponent<NavMeshAgent>();
        _player = FindAnyObjectByType<PlayerController>();
        _currentChaseTime = 0.0f;
        
        // subscribe to events
        _player.OnPlayerLookingAtMonster += SetIsPlayerLooking;
    }
    
    private void Update()
    {
        switch (_monsterState)
        {
            case MonsterState.None:
                // Do nothing, this is intentional
                break;
            case MonsterState.ChasePath:
                if (CanMonsterSeePlayer())
                {
                    _agent.destination = _player.transform.position;
                    SetMonsterState(MonsterState.Chase);
                }
                else
                {
                    if (Vector3.Distance(transform.position, _agent.destination) < _minimumDistanceToNode)
                    {
                        RandomlySetNextNode();
                    }
                }
                break;
            case MonsterState.Chase:
                if (_currentChaseTime > minimumChaseTime)
                {
                    if (CanMonsterSeePlayer())
                    {
                        _currentChaseTime = minimumChaseTime;
                    }
                    else
                    {
                        // We can safely set the same _pathNodes as before
                        SetMonsterState(MonsterState.ChasePath, _pathNodes);
                    }
                }
                _agent.destination = _player.transform.position;
                _currentChaseTime += Time.deltaTime;
                break;
            case MonsterState.SafePath:
                // Traverse a path
                if (Vector3.Distance(transform.position, _pathNodes[_currentNodeIndicator]) 
                    < _minimumDistanceToNode)
                {
                    // If we have reached the end of the safe path, teleport the monster out of the map
                    if ((_currentNodeIndicator + 1) > _pathNodes.Count - 1)
                    {
                        SetMonsterState(MonsterState.None);
                        transform.position = new Vector3(0.0f, -100.0f, 0.0f);
                    }
                    else
                    {
                        _currentNodeIndicator++;
                        _agent.destination = _pathNodes[_currentNodeIndicator];
                    }
                }
                break;
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
        if (_isPlayerLookingAtMe)
        {
            return true;
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
    
    private void RandomlySetNextNode()
    {
        int oldNodeIndicator = _currentNodeIndicator;
        
        // Avoid selecting the same point
        while (_currentNodeIndicator == _previousNodeIndicator)
        {
            _currentNodeIndicator = Random.Range(0, _pathNodes.Count);   
        }

        _agent.destination = _pathNodes[_currentNodeIndicator];
        _previousNodeIndicator = oldNodeIndicator;
    }

    private void SetIsPlayerLooking(bool newIsPlayerLooking)
    {
        _isPlayerLookingAtMe = newIsPlayerLooking;
    }

    private void OnDisable()
    {
        _player.OnPlayerLookingAtMonster -= SetIsPlayerLooking;
    }

    public void SetMonsterState(MonsterState monsterState)
    {
        _monsterState = monsterState;
        _currentNodeIndicator = 0;
    }

    public void SetMonsterState(MonsterState monsterState, List<Vector3> newPathNodes)
    {
        _monsterState = monsterState;
        _currentNodeIndicator = 0;
        _pathNodes = newPathNodes;
        _agent.destination = _pathNodes[_currentNodeIndicator];
    }
}
