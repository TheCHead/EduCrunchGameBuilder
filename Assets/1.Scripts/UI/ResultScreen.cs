using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResultScreen : MonoBehaviour
{
    [SerializeField] private TMP_Text contributorsText;
    private Options _options;
    
    private void Awake()
    {
        EventManager.GameComplete.AddListener(OnGameComplete);
    }

    private void Start()
    {
        _options = GameConfig.Instance.Options;
        gameObject.SetActive(false);
    }

    private void OnGameComplete()
    {
        gameObject.SetActive(true);
        contributorsText.text = "\n" + _options.Contributors;
    }
}
