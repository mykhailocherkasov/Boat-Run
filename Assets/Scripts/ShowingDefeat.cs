using UnityEngine;
using UnityEngine.Events;

public class ShowingDefeat : MonoBehaviour
{
    [SerializeField] private UnityEvent m_OnDefeat;

    public void ShowDefeat()
    {
        Debug.Log("Defeat");
        m_OnDefeat.Invoke();
    }
}