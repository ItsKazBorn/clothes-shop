
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputs : MonoBehaviour
{
    [SerializeField] private CharacterMovement m_characterMovement;

    private void OnRun(InputValue value)
    {
        Debug.Log(value.isPressed);
        m_characterMovement.SetRunning(value.isPressed);
    }

    private void OnMove(InputValue value)
    {
        m_characterMovement.SetMovement(value.Get<Vector2>());
    }

    private void OnInteract(InputValue value)
    {
        Debug.Log("Interacting...");
    }
}
