using UnityEngine;


public class NavigationPanel : MonoBehaviour
{
    [SerializeField] private NavigationButton _dialogueButton;
    [SerializeField] private NavigationButton _searchButton;

    private void Awake()
    {
        _dialogueButton.OnButtonClicked += OpenDialoguePanel;
        _searchButton.OnButtonClicked += OpenSearchPanel;
    }

    void OnDestroy()
    {
        _dialogueButton.OnButtonClicked -= OpenDialoguePanel;
        _searchButton.OnButtonClicked -= OpenSearchPanel;
    }

    private void OpenDialoguePanel()
    {
        _searchButton.Unselect();
        DialogueManager.Instance.Body.SetActive(true);
        SearchPanel.Instance.Body.SetActive(false);
    }

    private void OpenSearchPanel()
    {
        _dialogueButton.Unselect();
        DialogueManager.Instance.Body.SetActive(false);
        SearchPanel.Instance.Body.SetActive(true);
    }
}
