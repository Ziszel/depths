using UnityEngine;

public class LightProximitySensor : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Light"))
        {
            Debug.Log("collider hitting a light");
            if (other.TryGetComponent(out IMonsterInteractable monsterInteractable))
            {
                monsterInteractable.AffectedByMonster();
                Debug.Log("collider trying to affect light");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Light"))
        {
            if (other.TryGetComponent(out IMonsterInteractable monsterInteractable))
            {
                monsterInteractable.StopAffectedByMonster();
            }
        }
    }
}
