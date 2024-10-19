using UnityEngine;
using TMPro;

public class BossShip : MonoBehaviour
{
    [SerializeField] private UniformlyMovedObject m_UniformlyMovedObject;
    [SerializeField] private float m_MovingSpeed = 20;
    [SerializeField] private Vector3 m_DisplacementToFirstAttackingPosition;
    [SerializeField] private Vector3 m_DisplacementBetweenFirstAndSecondAttackingPositions;
    [SerializeField] private int m_LayerWithPlayerProjectiles = 10;
    [SerializeField] private int m_NeededQuantityOfShotsToGoAway = 5;
    [SerializeField] private TextMeshPro m_TextWithQuantityOfShots;

    private bool m_IsGoingAway = false;
    private ShowingVictory m_ShowingVictory;
    private Vector3 m_StartingPosition;
    private int m_QuantityOfShotsInTheShip = 0;

    private void Awake()
    {
        UpdateTextWithQuantityOfShots();
        m_StartingPosition = transform.position;
        m_ShowingVictory = FindAnyObjectByType<ShowingVictory>();
    }

    public void StartAttack()
    {
        m_UniformlyMovedObject.Move(m_DisplacementToFirstAttackingPosition, m_MovingSpeed, () => MoveInCycle(m_DisplacementBetweenFirstAndSecondAttackingPositions));
    }

    private void MoveInCycle(Vector3 displacement)
    {
        if (m_IsGoingAway)
        {
            GoAway();
        }
        else
        {
            m_UniformlyMovedObject.Move(displacement, m_MovingSpeed, () =>
            {
                m_UniformlyMovedObject.Move(-displacement, m_MovingSpeed, () => MoveInCycle(displacement));
            });
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == m_LayerWithPlayerProjectiles)
        {
            m_QuantityOfShotsInTheShip++;
            UpdateTextWithQuantityOfShots();
            if (m_QuantityOfShotsInTheShip >= m_NeededQuantityOfShotsToGoAway)
            {
                GoAway();
            }
        }
    }

    public void GoAway()
    {
        m_UniformlyMovedObject.Move(m_StartingPosition - transform.position, m_MovingSpeed, () => m_ShowingVictory.ShowVictory());
    }

    private void UpdateTextWithQuantityOfShots()
    {
        m_TextWithQuantityOfShots.text = $"{m_QuantityOfShotsInTheShip}/{m_NeededQuantityOfShotsToGoAway}";
    }
}