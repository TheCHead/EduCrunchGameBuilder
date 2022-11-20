using UnityEngine;

public class Hat : MonoBehaviour
{
    
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetSprite(Sprite sprite)
    {
        _spriteRenderer.sprite = sprite;
    }

    public int GetOrder()
    {
        return _spriteRenderer.sortingOrder;
    }

    public void SetOrder(int order)
    {
        _spriteRenderer.sortingOrder = order;
    }
}
