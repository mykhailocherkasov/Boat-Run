using UnityEngine;
using System;

public class UniformlyMovedObject : MonoBehaviour
{
    private float m_Speed;
    private Vector3 m_Displacement;
    private Vector3 m_Destination;
    private float m_QuantityOfTheIterationsOnThePath;
    private Vector3 m_SegmentOfTheWayForOneIteration;
    private Vector3 m_OvercomeDistance;
    private bool m_IsMoving;
    private Action m_AfterFinishingMoving;

    public void Move(Vector3 displacement, float speed, Action afterFinishingMoving = null)
    {
        m_Speed = speed;
        m_Displacement = displacement;
        m_Destination = transform.position + displacement;
        m_QuantityOfTheIterationsOnThePath = m_Displacement.magnitude / m_Speed;
        m_SegmentOfTheWayForOneIteration = m_Displacement / m_QuantityOfTheIterationsOnThePath;
        m_OvercomeDistance = Vector2.zero;
        m_AfterFinishingMoving = afterFinishingMoving;
        m_IsMoving = true;
    }

    private void Update()
    {
        if (m_IsMoving)
        {
            Vector3 DeltaPositionInThisIteration = m_SegmentOfTheWayForOneIteration * Time.deltaTime;
            transform.position += DeltaPositionInThisIteration;
            m_OvercomeDistance += DeltaPositionInThisIteration;
            if (m_OvercomeDistance.magnitude >= m_Displacement.magnitude)
            {
                transform.position = m_Destination;
                m_IsMoving = false;
                m_AfterFinishingMoving?.Invoke();
            }
        }
    }
}