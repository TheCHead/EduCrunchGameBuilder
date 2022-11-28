using System;
using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class AnswerButtonAbstract : MonoBehaviour, IPointerClickHandler
{
    public bool IsRight => _isRight;
    
    [SerializeField] private TMP_Text text;
    [SerializeField] private AudioClip rightSFX;
    [SerializeField] private AudioClip wrongSFX;
    [SerializeField] private ParticleSystem clickParticles;
    
    private protected bool _isRight;
    
    private Vector2 _startPos;
    private protected Vector2 _startScale;
    private protected SpriteRenderer _spriteRenderer;

    private protected Tween _scaleTween;
    private Collider2D _collider;

    private protected Options _options;
    
    

    protected virtual void Awake()
    {
        _startPos = transform.localPosition;
        _startScale = transform.localScale;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<Collider2D>();

        EventManager.OnRightAnswerGiven.AddListener(OnRightAnswerGiven);
    }

    private void OnRightAnswerGiven()
    {
        _collider.enabled = false;
        if (!_isRight && _options.AnswerEffects)
        {
            _scaleTween.Kill(true);
            _scaleTween = transform.DOScale(Vector3.zero, 0.5f);
        }
    }

    public virtual void Init(string text, bool isRight)
    {
        this.text.text = text;
        _isRight = isRight;
        transform.localPosition = _startPos;
        
        _scaleTween.Kill();

        _options = GameConfig.Instance.Options;

        if (_options.AnswerEffects)
        {
            _scaleTween.Kill(true);
            _scaleTween = transform.DOScale(_startScale, 1f).SetEase(Ease.OutBack);
        }
        else
        {
            transform.localScale = _startScale;
        }
        
        _spriteRenderer.color = Color.white;
        _collider.enabled = true;
    }

    public virtual void OnPointerClick(PointerEventData eventData)
    {
        _collider.enabled = false;
        
        if (_options.AnswerSFX)
        {
            AudioSource.PlayClipAtPoint(_isRight ? rightSFX : wrongSFX, Camera.main.transform.position);
        }
        
        if (_isRight)
        {
            EventManager.OnRightAnswerGiven.Invoke();

            if (_options.AnswerEffects)
            {
                _scaleTween.Kill(true);
                _scaleTween = transform.DOShakeScale(0.5f, 0.3f, 1);
                if (_options.AnswerParticles)
                {
                    clickParticles.Play();
                }
            }
        }

        else
        {
            EventManager.OnWrongAnswerGiven.Invoke();
            
            if (_options.AnswerEffects)
            {
                _spriteRenderer.color = Color.red;
                transform.DOShakePosition(0.5f, 0.2f);
            }
        }
    }

    public void HideAsWrong()
    {
        if (_options.AnswerEffects)
        {
            _spriteRenderer.color = Color.red;
            _scaleTween.Kill(true);
            _scaleTween = transform.DOScale(Vector3.zero, 0.5f);
        }

        else
        {
            transform.localScale = Vector3.zero;
        }
    }
}
