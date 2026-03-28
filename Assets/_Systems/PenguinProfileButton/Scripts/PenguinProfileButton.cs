using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PenguinFriendButton : ContextButtonBase<PenguinContext>
{
    [SerializeField] private TMP_Text _statusTextRef;
    [SerializeField] private Image _penguinAvatar;
    [SerializeField] private GameObject _onlineIcon;
    [SerializeField] private GameObject _offlineIcon;

    public override void OnPointerClick(PointerEventData eventData)
    {
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


public class PenguinProfileButton : ContextButtonBase<PenguinContext>
{
    [SerializeField] protected TMP_Text _usernameTextRef;
    [SerializeField] protected TMP_Text _followerCounterTextRef;
    [SerializeField] private Image _penguinAvatar;


    public override void Initialize(PenguinContext contex)
    {
        base.Initialize(contex);

        var penguinData = contex.PenguinData;

        _penguinAvatar.sprite = penguinData.PenguinIcon;
        _usernameTextRef.SetText(penguinData.Username);
        _followerCounterTextRef.SetText($"{penguinData.FollowerCount} followers");
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
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
