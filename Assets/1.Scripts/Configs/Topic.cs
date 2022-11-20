using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Topic", menuName = "ScriptableObjects/CreateTopicFile", order = 1)]
public class Topic : ScriptableObject
{
    public Task[] Tasks;
}

[Serializable]
public class Task
{
    public bool isAudio;
    public string Question;
    public AnswerOption[] AnswerOptions = new AnswerOption[4];
}


[Serializable]
public struct AnswerOption
{
    public string Answer;
    public bool IsRight;
}