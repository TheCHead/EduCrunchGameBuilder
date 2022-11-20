using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    public static OnRightAnswerGiven OnRightAnswerGiven = new OnRightAnswerGiven();
    public static OnWrongAnswerGiven OnWrongAnswerGiven = new OnWrongAnswerGiven();
    public static TaskComplete TaskComplete = new TaskComplete();
    public static HintCalled HintCalled = new HintCalled();
    public static GameComplete GameComplete = new GameComplete();
}

public class OnRightAnswerGiven : UnityEvent
{
    
}

public class OnWrongAnswerGiven : UnityEvent
{
    
}

public class TaskComplete : UnityEvent
{
    
}

public class HintCalled : UnityEvent
{
    
}

public class GameComplete : UnityEvent
{
    
}
