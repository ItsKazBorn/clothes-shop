using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class PlayerInputs : MonoBehaviour
{
    [Inject] private SignalBus m_signalBus;
    
    [SerializeField] private CharacterMovement m_characterMovement;
    [SerializeField] private Interactor m_interactor;
    [SerializeField] private PlayerInput m_playerInput;

    private const string m_uiMap = "UI";
    private const string m_playerMap = "Player";

    private void Start()
    {
        m_signalBus.Subscribe<OnClothingShopOpenedSignal>(OnShopOpened);
        m_signalBus.Subscribe<OnClothingShopClosedSignal>(OnShopClosed);
    }

    private void OnDestroy()
    {
        m_signalBus.Unsubscribe<OnClothingShopOpenedSignal>(OnShopOpened);
        m_signalBus.Unsubscribe<OnClothingShopClosedSignal>(OnShopClosed);
    }

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
        m_interactor.Interact();
    }

    private void OnShopOpened(OnClothingShopOpenedSignal args)
    {
        SetMapInputsEnabled(false);
    }
    
    private void OnShopClosed()
    {
        SetMapInputsEnabled(true);
    }

    private void SetMapInputsEnabled(bool enabled)
    {
        m_playerInput.SwitchCurrentActionMap(enabled ? m_playerMap : m_uiMap);
    }
}
