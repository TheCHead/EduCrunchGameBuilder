using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    private AudioSource _audioSource;
    private Options _options;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        _options = GameConfig.Instance.Options;
        _options.OptionsChanged.AddListener(OnOptionsChanged);
        if (_options.Music)
            _audioSource.Play();
    }

    private void OnOptionsChanged()
    {
        if (_options.Music)
        {
            if (!_audioSource.isPlaying)
            {
                _audioSource.Play();
            }
        }
        
        else
        {
            _audioSource.Stop();
        }
    }
}
