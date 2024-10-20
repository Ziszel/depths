using System.Collections.Generic;
using UnityEngine;

public class SetMonsterStateTrigger : MonoBehaviour
{
    [SerializeField] private Monster.MonsterState _monsterState;
    [SerializeField] private Vector3 monsterPosition;
    [SerializeField] private List<Vector3> pathNodes;

    private BoxCollider _collider;
    
    private Monster _monster;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _monster = FindAnyObjectByType<Monster>();
        _collider = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Set monster state");
            _monster.SetMonsterState(_monsterState, pathNodes, monsterPosition);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _collider.enabled = false;
        }
    }

    public void SetColliderOn()
    {
        _collider.enabled = true;
    }
}
