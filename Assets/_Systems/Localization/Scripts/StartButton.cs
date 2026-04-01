using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartButton : ButtonBase
{
    [SerializeField] private GameObject _container;
    [SerializeField] private LanguageButton[] _languageButtons;
    [SerializeField] private GameObject _fade;
    [SerializeField] private Image _image;
    [SerializeField] private GameObject _text;

    private bool _used;

    public override void OnPointerClick(PointerEventData eventData)
    {
        if (_used) return;

        _container.SetActive(true);

        foreach (var languageButton in _languageButtons)
        {
            languageButton.OnButtonClicked += LoadScene;
        }

        _used = true;
        _text.SetActive(false);
        _image.enabled = false;
    }

    public override void OnPointerDown(PointerEventData eventData)
    {

    }

    public override void OnPointerUp(PointerEventData eventData)
    {

    }

    private void LoadScene()
    {
        StartCoroutine(SceneLoad());

        IEnumerator SceneLoad()
        {
            _fade.SetActive(true);
            yield return new WaitForSeconds(2f);
            SceneManager.LoadScene(1);
        }
    }
}
