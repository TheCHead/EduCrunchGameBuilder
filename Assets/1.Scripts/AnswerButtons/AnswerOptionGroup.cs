using System;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class AnswerOptionGroup : MonoBehaviour
{
    [SerializeField] private float vertMovement = 1.2f;
    private AnswerButtonAbstract[] _mButtons;
    private bool _hintApplied;
    private Options _options;

    private void Awake()
    {
        EventManager.HintCalled.AddListener(OnHintCalled);
    }

    private void Start()
    {
        _options = GameConfig.Instance.Options;
        _options.OptionsChanged.AddListener(OnOptionsChanged);
        SetPosition(0f);
    }

    private void OnOptionsChanged()
    {
        SetPosition(1f);
    }

    private void SetPosition(float moveTime)
    {
        if (_options.Pet != null && _options.PetQuestionBubble)
        {
            transform.DOLocalMoveY(vertMovement, moveTime);
        }
        else
        {
            transform.DOLocalMoveY(0f, moveTime);
        }
    }
    

    private void OnDestroy()
    {
        EventManager.HintCalled.RemoveListener(OnHintCalled);
    }

    private void OnHintCalled()
    {
        if (_hintApplied)
            return;
        ApplyHint();
    }

    public void Init(Task task)
    {
        _hintApplied = false;
        
        _mButtons ??= GetComponentsInChildren<AnswerButtonAbstract>();

        for (int i = 0; i < _mButtons.Length && i < task.AnswerOptions.Length; i++)
        {
            _mButtons[i].Init(task.AnswerOptions[i].Answer, task.AnswerOptions[i].IsRight);
        }
    }

    private void ApplyHint()
    {
        _hintApplied = true;
        AnswerButtonAbstract[] wrongAnswers = _mButtons.Where(x => x.IsRight == false).ToArray();
        wrongAnswers = Utilities.ShuffleArray(wrongAnswers);
        for (int i = 0; i < 2 && i < wrongAnswers.Length - 1; i++)
        {
            wrongAnswers[i].HideAsWrong();
        }
    }
}
