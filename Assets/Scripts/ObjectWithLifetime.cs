using UnityEngine;

public class ObjectWithLifetime : MonoBehaviour
{
    [SerializeField] private float m_LifetimeInSeconds = 1;

    private void Awake()
    {
        Destroy(gameObject, m_LifetimeInSeconds);
    }
}