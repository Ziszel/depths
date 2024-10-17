using UnityEngine;

public class LightController : MonoBehaviour
{
    private Light flickeringLight;
    private float timer = 0;
    private float flickerInterval;

    [Header("Flicker Settings (input -1 for random flickering)")]
    [SerializeField] private float customFlickerInterval = 0.1f;
    [SerializeField] private bool isFlickering = true;

    void Start()
    {
        flickeringLight = GetComponent<Light>();
    }

    void Update()
    {
        if (isFlickering) 
        { 
            // Check if -1 was input for random flickering
            if (customFlickerInterval == -1)
            {
                timer += Time.deltaTime;
                if (timer > flickerInterval)
                {
                    flickeringLight.enabled = !flickeringLight.enabled;
                    flickerInterval = Random.Range(0f, 1f);
                    timer = 0;
                }
            }
            // Using custom flicker interval input
            else
            {
                timer += Time.deltaTime;
                if (timer > customFlickerInterval)
                {
                    flickeringLight.enabled = !flickeringLight.enabled;
                    timer = 0;
                }
            }
        }
    }
}
