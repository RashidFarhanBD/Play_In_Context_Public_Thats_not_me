using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PenguinProfileButton : ContextButtonBase<PenguinContext>
{
    [SerializeField] protected TMP_Text _usernameTextRef;
    [SerializeField] protected TMP_Text _followerCounterTextRef;
    [SerializeField] private Image _penguinAvatar;
    [SerializeField] private GameObject _plusButton;
    [SerializeField] private GameObject _onlineIcon;


    public override void Initialize(PenguinContext contex)
    {
        base.Initialize(contex);

        var penguinData = contex.PenguinData;

        _penguinAvatar.sprite = penguinData.PenguinIcon;
        _usernameTextRef.SetText(penguinData.Username);
        _followerCounterTextRef.SetText($"{penguinData.FollowerCount} followers");
        _onlineIcon.SetActive(false);
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        RaiseButtonClickedEvent();
        _plusButton.SetActive(false);
        _onlineIcon.SetActive(true);
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
