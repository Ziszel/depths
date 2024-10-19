using UnityEngine;
using UnityEngine.UI;

public class OptionsManager : MonoBehaviour
{
    public Slider _mouseSensitivitySlider;
    public Slider _musicVolumeSlider;
    public Slider _sfxVolumeSlider;

    public void Start()
    {
        // Loading in potential previous saved player option prefs
        _mouseSensitivitySlider.value = PlayerPrefs.GetFloat("MouseSensitivity", 1.0f);
        _musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1.0f);
        _sfxVolumeSlider.value = PlayerPrefs.GetFloat("SFXVolume", 1.0f);

        _mouseSensitivitySlider.onValueChanged.AddListener(OnMouseSensitivityChanged);
        _musicVolumeSlider.onValueChanged.AddListener(OnMusicVolumeChanged);
        _sfxVolumeSlider.onValueChanged.AddListener(OnSFXVolumeChanged);
    }
    private void OnMouseSensitivityChanged(float value)
    {
        PlayerPrefs.SetFloat("MouseSensitivity", value);
        PlayerPrefs.Save();
    }
    private void OnMusicVolumeChanged(float value)
    {
        PlayerPrefs.SetFloat("MusicVolume", value);
        PlayerPrefs.Save();
    }
    private void OnSFXVolumeChanged(float value)
    {
        PlayerPrefs.SetFloat("SFXVolume", value);
        PlayerPrefs.Save();
    }
}
