using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RestartButton : MonoBehaviour
{
    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnButtonClick);
    }
    
    private void OnDestroy()
    {
        _button.onClick.RemoveAllListeners();
    }

    private void OnButtonClick()
    {
        SceneManager.LoadScene(0);
    }
}
