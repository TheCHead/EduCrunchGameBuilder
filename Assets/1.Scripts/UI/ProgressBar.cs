using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private Image bar;
    [SerializeField] private float fillSpeed = 1f;
    [SerializeField] private float scaleAmount = 1f;

    private Options _options;
    private TaskSpawner _taskSpawner;

    private Tween _fillTween;
    private Tween _scaleTween;

    private void Start()
    {
        _options = GameConfig.Instance.Options;
        _options.OptionsChanged.AddListener(OnOptionsChanged);
        gameObject.SetActive(_options.ProgressBar);
        
        _taskSpawner = TaskSpawner.Instance;
        _taskSpawner.TaskUpdated.AddListener(Init);
        Init();
    }
    
    private void OnOptionsChanged()
    {
        gameObject.SetActive(_options.ProgressBar);
    }

    private void Init()
    {
        float fillAmount = (float)_taskSpawner.TaskIndex / _options.Topic.Tasks.Length;
        _fillTween.Kill(true);
        _fillTween = bar.DOFillAmount(fillAmount, fillSpeed);
        _scaleTween.Kill(true);
        _scaleTween = transform.DOShakeScale(fillSpeed, Vector3.one * scaleAmount, 3, 0);
    }
}
