using UnityEngine;

public class UncontrolledCannon : Cannon
{
    [SerializeField] private float m_IntervalBetweenShotsInSeconds = 3;

    private void Awake()
    {
        InvokeRepeating(nameof(Shoot), m_IntervalBetweenShotsInSeconds, m_IntervalBetweenShotsInSeconds);
    }

    private new void Shoot()
    {
        if (gameObject.activeInHierarchy)
        {
            base.Shoot();
        }
    }
}