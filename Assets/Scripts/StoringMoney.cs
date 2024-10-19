using UnityEngine;

public class StoringMoney : MonoBehaviour
{
    [SerializeField] private int m_AmountOfMoneyIfNoSaved = 0;

    private string m_KeyForPlayerPrefs = "Money";
    private int m_CurrentAmount;

    public int CurrentAmount { get => m_CurrentAmount; }

    public static StoringMoney instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        if (PlayerPrefs.HasKey(m_KeyForPlayerPrefs))
        {
            m_CurrentAmount = PlayerPrefs.GetInt(m_KeyForPlayerPrefs);
        }
        else
        {
            m_CurrentAmount = m_AmountOfMoneyIfNoSaved;
            PlayerPrefs.SetInt(m_KeyForPlayerPrefs, m_AmountOfMoneyIfNoSaved);
        }
    }

    public void AddMoney(int deltaMoney)
    {
        m_CurrentAmount += deltaMoney;
        if (m_CurrentAmount < 0)
        {
            m_CurrentAmount = 0;
        }
        PlayerPrefs.SetInt(m_KeyForPlayerPrefs, m_CurrentAmount);
        if (MoneyVisualizer.instance != null)
        {
            MoneyVisualizer.instance.ShowCurrentMoneyAmount();
        }
    }
}