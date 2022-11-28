using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class HIntButton : MonoBehaviour
{
    [SerializeField] private AudioClip clickAudioClip;
    
    private Button _button;
    private Options _options;
    private Tween _scaleTween;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnButtonClick);
    }
    
    private void OnDestroy()
    {
        _button.onClick.RemoveAllListeners();
    }
    
    private void Start()
    {
        _options = GameConfig.Instance.Options;
        _options.OptionsChanged.AddListener(OnOptionsChanged);
        gameObject.SetActive(_options.Hint);
    }

    private void OnOptionsChanged()
    {
        gameObject.SetActive(_options.Hint);
    }

    private void OnButtonClick()
    {
        EventManager.HintCalled.Invoke();
        _scaleTween.Kill(true);
        _scaleTween = transform.DOShakeScale(0.5f, 0.3f);
        AudioSource.PlayClipAtPoint(clickAudioClip, Camera.main.transform.position);
    }
}
