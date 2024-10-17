using UnityEngine;
using System.Collections;

public class LightController : MonoBehaviour
{
    // Inspector options
    [Header("Flicker Settings (select -1 for random flickering)")]
    [Tooltip("If using Custom, choose a value from the list")]
    public Light flickeringLight;
    public float customFlickerInterval = 0.1f;

    private float timer = 0;
    private float flickerInterval;

    void Start()
    {
        Debug.Log("We're in Start");
    }

    void Update()
    {
        Debug.Log("We're in Update");

        // Light timer interval to cause it to flick at random

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
