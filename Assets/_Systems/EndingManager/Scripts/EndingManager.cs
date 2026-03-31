using DG.Tweening;
using TMPro;
using UnityEngine;

public class EndingManager : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private TMP_Text _failText;
    [SerializeField] private TMP_Text _successText;
    [SerializeField] private TMP_Text _lastMessageText;

    [Header("Settings")]
    [SerializeField] private float _fadeTime = 1f;
    [SerializeField] private float _fadeDelay = 1f;
    [SerializeField] private float _textFadeTime = 2f;
    [SerializeField] private Ease _fadeEase;

    protected void Awake()
    {
        GameEvent.OnEndgameTriggered += InitiateEnding;
    }

    void OnDestroy()
    {
        GameEvent.OnEndgameTriggered -= InitiateEnding;
    }

    private void Start()
    {
        _canvasGroup.gameObject.SetActive(false);
        _canvasGroup.alpha = 0;
        _failText.alpha = 0;
        _successText.alpha = 0;
    }

    private void InitiateEnding(EndingType endingType)
    {
        TMP_Text endingText = null;

        switch (endingType)
        {
            case EndingType.Good:
                endingText = _successText;
                break;
            case EndingType.Bad:
                endingText = _failText;
                break;
        }

        _canvasGroup.gameObject.SetActive(true);
        var sequence = DOTween.Sequence();

        var canvasTween = _canvasGroup.DOFade(1, _fadeTime).SetEase(_fadeEase).From(0f).SetDelay(_fadeDelay);
        var endingTextFadeInTween = endingText.DOFade(1, _textFadeTime).From(0f).SetDelay(2);
        var endingTextFadeOutTween = endingText.DOFade(0, _textFadeTime).SetDelay(_fadeDelay * 1.5f);
        var textTween = _lastMessageText.DOFade(1, _textFadeTime).SetEase(_fadeEase).From(0f).SetDelay(_fadeDelay);

        sequence.Join(canvasTween)
        .Append(endingTextFadeInTween)
        .Append(endingTextFadeOutTween)
        .Append(textTween);
    }

}
