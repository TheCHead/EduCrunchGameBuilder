using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WinCounter : MonoBehaviour
{
    [SerializeField] private Image counterImage;
    [SerializeField] private TMP_Text counterText;
    [SerializeField] private float shakePower;

    private Options _options;

    private int _counter;

    private void Awake()
    {
        UpdateCounterText();
        EventManager.OnRightAnswerGiven.AddListener(OnRightAnswerGiven);
        EventManager.OnWrongAnswerGiven.AddListener(OnWrongAnswerGiven);
    }

    private void OnWrongAnswerGiven()
    {
        _counter--;
        _counter = _counter < 0 ? 0 : _counter;
        UpdateCounterText();
        transform.DOShakePosition(0.5f, shakePower);
    }

    private void Start()
    {
        _options = GameConfig.Instance.Options;
        _options.OptionsChanged.AddListener(OnOptionsChanged);
        counterImage.sprite = _options.Theme.counterSprite;
        gameObject.SetActive(_options.WinCounter);
    }

    private void OnOptionsChanged()
    {
        gameObject.SetActive(_options.WinCounter);
        counterImage.sprite = _options.Theme.counterSprite;
    }

    private void OnRightAnswerGiven()
    {
        _counter++;
        UpdateCounterText();
        transform.DOShakePosition(0.5f, shakePower);
    }

    private void UpdateCounterText()
    {
        counterText.text = $"x {_counter}";
    }
}
