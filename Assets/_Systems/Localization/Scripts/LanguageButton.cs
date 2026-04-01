using UnityEngine;
using UnityEngine.EventSystems;

public class LanguageButton : ButtonBase
{
    [SerializeField] private AppLanguage _setLanguage;

    public override void OnPointerClick(PointerEventData eventData)
    {
        LocalizationManager.Instance.LocalizationSettings.AppLanguage = _setLanguage;
        RaiseButtonClickedEvent();
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        RaiseButtonPressedEvent();
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        RaiseButtonReleasedEvent();
    }
}
