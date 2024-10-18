using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private float _bestTime; 

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

    public void LoadLevel(string levelName)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(levelName);
    }

    public void ShowOptions()
    {
        // Show options menu overlay (should work on MainMenu and in game)
        Debug.Log("options clicked");
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
