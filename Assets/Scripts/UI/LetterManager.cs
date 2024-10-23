using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LetterManager : MonoBehaviour, IInteractable
{
    [SerializeField] private Button _letterExitBtn;
    [SerializeField] private Button _letterContinueBtn;

    [Header("Leave empty if you want to use the text in the TMP Object inside LetterCanvas")]
    [SerializeField] private string _letterContents;

    private PlayerController _player;
    private TextMeshPro _textMeshPro;

    void Start()
    {
        _player = FindAnyObjectByType<PlayerController>();
        _letterExitBtn.onClick.AddListener(OnLetterExitClicked);
        _letterContinueBtn.onClick.AddListener(OnLetterContinueClicked);

        _textMeshPro = transform.GetChild(0).GetComponentInChildren<TextMeshPro>();
        if (_letterContents.Length != 0)
        {
            _textMeshPro.SetText(_letterContents);
        }
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
