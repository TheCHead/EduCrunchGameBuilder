using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestionPanelController : MonoBehaviour
{
    [SerializeField] private TextQuestionPanel textPanel;
    [SerializeField] private TextQuestionPanel textPanelPet;
    [SerializeField] private AudioQuestionPanel audioPanel;
    [SerializeField] private AudioQuestionPanel audioPanelPet;

    private Options _options;
    private Task _curTask;
    private PetConfig _curPet;
    private bool _usePetBubble;

    private void Start()
    {
        _options = GameConfig.Instance.Options;
        _usePetBubble = _options.PetQuestionBubble;
        _options.OptionsChanged.AddListener(OnOptionsChanged);
    }

    private void OnOptionsChanged()
    {
        if (_options.Pet != _curPet || _options.PetQuestionBubble != _usePetBubble)
        {
            _usePetBubble = _options.PetQuestionBubble;
            ResetPanels();
            Init(_curTask);
            _curPet = _options.Pet;
        }
    }

    public void Init(Task task)
    {
        ResetPanels();
        _curTask = task;
        if (task.isAudio)
        {
            if (_options.Pet != null && _usePetBubble)
                audioPanelPet.Init(task);
            else
                audioPanel.Init(task);
        }

        else
        {
            if (_options.Pet != null && _usePetBubble)
                textPanelPet.Init(task);
            else
                textPanel.Init(task);
        }
    }

    private void ResetPanels()
    {
        textPanel.Reset();
        textPanelPet.Reset();
        audioPanel.Reset();
        audioPanelPet.Reset();
    }
}
