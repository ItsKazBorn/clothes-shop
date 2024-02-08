using UnityEngine;

public class CharacterAnimationController : MonoBehaviour
{
    [SerializeField] private CharacterSpriteLayerController[] m_spriteLayerControllers;
    
    public void SetMovementParameters(Vector2 movement, float speed)
    {
        foreach (CharacterSpriteLayerController spriteLayer in m_spriteLayerControllers)
        {
            spriteLayer.SetMovementParameters(movement, speed);
        }
    }
}
