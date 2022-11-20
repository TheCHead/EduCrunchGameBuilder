using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class AnswerButtonHat : AnswerButtonAbstract
{
    [SerializeField] private Transform priceTag;
    private TrailRenderer _trail;
    private HatSelect _hat;

    protected override void Awake()
    {
        base.Awake();
        _trail = GetComponent<TrailRenderer>();
        _trail.emitting = false;
        _hat = GetComponent<HatSelect>();
    }

    public override void Init(string text, bool isRight)
    {
        base.Init(text, isRight);
        _hat.Init();
        priceTag.Rotate(Vector3.forward, Random.Range(-5f, 5f));
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);
        
        Pet pet = PetSpawner.Instance.GetPet();
        
        if (pet != null)
        {
            _trail.emitting = true;
            transform.DOJump(pet.GetHatPosition(), 5f, 1, 0.75f)
                .SetDelay(_options.AnswerEffects ? 0.5f : 0f)
                .SetEase(Ease.InSine)
                .OnPlay(()=> _trail.emitting = true)
                .OnComplete(() =>
                {
                    OnHatOn(pet);
                    if (_isRight)
                        EventManager.TaskComplete.Invoke();
                });
        }

        else
        {
            StartCoroutine(ClickRoutine());
        }
    }
    
    IEnumerator ClickRoutine()
    {
        if (_isRight)
        {
            if (_options.AnswerEffects)
            {
                yield return new WaitForSeconds(1.5f);
            }
            
            EventManager.TaskComplete.Invoke();
        }
    }

    private void OnHatOn(Pet pet)
    {
        _trail.emitting = false;
        _trail.Clear();
        transform.localScale = Vector3.zero;
        
        if (_options.AnswerSFX)
        {
            //AudioSource.PlayClipAtPoint(eatenAudio, Camera.main.transform.position);
        }
        
        pet.PutOnHat(_spriteRenderer.sprite, _isRight);
    }
}
