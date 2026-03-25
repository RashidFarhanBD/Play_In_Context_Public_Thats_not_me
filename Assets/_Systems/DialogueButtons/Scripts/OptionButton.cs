using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class OptionButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
    [SerializeField] private TMP_Text _displayTextRef;
    [SerializeField] private GameObject _lockIcon;
    [SerializeField] private bool _isLocked;

    private Action _onOptionChosen;
    private OptionContext _optionContext;

    public void Initialize(OptionContext optionContext, Action actionOnChoose)
    {
        _optionContext = optionContext;
        _onOptionChosen = actionOnChoose;

        gameObject.SetActive(true);
        _displayTextRef.SetText(_optionContext.OptionDisplayText);

        if (optionContext.IsLockedByDefault)
        {
            LockOption();
        }
        else
        {
            UnlockOption();
        }
    }

    private void LockOption()
    {
        _isLocked = true;
        _lockIcon.SetActive(true);
    }

    private void UnlockOption()
    {
        _isLocked = false;
        _lockIcon.SetActive(false);
    }

    public void Deinitialize()
    {
        _onOptionChosen = null;
        gameObject.SetActive(false);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        transform.localScale = Vector3.one * 0.7f;
        Debug.Log("Down");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        transform.localScale = Vector3.one;
        Debug.Log("Released");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Clicked");

        if (_isLocked)
        {
            GameEvent.RaiseLockedOptionClickedEvent(_optionContext);
        }
        else
        {
            _onOptionChosen?.Invoke();
        }
    }
}
