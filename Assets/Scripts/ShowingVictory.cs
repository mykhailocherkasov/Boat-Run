using UnityEngine;
using UnityEngine.Events;

public class ShowingVictory : MonoBehaviour
{
    [SerializeField] private UnityEvent m_OnVictory;

    public void ShowVictory()
    {
        Debug.Log("Victory");
        m_OnVictory.Invoke();
    }
}