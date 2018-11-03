using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "New Condition", menuName = "Condition/New Destroy Condition")]
public class DestroyCondition : GenericCondition
{
    [SerializeField]
    private GenericObstacle[] m_ObstaclesToDestroy;

    [SerializeField]
    private int m_Amount;
    private int m_Current;

    public override void Awake()
    {
        base.Awake();
        m_Current = 0;

        Obstacle.OnObstalceDestroyed += ObstacleDestroyed;
    }

    public void ObstacleDestroyed(GenericObstacle obstacle)
    {
        for (int i = 0; i < m_ObstaclesToDestroy.Length; i++)
        {
            if (obstacle.obstacleName == m_ObstaclesToDestroy[i].obstacleName)
            {
                m_Current++;
            }
        }
        if (m_Current >= m_Amount)
        {
            m_Completed = true;
        }
    }
}
