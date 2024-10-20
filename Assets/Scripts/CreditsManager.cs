using UnityEngine;
using UnityEngine.UI;

public class CreditsManager : MonoBehaviour
{

    private Button _creditsMainMenuBtn;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _creditsMainMenuBtn = transform.Find("CreditsMainMenuBtn").GetComponent<Button>();
        _creditsMainMenuBtn.onClick.AddListener(OnCreditsToMainMenuClicked);
    }
    private void OnCreditsToMainMenuClicked()
    {
        Debug.Log("OPTIONS MENU MAINMENU BUTTON CLICKED");
        GameManager.instance.ShowMainMenu();
    }

}
