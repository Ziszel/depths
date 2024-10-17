using UnityEngine;

public class LightController : MonoBehaviour
{
    private Light _flickeringLight;
    private float _flickerInterval = 0.1f;
    private float _timer = 0;

    [Header("Flicker Settings (input -1 for random flickering)")]
    [SerializeField] private float _customFlickerInterval = 0.1f;
    [SerializeField] private bool _isFlickering = true;

    void Start()
    {
        _flickeringLight = GetComponent<Light>();
    }

    void Update()
    {
        if (_isFlickering) 
        { 
            // Check if -1 was input for random flickering, if not, use custom flicker interval input
            if (_customFlickerInterval == -1) 
            {
                if (_timer == 0 )
                {
                    _flickerInterval = Random.Range(0.0f, 1.0f);
                }
                HandleFlicker(_flickerInterval); 
            }
            else 
            { 
                HandleFlicker(_customFlickerInterval); 
            }
        }
    }
    private void HandleFlicker(float flickerInterval)
    {
        _timer += Time.deltaTime;
        if (_timer > flickerInterval)
        {
            ToggleLight();
            _timer = 0;
        }
    }

    public void StartFlicker()
    {
        _isFlickering = true;
    }

    public void StopFlicker()
    {
        _isFlickering = false;
        _flickeringLight.enabled = true; 
    }
    public void TurnOnLight()
    {
        _flickeringLight.enabled = true;
    }

    public void TurnOffLight()
    {
        _flickeringLight.enabled = false;
    }

    public void ToggleLight()
    {
        _flickeringLight.enabled = !_flickeringLight.enabled;
    }
}
