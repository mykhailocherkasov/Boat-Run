using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MoneyVisualizer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_TextWithMoneyAmount;

    public static MoneyVisualizer instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        ShowCurrentMoneyAmount();
    }

    public void ShowCurrentMoneyAmountOnText(TextMeshProUGUI otherText)
    {
        otherText.text = StoringMoney.instance.CurrentAmount.ToString();
    }

    public void ShowCurrentMoneyAmount()
    {
        ShowCurrentMoneyAmountOnText(m_TextWithMoneyAmount);
    }
}