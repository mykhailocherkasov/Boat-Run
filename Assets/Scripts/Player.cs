using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    [SerializeField] private Axis m_AxisWithConstantMotion = Axis.Z;
    [SerializeField] private Axis m_AxisControlledByPlayer = Axis.X;
    [SerializeField] private float m_MinimalValueOnAxisControlledByPlayer;
    [SerializeField] private float m_MaximalValueOnAxisControlledByPlayer;
    [SerializeField] private string m_InputNameForAxisControlledByPlayer = "Mouse X";
    [SerializeField] private float m_SpeedOfConstantMotion = 5;
    [SerializeField] private float m_SpeedOfMovingControlledByPlayer = 5;
    [SerializeField] private int m_DefaultScreenWidth = 1000;
    [SerializeField] private bool m_WasConstantMotionStopped = false;
    [SerializeField] private bool m_WasControlledMotionStopped = false;
    [SerializeField] private int m_LayerWithVictoryZone = 6;
    [SerializeField] private int m_LayerWithDamagingObjects = 7;
    [SerializeField] private int m_LayerWithCoins = 8;
    [SerializeField] private int m_LayerWithBossShipZone = 9;
    [SerializeField] private ShowingVictory m_ShowingVictory;
    [SerializeField] private ShowingDefeat m_ShowingDefeat;
    [SerializeField] private int m_Health = 100;
    [SerializeField] private TextMeshPro m_TextWithHealth;
    [SerializeField] private List<GameObject> m_ObjectsWhichShouldBeDisabledBeforeBossFight = new();

    private Rigidbody m_ThisRigidbody;
    private Vector3 m_AxisWithConstantMotionAsVector;
    private Vector3 m_AxisControlledByPlayerAsVector;
    private int m_AxisControlledByPlayerAsInt;

    public float ArmorCoefficient { get; set; }

    public int Health
    {
        get => m_Health;
        private set
        {
            m_Health = value;
            if(m_TextWithHealth != null)
            {
                m_TextWithHealth.text = Mathf.Max(m_Health, 0).ToString();
            }
            if(m_Health <= 0)
            {
                m_Health = 0;
                StopConstantMotion();
                m_ShowingDefeat.ShowDefeat();
            }
        }
    }

    private void Awake()
    {
        m_ThisRigidbody = GetComponent<Rigidbody>();
        m_SpeedOfMovingControlledByPlayer /= Mathf.Sqrt((float)m_DefaultScreenWidth / Screen.width);
        m_AxisWithConstantMotionAsVector = AxisConversion.AxisToVector3(m_AxisWithConstantMotion);
        m_AxisControlledByPlayerAsVector = AxisConversion.AxisToVector3(m_AxisControlledByPlayer);
        m_AxisControlledByPlayerAsInt = AxisConversion.AxisToInt(m_AxisControlledByPlayer);
        ResetVelocity();
    }

    private void ResetVelocity()
    {
        if (!m_WasConstantMotionStopped)
        {
            m_ThisRigidbody.velocity = m_SpeedOfConstantMotion * m_AxisWithConstantMotionAsVector;
        }
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && !m_WasControlledMotionStopped)
        {
            float InputValue = Input.GetAxis(m_InputNameForAxisControlledByPlayer);
            float OffsetOnAxisControlledByPlayer = InputValue * m_SpeedOfMovingControlledByPlayer * Time.deltaTime;
            float NewPositionOnAxisControlledByPlayerAsInt = transform.position[m_AxisControlledByPlayerAsInt] + OffsetOnAxisControlledByPlayer;
            if (NewPositionOnAxisControlledByPlayerAsInt >= m_MinimalValueOnAxisControlledByPlayer && NewPositionOnAxisControlledByPlayerAsInt <= m_MaximalValueOnAxisControlledByPlayer)
            {
                transform.Translate(OffsetOnAxisControlledByPlayer * m_AxisControlledByPlayerAsVector);
            }
        }
        ResetVelocity();
    }

    public void StopConstantMotion()
    {
        if (m_ThisRigidbody != null)
        {
            m_ThisRigidbody.velocity = Vector3.zero;
        }
        m_WasConstantMotionStopped = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        int LayerOfTheOtherObject = collision.gameObject.layer;
        if (LayerOfTheOtherObject == m_LayerWithVictoryZone)
        {
            StopConstantMotion();
            m_ShowingVictory.ShowVictory();
        }
        else if (LayerOfTheOtherObject == m_LayerWithDamagingObjects)
        {
            Health -= (int)(collision.gameObject.GetComponent<ObjectWhichDamagesPlayer>().Damage * ArmorCoefficient);
            if (Health > 0)
            {
                Destroy(collision.gameObject);
                ResetVelocity();
            }
        }
        else if (LayerOfTheOtherObject == m_LayerWithCoins)
        {
            StoringMoney.instance.AddMoney(collision.gameObject.GetComponent<Coin>().Cost);
            Destroy(collision.gameObject);
        }
        else if (LayerOfTheOtherObject == m_LayerWithBossShipZone)
        {
            collision.gameObject.SetActive(false);
            m_ObjectsWhichShouldBeDisabledBeforeBossFight.ForEach(oneObject => oneObject.SetActive(false));
            StopConstantMotion();
            Destroy(m_ThisRigidbody);
            FindAnyObjectByType<BossShip>().StartAttack();
        }
    }
}