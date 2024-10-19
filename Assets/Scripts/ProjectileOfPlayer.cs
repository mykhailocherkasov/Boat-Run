using UnityEngine;

public class ProjectileOfPlayer : MonoBehaviour
{
    [SerializeField] private int m_LayerWithDamagingObjects = 7;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == m_LayerWithDamagingObjects)
        {
            Destroy(collision.gameObject);
        }
        Destroy(gameObject);
    }
}