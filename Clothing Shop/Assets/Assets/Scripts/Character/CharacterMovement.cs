using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private CharacterAnimationController m_animationController;
    [SerializeField] private Rigidbody2D m_rigidBody2D;
    [SerializeField] private float m_walkSpeed = 10;
    [SerializeField] private float m_runSpeed = 20;
    
    private Vector2 m_movement = Vector2.zero;
    private float m_moveSpeed = 10;

    public void SetRunning(bool running)
    {
        m_moveSpeed = running ? m_runSpeed : m_walkSpeed;
    }
    
    public void SetMovement(Vector2 newMovement)
    {
        m_movement = newMovement;
        m_animationController.SetMovementParameters(newMovement, m_moveSpeed);
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        m_rigidBody2D.MovePosition(m_rigidBody2D.position + m_movement * (m_moveSpeed * Time.deltaTime));
    }
}
