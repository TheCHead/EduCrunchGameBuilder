using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class TextQuestionPanel : MonoBehaviour, IQuestionPanel
{
    [SerializeField] private TMP_Text text;
    private Tween _scaleTween;

    private void Awake()
    {
        EventManager.OnRightAnswerGiven.AddListener(OnRightAnswerGiven);
    }

    private void OnDestroy()
    {
        EventManager.OnRightAnswerGiven.RemoveListener(OnRightAnswerGiven);
    }

    private void OnRightAnswerGiven()
    {
        _scaleTween.Kill();
        _scaleTween = transform.DOScale(Vector3.zero, 0.5f);
    }

    private void Start()
    {
        Reset();
    }

    public void Init(Task task)
    {
        gameObject.SetActive(true);
        text.text = task.Question;
        _scaleTween.Kill();
        _scaleTween = transform.DOScale(Vector3.one, 0.5f)
            .SetEase(Ease.OutBack);
    }
    
    public void Reset()
    {
        gameObject.SetActive(false);
        transform.localScale = Vector3.zero;
    }
}