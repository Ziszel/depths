using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public LightController elevatorLight;
    public HorizontalDoor doorOne;
    public HorizontalDoor doorTwo;
    
    [SerializeField] private float wobbleAmplitude;
    [SerializeField] private float wobbleFrequency;
    [SerializeField] private AudioClip audioClip;
    [SerializeField] private float sequenceDuration = 5.0f;

    private PlayerController _player;
    private CinemachineCamera _camera;
    private CinemachineBasicMultiChannelPerlin _perlinComponent;

    private float _previousAmplitudeGain;
    private float _previousFrequencyGain;

    private void Start()
    {
        _player = FindAnyObjectByType<PlayerController>();
        _camera = FindAnyObjectByType<CinemachineCamera>();
        _perlinComponent = _camera.GetComponent<CinemachineBasicMultiChannelPerlin>();
        _previousAmplitudeGain = _perlinComponent.AmplitudeGain;
        _previousFrequencyGain = _perlinComponent.FrequencyGain;
    }

    public IEnumerator ElevatorSequence()
    {
        float timeElapsed = 0.0f;
        
        _perlinComponent.AmplitudeGain = wobbleAmplitude;
        _perlinComponent.FrequencyGain = wobbleFrequency;
        
        _player.DisableInputActions();
        
        while (timeElapsed < sequenceDuration)
        {
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        
        doorOne.Toggle();
        doorTwo.Toggle();
        elevatorLight.StopFlicker();
        elevatorLight.TurnOnLight();
        _perlinComponent.AmplitudeGain = _previousAmplitudeGain;
        _perlinComponent.FrequencyGain = _previousFrequencyGain;
        _player.EnableInputActions();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(ElevatorSequence());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(this);
        }
    }
}
