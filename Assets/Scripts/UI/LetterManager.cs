using UnityEngine;
using UnityEngine.UI;

public class LetterManager : MonoBehaviour
{
    private Button _letterExitBtn;
    private Button _letterContinueBtn;
    void Awake()
    {
        _letterExitBtn = transform.Find("LetterExitBtn").GetComponent<Button>();
        _letterExitBtn.onClick.AddListener(OnLetterExitClicked);

        _letterContinueBtn = transform.Find("LetterContinueBtn").GetComponent<Button>();
        _letterContinueBtn.onClick.AddListener(OnLetterContinueClicked);
    }

    private void OnLetterExitClicked()
    {
        GameManager.instance.ShowMainMenu();
    }

    private void OnLetterContinueClicked()
    {
        // call game manager to deactivate this 
    }
}
