using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[RequireComponent(typeof(Button))]
public class ShopBlock : MonoBehaviour
{
    [SerializeField] private int m_Cost;
    [SerializeField] private string m_KeyForPlayerPrefs;
    [SerializeField] private UnityEvent m_WhenMoneyIsNotEnough;

    private void Awake()
    {
        if (PlayerPrefs.HasKey(m_KeyForPlayerPrefs))
        {
            gameObject.SetActive(PlayerPrefs.GetInt(m_KeyForPlayerPrefs) == 0);
        }
        else
        {
            PlayerPrefs.SetInt(m_KeyForPlayerPrefs, 0);
        }
        GetComponent<Button>().onClick.AddListener(TryToUnblock);
    }

    private void TryToUnblock()
    {
        if (StoringMoney.instance.CurrentAmount >= m_Cost)
        {
            StoringMoney.instance.AddMoney(-m_Cost);
            gameObject.SetActive(false);
            PlayerPrefs.SetInt(m_KeyForPlayerPrefs, 1);
        }
        else
        {
            m_WhenMoneyIsNotEnough.Invoke();
        }
    }
}