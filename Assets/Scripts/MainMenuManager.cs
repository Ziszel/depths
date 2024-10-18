using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    private Button _startGame;
    private Button _options;

    private void Awake()
    {
        SetButtonReferences();
        _startGame.onClick.AddListener(OnStartGameClicked);
        _options.onClick.AddListener(OnOptionsClicked);
    }

    private void OnStartGameClicked()
    {
        GameManager.instance.LoadLevel("MainLevel");
    }

    private void OnOptionsClicked()
    {
        GameManager.instance.ShowOptions();
    }
    
    private void SetButtonReferences()
    {
        Button[] buttons = GetComponentsInChildren<Button>();
        foreach (var b in buttons)
        {
            if (b.gameObject.name == "StartGameBtn")
            {
                _startGame = b;
                continue;
            }

            if (b.gameObject.name == "OptionsBtn")
            {
                _options = b;
            }
        }
    }
}
