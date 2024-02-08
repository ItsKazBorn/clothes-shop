using UnityEngine;

public class PlayerPreviewLayer : CharacterLayer
{
    [SerializeField] private UIImageExtension m_image;

    protected override void Start()
    {
        m_spriteHaver = m_image;
        base.Start();
    }
    
    
}
