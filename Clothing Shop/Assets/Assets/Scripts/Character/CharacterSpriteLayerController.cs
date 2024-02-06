using UnityEngine;

public class CharacterSpriteLayerController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer m_spriteRenderer;
    [SerializeField] private Animator m_animator;
    [SerializeField] private SpriteSwapper m_spriteSwapper;
    [SerializeField] private string m_baseSpriteSheetName;
    
    private string m_horizontalAxis = "Horizontal";
    private string m_verticalAxis = "Vertical";
    private string m_movementSpeed = "Speed";
    
    private void Start()
    {
        m_spriteSwapper.Setup(m_spriteRenderer);
        if (!string.IsNullOrEmpty(m_baseSpriteSheetName)) m_spriteSwapper.SetSpriteSheet(m_baseSpriteSheetName);
        else m_spriteRenderer.enabled = false;
    }

    public void SetMovementParameters(Vector2 movement, float speed)
    {
        m_animator.SetFloat(m_horizontalAxis, movement.x);
        m_animator.SetFloat(m_verticalAxis, movement.y);
        m_animator.SetFloat(m_movementSpeed, movement.magnitude * speed);
    }
}