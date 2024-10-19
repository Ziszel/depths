using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    private GameObject _mainMenuCanvas; 

    // Main menu buttons
    private Button _startGameBtn;
    private Button _optionsBtn;

    // Options menu buttons
    private Button _optionsBackBtn;

    private void Awake()
    {
        SetButtonReferences();
        _startGameBtn.onClick.AddListener(OnStartGameClicked);
        _optionsBtn.onClick.AddListener(OnOptionsClicked);
        _optionsBackBtn.onClick.AddListener(OnOptionsBackClicked);
    }

    private void Start()
    {
        _mainMenuCanvas = GameObject.Find("MainMenuCanvas");
    }

    private void OnStartGameClicked()
    {
        GameManager.instance.LoadLevel("MainLevel");
    }

    private void OnOptionsClicked()
    {
        GameManager.instance.ShowOptionsCanvas(true, _mainMenuCanvas);
    }
    private void OnOptionsBackClicked()
    {
        GameManager.instance.ShowMainMenuCanvas(_mainMenuCanvas);
    }

    private void SetButtonReferences()
    {
        Button[] buttons = GetComponentsInChildren<Button>();
        foreach (var b in buttons)
        {
            if (b.gameObject.name == "StartGameBtn")
            {
                _startGameBtn = b;
                continue;
            }

            if (b.gameObject.name == "OptionsBtn")
            {
                _optionsBtn = b;
            }

            if (b.gameObject.name == "BackBtn")
            {
                _optionsBackBtn = b;
            }
        }
    }
}
