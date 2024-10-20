using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class OptionsManager : MonoBehaviour
{
    private Button _optionsMainMenuBtn;

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

        _optionsMainMenuBtn = transform.Find("MainMenuBtn").GetComponent<Button>();
        _optionsMainMenuBtn.onClick.AddListener(OnOptionsMainMenuClicked);

        // then attempt to set actual values as we might have 'just loaded in' to the in-game
        if (!isMainMenu)
        {
            // instead of this - we want to enable a 2nd button called resume
            //_optionsBackBtn.GetComponentInChildren<TextMeshProUGUI>().SetText("RESUME");

            InitialiseOptions();
        }
    }

    private void InitialiseOptions()
    {
        // this is called if we have just entered the game level, and we can access 
        // potential options set by the user on the main menu through 'PlayerPrefs'.

        // only load in fps camera if we know we're not in main menu 
        _FPSCamera = GameObject.Find("FPSCamera").GetComponent<FPSCamera>();
        _FPSCamera.SetGain(PlayerPrefs.GetFloat("MouseSensitivity", 1.0f));

        // AUDIO SETTINGS
        _musicMixer.SetFloat("MusicVolume", PlayerPrefs.GetFloat("MusicVolume", 1.0f));
        _SFXMixer.SetFloat("SFXVolume", PlayerPrefs.GetFloat("SFXVolume", 1.0f));
    }

    private void OnOptionsMainMenuClicked()
    {
        Debug.Log("OPTIONS MENU MAINMENU BUTTON CLICKED");
        GameManager.instance.ShowMainMenu();
    }

    private void OnMouseSensitivityChanged(float value)
    {
        PlayerPrefs.SetFloat("MouseSensitivity", value);
        PlayerPrefs.Save();

        if (!isMainMenu)
        {
            // only load in fps camera if we know we're not in main menu 
            _FPSCamera.SetGain(value);
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
