using UnityEngine;

public class Cannon : WeaponWhichCanShoot
{
    [SerializeField] private Transform m_ObjectWhichDeterminesDirection;

    public void Shoot() => Shoot(m_ObjectWhichDeterminesDirection.rotation);
}