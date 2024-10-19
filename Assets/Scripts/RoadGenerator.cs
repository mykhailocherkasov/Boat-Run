using System.Collections.Generic;
using UnityEngine;

public class RoadGenerator : MonoBehaviour
{
    [SerializeField] private Axis m_AxisWithConstantMotion = Axis.Z;
    [SerializeField] private List<GameObject> m_PossibleObstacles = new();
    [SerializeField] private GameObject m_MovingObjectWhichDeterminesSpawning;
    [SerializeField] private float m_DistanceBetweenNeighboringObstacles;
    [SerializeField] private float m_MaximalDitanceFromThisObjectToSpawn;
    [SerializeField] private float m_MinimalDistanceFromThisObjectTODestroy;
    [SerializeField] private int m_QuantityOfStartingObstacles;
    [SerializeField] private int m_QuantityOfObstaclesToBeSpawned;
    [SerializeField] private GameObject m_ObjectAfterLastObstacle;

    [Header("Small obstacles")]
    [SerializeField] private GameObject m_SmallObstacleTemplate;
    [SerializeField] private Axis m_AxisWithDifferentPositionsOfSmallObstalces = Axis.X;
    [SerializeField] private float m_MinimalPositionOfSmallObstacle;
    [SerializeField] private float m_MaximalPositionOfSmallObstacle;

    private GameObject m_LastObstacle;
    private List<GameObject> m_GeneratedObjects = new();
    private int m_QuantityOfSpawnedObjects = 0;
    private int m_AxisWithConstantMotionAsInt;
    private Vector3 m_AxisWithConstantMotionAsVector3;
    private int m_AxisWithDifferentPositionsOfSmallObstalcesAsInt;
    private bool m_WasLastObjectGenerated = false;

    private void Awake()
    {
        m_AxisWithConstantMotionAsInt = AxisConversion.AxisToInt(m_AxisWithConstantMotion);
        m_AxisWithConstantMotionAsVector3 = AxisConversion.AxisToVector3(m_AxisWithConstantMotion);
        m_AxisWithDifferentPositionsOfSmallObstalcesAsInt = AxisConversion.AxisToInt(m_AxisWithDifferentPositionsOfSmallObstalces);
        m_LastObstacle = m_MovingObjectWhichDeterminesSpawning;
        for (int i = 0; i < m_QuantityOfStartingObstacles; i++)
        {
            SpawnNewObstacle();
        }
    }

    private GameObject GetRandomObstacle()
    {
        return m_PossibleObstacles[new System.Random().Next(0, m_PossibleObstacles.Count)];
    }

    private void SpawnNewObstacle()
    {
        GameObject SelectedObstacleTemplate;
        if (m_QuantityOfSpawnedObjects < m_QuantityOfObstaclesToBeSpawned)
        {
            SelectedObstacleTemplate = GetRandomObstacle();
        }
        else
        {
            if (m_WasLastObjectGenerated == false)
            {
                SelectedObstacleTemplate = m_ObjectAfterLastObstacle;
                m_WasLastObjectGenerated = true;
            }
            else
            {
                return;
            }
        }
        Vector3 PositionOfNewObstacle = m_LastObstacle.transform.position + m_AxisWithConstantMotionAsVector3 * m_DistanceBetweenNeighboringObstacles;
        m_LastObstacle = Instantiate(SelectedObstacleTemplate, PositionOfNewObstacle, SelectedObstacleTemplate.transform.rotation);
        GameObject NewSmallObstacle = Instantiate(m_SmallObstacleTemplate, PositionOfNewObstacle, Quaternion.identity);
        m_LastObstacle.transform.position = new Vector3(m_LastObstacle.transform.position.x, SelectedObstacleTemplate.transform.position.y, m_LastObstacle.transform.position.z);
        NewSmallObstacle.transform.parent = m_LastObstacle.transform;
        Vector3 LocalPositionOfSmallObstacle = Vector3.zero;
        LocalPositionOfSmallObstacle[m_AxisWithDifferentPositionsOfSmallObstalcesAsInt] = UnityEngine.Random.Range(m_MinimalPositionOfSmallObstacle, m_MaximalPositionOfSmallObstacle);
        NewSmallObstacle.transform.localPosition = LocalPositionOfSmallObstacle;
        m_GeneratedObjects.Add(m_LastObstacle);
        m_QuantityOfSpawnedObjects++;
    }

    private void Update()
    {
        if (m_LastObstacle.transform.position.z + m_DistanceBetweenNeighboringObstacles - m_MovingObjectWhichDeterminesSpawning.transform.position.z < m_MaximalDitanceFromThisObjectToSpawn)
        {
            SpawnNewObstacle();
        }
        for (int i = m_GeneratedObjects.Count - 1; i >= 0; i--)
        {
            if (m_GeneratedObjects[i] != null)
            {
                if (m_MovingObjectWhichDeterminesSpawning.transform.position[m_AxisWithConstantMotionAsInt] - m_GeneratedObjects[i].transform.position[m_AxisWithConstantMotionAsInt] > m_MinimalDistanceFromThisObjectTODestroy)
                {
                    Destroy(m_GeneratedObjects[i]);
                    m_GeneratedObjects.RemoveAt(i);
                }
            }
        }
    }
}