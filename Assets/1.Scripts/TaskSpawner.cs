using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class TaskSpawner : MonoBehaviour
{
    public int TaskIndex => _taskIndex;
    public UnityEvent TaskUpdated;
    [SerializeField] private TMP_Text questionText;
    [SerializeField] private QuestionPanelController questionPanelController;
    
    private Options _options;
    private Topic _curTopic;
    private Theme _curTheme;
    private int _taskIndex;

    private Dictionary<Theme, AnswerOptionGroup> _themeMap = new Dictionary<Theme, AnswerOptionGroup>();
    private AnswerOptionGroup _curAnswerGroup;

    public static TaskSpawner Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        EventManager.TaskComplete.AddListener(OnTaskComplete);
        

        _options = GameConfig.Instance.Options;
        _options.OptionsChanged.AddListener(OnOptionsChanged);
        
        _curTopic = _options.Topic;
        _curTheme = _options.Theme;
        

        SpawnNewTask();
    }

    private void OnOptionsChanged()
    {
        if (_options.Topic != _curTopic || _options.Theme != _curTheme)
        {
            if (_options.Topic != _curTopic)
            {
                _taskIndex = 0;
            }
            
            _curTopic = _options.Topic;
            _curTheme = _options.Theme;
            
            _curAnswerGroup.gameObject.SetActive(false);
            SpawnNewTask();
        }
    }


    private void SpawnNewTask()
    {
        Task nextTask = _curTopic.Tasks[_taskIndex];
        
        questionPanelController.Init(nextTask);

        if (!_themeMap.ContainsKey(_curTheme))
        {
            _curAnswerGroup = Instantiate(_curTheme.optionGroupPrefab, transform);
            _themeMap.Add(_curTheme, _curAnswerGroup);
            _curAnswerGroup.Init(nextTask);
        }

        else
        {
            _curAnswerGroup = _themeMap[_curTheme];
            _curAnswerGroup.Init(nextTask);
        }

        _curAnswerGroup.gameObject.SetActive(true);
    }

    private void OnTaskComplete()
    {
        _taskIndex++;
        if (_taskIndex >= _curTopic.Tasks.Length)
        {
            if (_options.ResultScreen)
            {
                EventManager.GameComplete.Invoke();
            }
            else
            {
                _taskIndex = 0;
                SpawnNewTask();
                TaskUpdated.Invoke();
            }
            
        }
        else
        {
            SpawnNewTask();
            TaskUpdated.Invoke();
        }
    }
}
