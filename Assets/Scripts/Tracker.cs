using UnityEngine;

public class Tracker : MonoBehaviour
{
    [SerializeField] private Transform m_TrackedObject;
    [SerializeField] private Vector3 m_Offset;
    [SerializeField] private bool m_ShouldTrackByX = true;
    [SerializeField] private bool m_ShouldTrackByY = true;
    [SerializeField] private bool m_ShouldTrackByZ = true;

    private void LateUpdate()
    {
        Vector3 NewPosition = transform.position;
        if(m_ShouldTrackByX)
        {
            NewPosition.x = m_TrackedObject.position.x + m_Offset.x;
        }
        if(m_ShouldTrackByY)
        {
            NewPosition.y = m_TrackedObject.position.y + m_Offset.y;
        }
        if(m_ShouldTrackByZ)
        {
            NewPosition.z = m_TrackedObject.position.z + m_Offset.z;
        }
        transform.position = NewPosition;
    }
}