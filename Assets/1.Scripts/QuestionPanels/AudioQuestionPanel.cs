﻿using System;
using System.IO;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class AudioQuestionPanel : MonoBehaviour, IQuestionPanel, IPointerClickHandler
{
    private Tween _scaleTween;
    private AudioClip _clip;
    private Options _options;

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
        _options = GameConfig.Instance.Options;
        Reset();
    }

    public void Init(Task task)
    {
        gameObject.SetActive(true);

        _clip = Resources.Load<AudioClip>(Path.Combine("Audio", task.Question));
        _scaleTween.Kill();
        if (_options.AnswerEffects)
        {
            _scaleTween = transform.DOScale(Vector3.one, 0.5f)
                .SetEase(Ease.OutBack)
                .OnComplete(() =>
                {
                    PlayClip();
                });
        }
        else
        {
            transform.localScale = Vector3.one;
            PlayClip();
        }
    }


    public void Reset()
    {
        gameObject.SetActive(false);
        transform.localScale = Vector3.zero;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        PlayClip();
    }

    private void PlayClip()
    {
        if (_clip != null)
        {
            AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position);
            if (_options.AnswerEffects)
            {
                transform.DOShakeScale(0.3f, 0.3f, 5);
            }
        }
    }
}