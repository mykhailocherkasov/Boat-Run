using UnityEngine;

public class ObjectWhichDamagesPlayer : MonoBehaviour
{
    [SerializeField] private int m_Damage;
    public int Damage { get => m_Damage; }
}