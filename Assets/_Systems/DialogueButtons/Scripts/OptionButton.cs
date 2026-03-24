using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class OptionButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
    [SerializeField] private TMP_Text _displayTextRef;

    private Action _onOptionChosen;

    public void Initialize(string displayText, Action actionOnChoose)
    {
        gameObject.SetActive(true);
        _displayTextRef.SetText(displayText);
        _onOptionChosen = actionOnChoose;
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
        _onOptionChosen?.Invoke();
    }
}
