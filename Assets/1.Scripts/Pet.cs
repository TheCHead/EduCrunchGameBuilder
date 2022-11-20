using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Spine.Unity;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

public class Pet : MonoBehaviour, IPointerClickHandler
{
    [field:SerializeField] public Transform Mouth { get; private set; }
    
    [Header("Particles")]
    [SerializeField] private ParticleSystem EatingParticles;
    [SerializeField] private ParticleSystem TapParticles;
    [SerializeField] private ParticleSystem WinParticles;

    [Header("Audio")] 
    [SerializeField] private AudioClip winAudio;
    [SerializeField] private AudioClip eatGood;
    [SerializeField] private AudioClip eatBad;
    [SerializeField] private AudioClip tapAudio;
    [SerializeField] private AudioClip hatFailAudio;
    [SerializeField] private AudioClip smileAudio;
    [SerializeField] private AudioClip sadAudio;

    [Header("Hats")] 
    [SerializeField] private Hat hatPrefab;
    [SerializeField] private Transform hatStack;
    [SerializeField] private float hatStep = 1.2f;
    private Hat _lastHat;
    
    
    
    private SkeletonAnimation _animation;
    private Options _options;
    private Transform _cameraTransform;

    private Tween _scaleTween;

    private void Awake()
    {
        _animation = GetComponent<SkeletonAnimation>();
    }

    private void Start()
    {
        _options = GameConfig.Instance.Options;
        _cameraTransform = Camera.main.transform;
    }

    public Vector3 GetHatPosition()
    {
        return _lastHat == null ? hatStack.position : _lastHat.transform.position;
    }

    public void PutOnHat(Sprite hat, bool good)
    {
        if (good)
        {
            //PlayAudioClip(eatGood, 0f);
            
            Hat newHat = Instantiate(hatPrefab, hatStack);
            Vector3 newPos = _lastHat != null ? _lastHat.transform.localPosition + Vector3.up * hatStep : Vector3.zero;
            newHat.transform.localPosition = newPos;
            newHat.transform.Rotate(Vector3.forward, Random.Range(-30, 30));
            newHat.SetSprite(hat);
        
            if (_lastHat != null)
                newHat.SetOrder(_lastHat.GetOrder() + 1);
        
        
            _lastHat = newHat;

            _scaleTween.Kill();
            _scaleTween = transform.DOScale(transform.localScale * 0.93f, 1f);
            
        }
        
        else
        {
            
            PlayAudioClip(hatFailAudio, 0f);
            
            foreach (Transform h in hatStack.transform)
            {
                h.DOJump(new Vector3(Random.Range(-10f, 10f), -10, 0), 10f, 1, 1f)
                    .SetEase(Ease.InSine)
                    .OnComplete(() =>
                    {
                        Destroy(h.gameObject);
                    });
            }

            _lastHat = null;
            _scaleTween.Kill();
            _scaleTween = transform.DOScale(Vector3.one, 3f);
        }

        if (_options.PetAnimations)
        {
            if (_options.ExtraPetAnimations)
            {
                _animation.AnimationState.SetAnimation(0, good ? "hat" : "cry", false);
                _animation.AnimationState.AddAnimation(0, "idle", true, 0);
            }
            else
            {
                _animation.AnimationState.SetAnimation(0, good ? "smile" : "sad", false);
                _animation.AnimationState.AddAnimation(0, "idle", true, 0);
            }
        }

        if (_options.PetSFX)
        {
            if (good)
            {
                PlayAudioClip(winAudio, 0f);
                PlayAudioClip(_options.ExtraPetAnimations ? eatGood : smileAudio, 0f);
            }
            else
            {
                PlayAudioClip(_options.ExtraPetAnimations ? eatBad : sadAudio, 0f);
            }
        }
        
        if (good && _options.ExtraPetParticles)
        {
            WinParticles.Play();
        }
    }

    public void Eat(bool isGood)
    {
        if (_options.PetAnimations)
        {
            StartCoroutine(EatRoutine(isGood));
        }
    }

    IEnumerator EatRoutine(bool isGood)
    {
        yield return new WaitForSeconds(_options.AnswerEffects ? 0.75f : 0.25f);

        if (_options.ExtraPetAnimations)
        {
            _animation.AnimationState.SetAnimation(0, isGood ? "good" : "bad", false);
            _animation.AnimationState.AddAnimation(0, "idle", true, 0);
        }
        else
        {
            _animation.AnimationState.SetAnimation(0, isGood ? "smile" : "sad", false);
            _animation.AnimationState.AddAnimation(0, "idle", true, 0);
        }
        

        if (_options.PetFeedParticles)
            EatingParticles.Play();
        
        if (isGood && _options.ExtraPetParticles)
        {
            yield return new WaitForSeconds(0.5f);
            WinParticles.Play();
        }
        
        if (_options.PetSFX)
        {
            if (isGood)
            {
                PlayAudioClip(winAudio, 0f);
                PlayAudioClip(_options.ExtraPetAnimations ? eatGood : smileAudio, _options.ExtraPetAnimations ? 1.5f : 0f);
            }
            else
            {
                PlayAudioClip(_options.ExtraPetAnimations ? eatBad : sadAudio, _options.ExtraPetAnimations ? 2f : 0.5f);
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_options.PetTapReaction)
        {
            _scaleTween = transform.DOShakeScale(0.5f, 0.2f).OnComplete(() =>
            {
                transform.localScale = Vector3.one;
            });

            if (_options.ExtraPetParticles)
            {
                TapParticles.Play();
            }

            if (_options.PetSFX)
            {
                PlayAudioClip(tapAudio, 0f);
            }
        }
    }

    private void PlayAudioClip(AudioClip clip, float delay)
    {
        StartCoroutine(PlayAudioRoutine(clip, delay));
    }

    IEnumerator PlayAudioRoutine(AudioClip clip, float delay)
    {
        yield return new WaitForSeconds(delay);
        AudioSource.PlayClipAtPoint(clip, _cameraTransform.position, 0.25f);
    }
}
