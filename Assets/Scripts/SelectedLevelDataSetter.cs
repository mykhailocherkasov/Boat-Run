using UnityEngine;

public class SelectedLevelDataSetter : MonoBehaviour
{
    [SerializeField] private SelectedLevelData m_SelectedLevelData;

    private void SetLevelType(LevelType levelTypeToBeSet)
    {
        m_SelectedLevelData.SelectedLevelType = levelTypeToBeSet;
    }

    public void SetLevelWithoutBoss() => SetLevelType(LevelType.LevelWithoutBoss);
    public void SetLevelWithBoss() => SetLevelType(LevelType.LevelWithBoss);
    public void SetTheOtherLevel()
    {
        if(m_SelectedLevelData.SelectedLevelType == LevelType.LevelWithoutBoss)
        {
            SetLevelWithBoss();
        }
        else
        {
            SetLevelWithoutBoss();
        }
    }
}