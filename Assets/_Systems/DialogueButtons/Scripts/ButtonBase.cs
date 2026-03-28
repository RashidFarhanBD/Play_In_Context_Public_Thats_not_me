using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class ButtonBase : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
    [SerializeField] protected TMP_Text _displayTextRef;

    public event Action OnButtonClicked;
    public event Action OnButtonPressed;
    public event Action OnButtonReleased;

    public void RaiseButtonClickedEvent()
    {
        OnButtonClicked?.Invoke();
    }

    public void RaiseButtonPressedEvent()
    {
        OnButtonPressed?.Invoke();
    }

    public void RaiseButtonReleasedEvent()
    {
        OnButtonReleased?.Invoke();
    }

    public virtual void Deinitialize()
    {
        OnButtonClicked = null;
        OnButtonPressed = null;
        OnButtonReleased = null;
    }

    public abstract void OnPointerClick(PointerEventData eventData);
    public abstract void OnPointerDown(PointerEventData eventData);
    public abstract void OnPointerUp(PointerEventData eventData);
}
