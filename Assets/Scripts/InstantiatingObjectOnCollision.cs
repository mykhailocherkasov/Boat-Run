using System.Collections.Generic;
using UnityEngine;

public class InstantiatingObjectOnCollision : MonoBehaviour
{
    [SerializeField] private GameObject m_ObjectToBeInstantiated;
    [SerializeField] private List<int> m_LayersOnWhichShouldBeInstantiated = new();

    private void OnCollisionEnter(Collision collision)
    {
        if (m_LayersOnWhichShouldBeInstantiated.Contains(collision.gameObject.layer))
        {
            Instantiate(m_ObjectToBeInstantiated, transform.position, Quaternion.identity);
        }
    }
}