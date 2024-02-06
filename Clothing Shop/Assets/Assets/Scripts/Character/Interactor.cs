using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField] private Transform m_interactionPoint;
    [SerializeField] private float m_interactionRadius = 0.5f;
    [SerializeField] private LayerMask m_interactableMask;

    private readonly Collider2D[] m_colliders = new Collider2D[3];
    [SerializeField] private int m_numFound;

    public void Interact()
    {
        m_numFound = Physics2D.OverlapCircleNonAlloc(m_interactionPoint.position, m_interactionRadius, m_colliders,
            m_interactableMask);
        
        
        if (m_numFound > 0)
        {
            IInteractible interactible;
            bool isInteractible = m_colliders[0].TryGetComponent<IInteractible>(out interactible);
            
            if (isInteractible)
            {
                interactible.Interact(this);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(m_interactionPoint.position, m_interactionRadius);
    }
}
