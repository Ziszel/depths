using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SwitchController : MonoBehaviour
{
    private LightController _light;
    private Transform _playerTransform;
    private float _distanceBetweenPlayerAndSwitch;
    private bool _isInProximity;

    [SerializeField] private TMP_Text _switchInteractionText;
    [SerializeField] private float _proximityDistance = 3.0f;

    void Start()
    {
        _light = GameObject.FindGameObjectWithTag("Light").GetComponent<LightController>();
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        _switchInteractionText.gameObject.SetActive(false);

        if (_light == null)
        {
            Debug.LogError("No LightController script found on the object tagged 'Light'.");
        }
        else
        {
            Debug.Log("_light: " + _light);
        }
    }
    private void Update()
    {
        _distanceBetweenPlayerAndSwitch = Vector3.Distance(transform.position, _playerTransform.position);
        _isInProximity = _distanceBetweenPlayerAndSwitch < _proximityDistance;

        if (_isInProximity)
        {
            _switchInteractionText.gameObject.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                /*if (_light.enabled == true)
                {
                    
                    _light.StopFlicker();
                }
                else
                {
                    _light.StartFlicker();
                }*/
                _light.ToggleLight();
                Debug.Log("Is light on? " + _light.enabled);
            }
        }
        else
        {
            _switchInteractionText.gameObject.SetActive(false);
        }
    }
}
