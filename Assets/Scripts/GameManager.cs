using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private float _bestTime;

    /* Storing in this class so we can access them across levels */
    private GameObject _mainMenuCanvas;
    private GameObject _optionsMenuCanvas; 
    private GameObject _creditsCanvas;

    public void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Start()
    {
        _mainMenuCanvas = GameObject.Find("MainMenuCanvas");
        _optionsMenuCanvas = GameObject.Find("OptionsMenuCanvas");
        _creditsCanvas = GameObject.Find("CreditsCanvas");
        //_optionsMenuCanvas.SetActive(false);
        if (_creditsCanvas) { _creditsCanvas.SetActive(false); }
    }

    public void LoadLevel(string levelName)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(levelName);
        //_optionsMenuCanvas = GameObject.Find("OptionsMenuCanvas"); // reassign options menu 
        //_optionsMenuCanvas.SetActive(false);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "KaliTest2_OptionsTesting" || scene.name == "MainLevel") // DELETE KALITEST2 SCENE ONCE HOOKED UP TO MAIN LEVEL
        {
            // Update the reference to the in-game options menu
            _optionsMenuCanvas = GameObject.Find("OptionsMenuCanvas"); // Replace with the correct object name
        }
        else if (scene.name == "MainMenu")
        {
            // Update the reference to the main menu options menu
            _optionsMenuCanvas = GameObject.Find("OptionsMenuCanvas"); // Replace with the correct object name
        }
        _optionsMenuCanvas.SetActive(false);
    }

    // Ideally, we don't want mainmenu canvas to be a parameter here, as we want this to be able to be called from in-game
    public void ShowOptionsCanvas(bool isFromMainMenu)
    {
        // Show options menu overlay (should work on MainMenu and in game)
        Debug.Log("ShowOptionsCanvas() called");

        if (_optionsMenuCanvas)
        {
            _optionsMenuCanvas.SetActive(true);
        }
        else
        {
            Debug.Log("GameManager ShowOptionsCanvas(): Could not find the options menu canvas object");
        }
        if (_mainMenuCanvas)
        {
            _mainMenuCanvas.SetActive(false);
        }
        else
        {
            Debug.Log("GameManager ShowOptionsCanvas(): Could not find the main menu canvas object");
        }
    }

    // Maybe rename this function - backto main menu from options or something
    public void ShowMainMenuCanvas()
    {
        if (_mainMenuCanvas)
        {
            _mainMenuCanvas.SetActive(true);
        }
        else
        {
            // This must mean we are in-game if the main menu canvas does not exist..
            Debug.Log("GameManager ShowMainMenuCanvas(): Could not find the main menu canvas object so we must be in-game");
            Time.timeScale = 1f;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        if (_optionsMenuCanvas)
        {
            _optionsMenuCanvas.SetActive(false);
        }
        else
        {
            Debug.Log("GameManager ShowMainMenuCanvas(): Could not find the options menu canvas object");
        }
    }

    // we know this will only ever get called from the main menu
    public void ShowCreditsCanvas()
    {
        // Show options menu overlay (should work on MainMenu and in game)
        Debug.Log("ShowOptionsCanvas() called");

        if (_creditsCanvas)
        {
            _creditsCanvas.SetActive(true);
        }
        else
        {
            Debug.Log("GameManager ShowOptionsCanvas(): Could not find the credits canvas object");
        }
        if (_mainMenuCanvas)
        {
            _mainMenuCanvas.SetActive(false);
        }
        else
        {
            Debug.Log("GameManager ShowOptionsCanvas(): Could not find the main menu canvas object");
        }
    }

    public void SetBestTime(float newBestTime)
    {
        _bestTime = newBestTime;
    }

    public float GetBestTime()
    {
        return _bestTime;
    }
    
}
