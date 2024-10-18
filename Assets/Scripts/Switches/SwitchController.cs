using UnityEngine;
using UnityEngine.UI;

public class SwitchController : MonoBehaviour
{
    private ISwitchable _switchable;
    private Transform _playerTransform;
    private float _distanceBetweenPlayerAndSwitch;
    private bool _isInProximity;

    [SerializeField] private float _proximityDistance = 3.0f;
    [SerializeField] private Text _interactionText;
    [SerializeField] private MonoBehaviour _switchableObject;

    void Start()
    {
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        /*if (!player)
        {
            _playerTransform = player.transform;
        }
        else
        {
            Debug.LogError("SwitchController Start(): Object tagged 'Player' could not be found");
        }*/

        if (_interactionText != null)
        {
            _interactionText.gameObject.SetActive(false);
        }

        if (_switchableObject is ISwitchable switchable)
        {
            _switchable = switchable;
        }
        else
        {
            Debug.LogError("Assigned object does not implement ISwitchable!");
        }
    }
    void Update()
    {
        if (!_playerTransform)
        {
            _distanceBetweenPlayerAndSwitch = Vector3.Distance(transform.position, _playerTransform.position);
            _isInProximity = _distanceBetweenPlayerAndSwitch < _proximityDistance;

            if (_isInProximity && _interactionText != null)
            {
                Debug.Log("in proximity");
                _interactionText.gameObject.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E) && _switchableObject != null)
                {
                    _switchable.Toggle();
                    Debug.Log("Switch triggered!");
                }
            }
            else
            {
                _interactionText.gameObject.SetActive(false);
            }
        }
    }
}
