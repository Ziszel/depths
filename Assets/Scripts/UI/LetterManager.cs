using UnityEngine;
using UnityEngine.UI;

public class LetterManager : MonoBehaviour
{
    public Button _letterExitBtn;
    public Button _letterContinueBtn;

    void Start()
    {
        //_letterExitBtn = transform.Find("LetterExitBtn").GetComponent<Button>();
        /*if (!_letterExitBtn)
        {
            Debug.LogError("Button component not found on LetterExitBtn!");
        }*/
        _letterExitBtn.onClick.AddListener(OnLetterExitClicked);

        //_letterContinueBtn = transform.Find("LetterContinueBtn").GetComponent<Button>();
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
