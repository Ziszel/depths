using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private float _bestTime;
    private GameObject _optionsMenuCanvas; /* Storing in this class so we can access it across levels */

    public void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        _optionsMenuCanvas = GameObject.Find("OptionsMenuCanvas");
        _optionsMenuCanvas.SetActive(false);
    }

    public void LoadLevel(string levelName)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(levelName);
    }

    // Ideally, we don't want mainmenu canvas to be a parameter here, as we want this to be able to be called from in-game
    public void ShowOptionsCanvas(bool isFromMainMenu, GameObject mainMenuCanvas)
    {
        // Show options menu overlay (should work on MainMenu and in game)
        Debug.Log("ShowOptions() called");

        if (_optionsMenuCanvas)
        {
            _optionsMenuCanvas.SetActive(true);
        }
        else
        {
            Debug.LogError("GameManager ShowOptions(): Could not find the options menu canvas object");
        }
        if (mainMenuCanvas)
        {
            mainMenuCanvas.SetActive(false);
        }
        else
        {
            Debug.LogError("GameManager ShowOptions(): Could not find the main menu canvas object");
        }
    }

    // Maybe rename this function - backto main menu from options or something
    public void ShowMainMenuCanvas(GameObject mainMenuCanvas)
    {
        if (mainMenuCanvas)
        {
            mainMenuCanvas.SetActive(true);
        }
        else
        {
            Debug.LogError("GameManager ShowMainMenu(): Could not find the main menu canvas object");
        }
        if (_optionsMenuCanvas)
        {
            _optionsMenuCanvas.SetActive(false);
        }
        else
        {
            Debug.LogError("GameManager ShowMainMenu(): Could not find the options menu canvas object");
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
