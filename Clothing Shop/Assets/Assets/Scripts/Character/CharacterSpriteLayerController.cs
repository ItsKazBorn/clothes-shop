using UnityEngine;
using Zenject;

public class CharacterSpriteLayerController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer m_spriteRenderer;
    [SerializeField] private Animator m_animator;
    [SerializeField] private string m_baseSpriteSheetName;
    
    public int SortingLayer { get; private set; }

    [Inject] private SpriteSheetManager m_spriteSheetManager;
    
    private SpriteSwapper m_spriteSwapper;
    
    private const string m_horizontalAxis = "Horizontal";
    private const string m_verticalAxis = "Vertical";
    private const string m_movementSpeed = "Speed";
    private bool initialized = false;
    
    public void Start()
    {
        m_spriteSwapper = new SpriteSwapper(m_spriteRenderer, m_spriteSheetManager);
        if (!string.IsNullOrEmpty(m_baseSpriteSheetName)) m_spriteSwapper.SetSpriteSheet(m_baseSpriteSheetName);
        else m_spriteRenderer.enabled = false;
        
        initialized = true;
    }

    public void SetMovementParameters(Vector2 movement, float speed)
    {
        m_animator.SetFloat(m_horizontalAxis, movement.x);
        m_animator.SetFloat(m_verticalAxis, movement.y);
        m_animator.SetFloat(m_movementSpeed, movement.magnitude * speed);
    }

    private void LateUpdate()
    {
        if (!initialized || !m_spriteRenderer.enabled) return;
        m_spriteSwapper.LateUpdate();
    }
}
