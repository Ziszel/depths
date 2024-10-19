using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    private Button _startGameBtn;
    private Button _optionsBtn;

    private void Awake()
    {
        SetButtonReferences();
        _startGameBtn.onClick.AddListener(OnStartGameClicked);
        _optionsBtn.onClick.AddListener(OnOptionsClicked);
    }

    private void OnStartGameClicked()
    {
        GameManager.instance.LoadLevel("MainLevel");
    }

    private void OnOptionsClicked()
    {
        GameManager.instance.ShowOptionsCanvas(true);
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
        }
    }
}
