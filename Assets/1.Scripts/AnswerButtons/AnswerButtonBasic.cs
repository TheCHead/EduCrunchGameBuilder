using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class AnswerButtonBasic : AnswerButtonAbstract
{
    protected override void Awake()
    {
        base.Awake();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);

        StartCoroutine(ClickRoutine());
    }

    IEnumerator ClickRoutine()
    {
        if (_isRight)
        {
            if (_options.AnswerEffects)
            {
                _spriteRenderer.color = Color.green;
                yield return new WaitForSeconds(1.5f);
            }
            
            EventManager.TaskComplete.Invoke();
        }
    }
}
