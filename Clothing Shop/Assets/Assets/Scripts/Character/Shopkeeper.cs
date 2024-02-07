using System;
using TMPro;
using UnityEngine;

public class Shopkeeper : MonoBehaviour, IInteractible
{
    [SerializeField] private string m_prompt;

    public string InteractionPrompt => m_prompt;

    public bool Interact(Interactor interactor)
    {
        Debug.Log("Opening Shop");
        return true;
    }

}
