using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Splines;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private float _bestTime;

    /* Storing in this class so we can access them across levels */
    private GameObject _mainMenuCanvas;
    private GameObject _optionsMenuCanvas; 
    private GameObject _creditsCanvas;

    private GameObject _mainMenuBtn; 
    private GameObject _resumeBtn;

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

        _optionsMenuCanvas = GameObject.Find("OptionsMenuCanvas");
        //_optionsMenuCanvas.SetActive(false);
        _mainMenuBtn = _optionsMenuCanvas.transform.Find("MainMenuBtn").gameObject;
        _resumeBtn = _optionsMenuCanvas.transform.Find("ResumeBtn").gameObject;
    }

    private void Start()
    {
        //_optionsMenuCanvas = GameObject.Find("OptionsMenuCanvas");
        //_optionsMenuCanvas.SetActive(false);
        //_mainMenuBtn = _optionsMenuCanvas.transform.Find("MainMenuBtn").gameObject;
       // _resumeBtn = _optionsMenuCanvas.transform.Find("ResumeBtn").gameObject;
    }

    public void LoadLevel(string levelName)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(levelName);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        _optionsMenuCanvas = GameObject.Find("OptionsMenuCanvas");
        _mainMenuBtn = _optionsMenuCanvas.transform.Find("MainMenuBtn").gameObject;
        _resumeBtn = _optionsMenuCanvas.transform.Find("ResumeBtn").gameObject;
        _optionsMenuCanvas.SetActive(false);

        if (scene.name == "MainMenu")
        {
            _mainMenuCanvas = GameObject.Find("MainMenuCanvas");
            _mainMenuCanvas.SetActive(true);
            _creditsCanvas = GameObject.Find("CreditsCanvas");
            _creditsCanvas.SetActive(false);

            // Adjust button layout of options menu
            RectTransform rectTransform = _mainMenuBtn.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector3(0, -61, 0);
            _resumeBtn.SetActive(false);
        }
        if (scene.name == "MainLevel" || scene.name == "KaliTest2_OptionsTesting")
        {
            // Ensure game is unpaused upon entering game levels
            Time.timeScale = 1f;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            // Adjust button layout of options menu
            RectTransform rectTransform = _mainMenuBtn.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector3(110, -61, 0);
            _resumeBtn.SetActive(true);
        }
    }

    // Ideally, we don't want mainmenu canvas to be a parameter here, as we want this to be able to be called from in-game
    public void ShowOptionsCanvas(bool isFromMainMenu)
    {
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
    public void ShowMainMenu()
    {
        if (_mainMenuCanvas)
        {
            // we know we are in the MainMenu level so we just need to activate the canvas
            _mainMenuCanvas.SetActive(true);
        }
        else
        {
            // This must mean we are in-game if the main menu canvas does not exist so we want to fully return to the main menu level
            LoadLevel("MainMenu");
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

    public void Pause()
    {
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        ShowOptionsCanvas(true);
    }

    public void Unpause()
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _optionsMenuCanvas.SetActive(false);
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
