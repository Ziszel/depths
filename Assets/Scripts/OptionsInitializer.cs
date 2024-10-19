using UnityEngine;

public class OptionsInitializer : MonoBehaviour
{
    void Start()
    {
        InitialiseOptions();
    }
    private void InitialiseOptions()
    {
        float mouseSensitivity = PlayerPrefs.GetFloat("MouseSensitivity", 1.0f);
        Debug.Log("mouse sensi: " +  mouseSensitivity);
    }

}
