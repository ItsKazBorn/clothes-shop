using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CameraFollow : MonoBehaviour
{
    [Inject] private Camera m_camera;
    
    private Vector3 m_offset = new Vector3(0, 1.75f);
    private float m_damping = 0.1f;

    private Transform m_cameraTransform;
    private Transform m_target;
    private Vector3 speed = Vector3.zero;
    private Vector3 m_targetPosition;

    private void FixedUpdate()
    {
        if (m_target == null) m_target = transform;
        if (m_cameraTransform == null) m_cameraTransform = m_camera.transform;

        m_targetPosition = m_target.position + m_offset;
        m_targetPosition.z = m_cameraTransform.position.z;
        
        m_cameraTransform.position = Vector3.SmoothDamp(m_cameraTransform.position, m_targetPosition, ref speed, m_damping);
    }
}
