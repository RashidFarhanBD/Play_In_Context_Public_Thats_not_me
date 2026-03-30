using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OptionButton : ContextButtonBase<OptionContext>
{
    [SerializeField] private Image _buttonVisualRef;
    [SerializeField] private Sprite _unlockedOptionImage;
    [SerializeField] private Sprite _lockedOptionImage;
    [SerializeField] private GameObject _lockIcon;
    [SerializeField] private bool _isLocked;

    public override void Initialize(OptionContext optionContext)
    {
        //base.Initialize(optionContext);

        _context = optionContext;

        if (LocalizationManager.Instance.TryGetText(_context.DisplayText, out var result))
        {
            _displayTextRef.SetText(result);
        }

        gameObject.SetActive(true);

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
        _buttonVisualRef.sprite = _lockedOptionImage;
        _isLocked = true;
        _lockIcon.SetActive(true);

        SearchPanel.Instance.RegisterPenguin(_context.PenguinData, UnlockOption);
    }

    private void UnlockOption()
    {
        _buttonVisualRef.sprite = _unlockedOptionImage;
        _isLocked = false;
        _lockIcon.SetActive(false);
    }

    public override void Deinitialize()
    {
        base.Deinitialize();
        gameObject.SetActive(false);
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        RaiseButtonPressedEvent();
        transform.localScale = Vector3.one * 0.7f;
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        RaiseButtonReleasedEvent();
        transform.localScale = Vector3.one;
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        if (_isLocked)
        {
            GameEvent.RaiseLockedOptionClickedEvent(_context);
        }
        else
        {
            RaiseButtonClickedEvent();
        }
    }
}
