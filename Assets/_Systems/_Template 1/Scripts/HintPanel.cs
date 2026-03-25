using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class HintPanel : MonoBehaviour, IPointerDownHandler
{
    [SerializeField, TextArea] private string _textTemplate;
    [SerializeField] private TMP_Text _textRef;

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
        gameObject.SetActive(true);
        var color = "yellow";
        var displayText = $"<color={color}>{optionContext.OptionDisplayText}</color>";
        var description = $"<color={color}>{optionContext.PenguinDescription}</color>";
        var formattedString = string.Format(_textTemplate, displayText, description);
        _textRef.SetText(formattedString);
    }

    public void HidePanel()
    {
        gameObject.SetActive(false);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        HidePanel();
    }
}
