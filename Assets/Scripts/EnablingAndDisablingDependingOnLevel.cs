using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelTypeAndObjectsToShowAndHide
{
    public LevelType LevelType;
    public List<GameObject> GameObjectsToBeEnabled = new();
    public List<GameObject> GameObjectsToBeDisabled = new();

    public void ShowAndHideObjects()
    {
        GameObjectsToBeEnabled.ForEach(oneObject => oneObject.SetActive(true));
        GameObjectsToBeDisabled.ForEach(oneObject => oneObject.SetActive(false));
    }
}

public class EnablingAndDisablingDependingOnLevel : MonoBehaviour
{
    [SerializeField] private List<LevelTypeAndObjectsToShowAndHide> ObjectsToBeShowedAndHiddenDependingOnLevel = new();
    [SerializeField] private SelectedLevelData m_SelectedLevelData;

    private void Awake()
    {
        foreach(LevelTypeAndObjectsToShowAndHide oneLevelTypeData in ObjectsToBeShowedAndHiddenDependingOnLevel)
        {
            if(m_SelectedLevelData.SelectedLevelType == oneLevelTypeData.LevelType)
            {
                oneLevelTypeData.ShowAndHideObjects();
            }
        }
    }
}