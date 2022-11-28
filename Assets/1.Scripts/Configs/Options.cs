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
    public bool AnswerSFX;
    public bool AnswerParticles;


    [Header("Pet Settings")]
    public PetConfig Pet;
    public bool PetAnimations;
    public bool ExtraPetAnimations;
    public bool PetTapReaction;
    public bool PetQuestionBubble;
    public bool PetFeedParticles;
    public bool ExtraPetParticles;
    public bool PetSFX;
    

    [Header("Audio")] 
    public bool Music;
    
    [Header("UI")] 
    public bool WinCounter;
    public bool ProgressBar;
    public bool Hint;
    public bool ResultScreen;

    [Header("Result Screen")]
    [TextArea(3, 10)]
    public string Contributors;

    private void OnValidate()
    {
        OptionsChanged.Invoke();
    }
}