using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private int m_Cost = 1;
    public int Cost { get => m_Cost; }
}