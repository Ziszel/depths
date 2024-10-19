using System.Collections.Generic;
using UnityEngine;

public class SetMonsterStateTrigger : MonoBehaviour
{
    [SerializeField] private Monster.MonsterState _monsterState;
    [SerializeField] private List<Vector3> pathNodes;

    private Monster _monster;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _monster = FindAnyObjectByType<Monster>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _monster.SetMonsterState(_monsterState, pathNodes);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Destroy(this);
    }
}
