using UnityEngine;
using UnityEngine.UI;

public class LetterManager : MonoBehaviour, IInteractable
{
    public Button _letterExitBtn;
    public Button _letterContinueBtn;

    private PlayerController _player;

    void Start()
    {
        _player = FindAnyObjectByType<PlayerController>();
        _letterExitBtn.onClick.AddListener(OnLetterExitClicked);
        _letterContinueBtn.onClick.AddListener(OnLetterContinueClicked);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _player.SetCurrentInteractable(this.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _player.SetCurrentInteractable(null);
        }
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

    public void AttemptToInteract()
    {
        
        if (GameManager.instance.IsLetterActive())
        {
            GameManager.instance.DeactivateLetter();
        }
        else
        {
            GameManager.instance.ActivateLetter();
        }
        // Figure out a way to hide buttons (or hide one and edit text of other) if we're not in initial starting letter
    }
}
