using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HintPanel : MonoBehaviour, IPointerDownHandler
{
    [SerializeField, TextArea] private string _textTemplate;
    [SerializeField] private TMP_Text _textRef;
    [SerializeField] private CanvasGroup _canvasGroup;

    void Awake()
    {
        GameEvent.OnLockedOptionClicked += InitializePanel;
    }

    void Start()
    {
        HidePanel();
    }

    void OnDestroy()
    {
        GameEvent.OnLockedOptionClicked -= InitializePanel;
    }

    private void InitializePanel(OptionContext optionContext)
    {
        ShowPanel();

        var color = "yellow";
        var displayText = $"<color={color}>{optionContext.DisplayText}</color>";
        var description = $"<color={color}>{optionContext.PenguinData.VisualDescription}</color>";
        var formattedString = string.Format(_textTemplate, displayText, description);

        _textRef.SetText(formattedString);
    }

    public void HidePanel()
    {
        _canvasGroup.alpha = 0;
        _canvasGroup.interactable = false;
        _canvasGroup.blocksRaycasts = false;
    }

    private void ShowPanel()
    {
        _canvasGroup.alpha = 1;
        _canvasGroup.interactable = true;
        _canvasGroup.blocksRaycasts = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        HidePanel();
    }
}
