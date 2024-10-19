using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
//using static UnityEngine.Rendering.DebugUI;

public class OptionsManager : MonoBehaviour
{
    private Button _optionsBackBtn;

    public Slider _mouseSensitivitySlider;
    public Slider _musicVolumeSlider;
    public Slider _sfxVolumeSlider;

    public FPSCamera _FPSCamera;
    public AudioMixer _musicMixer;
    public AudioMixer _SFXMixer;

    public bool isMainMenu = true;

    public void Start()
    {
        // Loading in potential previous saved player option prefs
        _mouseSensitivitySlider.value = PlayerPrefs.GetFloat("MouseSensitivity", 1.0f);
        _musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1.0f);
        _sfxVolumeSlider.value = PlayerPrefs.GetFloat("SFXVolume", 1.0f);

        _mouseSensitivitySlider.onValueChanged.AddListener(OnMouseSensitivityChanged);
        _musicVolumeSlider.onValueChanged.AddListener(OnMusicVolumeChanged);
        _sfxVolumeSlider.onValueChanged.AddListener(OnSFXVolumeChanged);

        _optionsBackBtn = transform.Find("BackBtn").GetComponent<Button>();
        _optionsBackBtn.onClick.AddListener(OnOptionsBackClicked);

        // then attempt to set actual values as we might have 'just loaded in'
        if (!isMainMenu)
        {
            // CHANGE TEXT ON BUTTON TO 'RESUME' AND CHANGE ON CLICK EVENT
            _optionsBackBtn.GetComponentInChildren<TextMeshProUGUI>().SetText("RESUME");

            InitialiseOptions();
        }
    }

    private void InitialiseOptions()
    {
        // only load in fps camera if we know we're not in main menu 
        _FPSCamera = GameObject.Find("FPSCamera").GetComponent<FPSCamera>();
        _FPSCamera.SetGain(PlayerPrefs.GetFloat("MouseSensitivity", 1.0f));

        // ADD AUDIO SETTINGS
    }

    private void OnOptionsBackClicked()
    {
        Debug.Log("OPTIONS MENU BUTTON CLICKED");
        GameManager.instance.ShowMainMenuCanvas();
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnMouseSensitivityChanged(float value)
    {
        PlayerPrefs.SetFloat("MouseSensitivity", value);
        PlayerPrefs.Save();

        // Attempt to find FPSCamera to change it directly if we're in game
        if (!isMainMenu)
        {
            // only load in fps camera if we know we're not in main menu 
            _FPSCamera.SetGain(value);
            Debug.Log("Set gain to " + value);
        }

    }
    private void OnMusicVolumeChanged(float value)
    {
        PlayerPrefs.SetFloat("MusicVolume", value);
        PlayerPrefs.Save();
        _musicMixer.SetFloat("MusicVolume", value);
    }
    private void OnSFXVolumeChanged(float value)
    {
        PlayerPrefs.SetFloat("SFXVolume", value);
        PlayerPrefs.Save();
        _SFXMixer.SetFloat("SFXVolume", value);
    }
}
