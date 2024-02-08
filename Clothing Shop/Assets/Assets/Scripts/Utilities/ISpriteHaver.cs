using UnityEngine;

public interface ISpriteHaver
{
    public Sprite GetSprite();

    public string GetSpriteName();

    public void SetSprite(Sprite newSprite);

    public void SetSpriteEnabled(bool enabled);

    public bool IsSpriteEnabled();
}
