using UnityEngine;

public class CharacterSpriteLayerController : CharacterLayer
{
    [SerializeField] private SpriteRendererExtension m_spriteRenderer;
    [SerializeField] private Animator m_animator;
    
    private const string m_horizontalAxis = "Horizontal";
    private const string m_verticalAxis = "Vertical";
    private const string m_movementSpeed = "Speed";
    
    protected override void Start()
    {
        m_spriteHaver = m_spriteRenderer;
        base.Start();
    }

    public void SetMovementParameters(Vector2 movement, float speed)
    {
        m_animator.SetFloat(m_horizontalAxis, movement.x);
        m_animator.SetFloat(m_verticalAxis, movement.y);
        m_animator.SetFloat(m_movementSpeed, movement.magnitude * speed);
    }
}
