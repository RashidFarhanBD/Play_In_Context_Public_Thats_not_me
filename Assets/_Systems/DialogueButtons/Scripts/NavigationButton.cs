using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NavigationButton : ButtonBase
{
    [SerializeField] private bool _selectedByDefault;
    [SerializeField] private Image _buttonBackground;
    [SerializeField] private GameObject _arrow;
    [SerializeField] private Color _unselectedColor = Color.white;
    [SerializeField] private Color _selectedColor = Color.white;

    void Start()
    {
        if (_selectedByDefault)
        {
            Select();
            RaiseButtonClickedEvent();
        }
    }

    public void Select()
    {
        _buttonBackground.color = _selectedColor;
        _arrow.SetActive(true);
    }

    public void Unselect()
    {
        _arrow.SetActive(false);
        _buttonBackground.color = _unselectedColor;
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        Select();
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
