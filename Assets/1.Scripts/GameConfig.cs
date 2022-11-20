using System;
using UnityEngine;

public class GameConfig : MonoBehaviour
{
    public Options Options;
    public static GameConfig Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void OnDestroy()
    {
        Options.OptionsChanged.RemoveAllListeners();
    }
}
