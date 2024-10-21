using UnityEngine;
using UnityEngine.UI;

public class LetterManager : MonoBehaviour
{
    // edit
    public Button _letterExitBtn;
    public Button _letterContinueBtn;

    void Start()
    {
        _letterExitBtn.onClick.AddListener(OnLetterExitClicked);
        _letterContinueBtn.onClick.AddListener(OnLetterContinueClicked);
    }

    private void OnLetterExitClicked()
    {
        GameManager.instance.ShowMainMenu();
    }

    private void OnLetterContinueClicked()
    {
        // call game manager to deactivate this 
        GameManager.instance.LetterContinue();
    }
}
