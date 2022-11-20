using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class AnswerButtonFood : AnswerButtonAbstract
{
    [SerializeField] private AudioClip eatenAudio;
    private TrailRenderer _trail;

    protected override void Awake()
    {
        base.Awake();
        _trail = GetComponent<TrailRenderer>();
        _trail.emitting = false;
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);
        
        Pet pet = PetSpawner.Instance.GetPet();
        
        if (pet != null)
        {
            pet.Eat(_isRight);

            _trail.emitting = true;
            transform.DOJump(pet.Mouth.position, 5f, 1, 0.75f)
                .SetDelay(_options.AnswerEffects ? 0.5f : 0f)
                .SetEase(Ease.InSine)
                .OnPlay(()=> _trail.emitting = true)
                .OnComplete(() =>
                {
                    OnEaten();
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

    private void OnEaten()
    {
        _trail.emitting = false;
        _trail.Clear();
        transform.localScale = Vector3.zero;
        if (_options.AnswerSFX)
        {
            AudioSource.PlayClipAtPoint(eatenAudio, Camera.main.transform.position);
        }
    }
}
