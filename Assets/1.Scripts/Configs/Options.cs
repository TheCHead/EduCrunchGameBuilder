using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Options", menuName = "ScriptableObjects/CreateOptionsFile", order = 0)]
public class Options : ScriptableObject
{
    [HideInInspector]
    public UnityEvent OptionsChanged;

    [Header("Topic Select")]
    public Topic Topic;
    [Header("Theme Select")]
    public Theme Theme;

    [Header("Answer Feel")] 
    public bool AnswerEffects;
    
    
    [Header("Pet Settings")]
    public PetConfig Pet;
    public bool PetAnimations;
    public bool PetTapReaction;
    public bool ExtraPetParticles;
    public bool PetQuestionBubble;
    public bool ExtraPetAnimations;
    public bool PetFeedParticles;

    [Header("Audio")] 
    public bool Music;
    public bool AnswerSFX;
    public bool PetSFX;

    [Header("UI")] 
    public bool WinCounter;
    public bool ProgressBar;
    public bool Hint;
    public bool ResultScreen;

    [Header("Result Screen")]
    public bool ShowContributors;
    [TextArea(3, 10)]
    public string Contributors;

    private void OnValidate()
    {
        OptionsChanged.Invoke();
    }
}