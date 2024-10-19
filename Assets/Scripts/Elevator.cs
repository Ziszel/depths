using UnityEngine;

public class Elevator : MonoBehaviour
{
    [SerializeField] private float _wobble;
    [SerializeField] private AudioClip _audio;

    // Will be called for as long as game object is active
    void Update()
    {
        Debug.Log(_wobble);
    }
}
