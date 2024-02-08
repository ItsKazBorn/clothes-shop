using UnityEngine;

public class SpriteRendererExtension : MonoBehaviour, ISpriteHaver
{
    [SerializeField] private SpriteRenderer m_spriteRenderer;

    public Sprite GetSprite()
    {
        return m_spriteRenderer.sprite;
    }

    public string GetSpriteName()
    {
        return m_spriteRenderer.sprite.name;
    }

    public void SetSprite(Sprite newSprite)
    {
        m_spriteRenderer.sprite = newSprite;
    }

    public void SetSpriteEnabled(bool enabled)
    {
        if (!enabled) m_spriteRenderer.sprite = null;
    }

    public bool IsSpriteEnabled()
    {
        return m_spriteRenderer.enabled;
    }
}
