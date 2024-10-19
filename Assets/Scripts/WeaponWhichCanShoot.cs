using UnityEngine;

public class WeaponWhichCanShoot : MonoBehaviour
{
    [SerializeField] private GameObject m_TemplateProjectile;
    [SerializeField] private Vector3 m_ProjectileScale = Vector3.one;
    [SerializeField] private GameObject m_StartPlaceForNewProjectiles;
    [SerializeField] private float m_Force = 1;
    [SerializeField] private Vector3 m_BaseVectorForDirection;

    public void Shoot(Quaternion direction)
    {
        GameObject NewProjectile = Instantiate(m_TemplateProjectile);
        NewProjectile.transform.localScale = m_ProjectileScale;
        NewProjectile.transform.position = m_StartPlaceForNewProjectiles.transform.position;
        NewProjectile.GetComponent<Rigidbody>().AddForce(direction * m_BaseVectorForDirection * m_Force);
    }
}