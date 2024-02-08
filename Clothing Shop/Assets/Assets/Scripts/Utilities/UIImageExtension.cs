using UnityEngine;
using Image = UnityEngine.UI.Image;

public class UIImageExtension : MonoBehaviour, ISpriteHaver
{
    [SerializeField] private Image m_image;
    
    public Sprite GetSprite()
    {
        return m_image.sprite;
    }

    public string GetSpriteName()
    {
        return m_image.sprite.name;
    }

    public void SetSprite(Sprite newSprite)
    {
        m_image.sprite = newSprite;
    }

    public void SetSpriteEnabled(bool enabled)
    {
        m_image.enabled = enabled;
    }

    public bool IsSpriteEnabled()
    {
        return m_image.enabled;
    }
}
