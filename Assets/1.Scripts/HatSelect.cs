using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatSelect : MonoBehaviour
{
    [SerializeField] private Sprite[] hatPool;
    private SpriteRenderer _spriteRenderer;
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Init()
    {
        SelectHat();
    }
    
    private void SelectHat()
    {
        _spriteRenderer.sprite = hatPool[Random.Range(0, hatPool.Length)];
    }
}
