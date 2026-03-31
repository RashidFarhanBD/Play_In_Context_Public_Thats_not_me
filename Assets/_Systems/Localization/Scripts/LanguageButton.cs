using UnityEngine;
using UnityEngine.EventSystems;

public class LanguageButton : ButtonBase
{
    [SerializeField] private AppLanguage _setLanguage;

    public override void OnPointerClick(PointerEventData eventData)
    {
        LocalizationManager.Instance.LocalizationSettings.AppLanguage = _setLanguage;
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }
}
