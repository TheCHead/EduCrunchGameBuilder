using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HIntButton : MonoBehaviour
{
    private Button _button;
    private Options _options;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnButtonClick);
    }
    
    private void OnDestroy()
    {
        _button.onClick.RemoveAllListeners();
    }
    
    private void Start()
    {
        _options = GameConfig.Instance.Options;
        _options.OptionsChanged.AddListener(OnOptionsChanged);
        gameObject.SetActive(_options.Hint);
    }

    private void OnOptionsChanged()
    {
        gameObject.SetActive(_options.Hint);
    }

    private void OnButtonClick()
    {
        EventManager.HintCalled.Invoke();
    }
}
