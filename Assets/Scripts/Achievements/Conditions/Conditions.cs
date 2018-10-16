using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICondition
{
    bool state { get; }
}

public abstract class GenericCondition : ScriptableObject, ICondition
{
    public virtual void Awake()
    {
        m_Completed = false;
    }
    protected bool m_Completed;
    public bool state { get { return m_Completed; } }
}

[CreateAssetMenu(fileName = "New Condition", menuName = "Condition/New Distance Condition")]
public class DistanceCondition : GenericCondition
{
    [SerializeField]
    private float m_Distance;

    public override void Awake()
    {
        base.Awake();
        ScoreManager.OnEndRun += CheckAndSet;
    }

    public void CheckAndSet(float score, float high)
    {
        if(score >= m_Distance)
        {
            m_Completed = true;
        }
    }
}

[CreateAssetMenu(fileName = "New Condition", menuName = "Condition/New State Condition")]
public class StateCondition : GenericCondition
{
    public void SetState(bool state)
    {
        m_Completed = state;
    }
}

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
        for(int i = 0; i < m_ObstaclesToDestroy.Length; i++)
        {
            if(obstacle.obstacleName == m_ObstaclesToDestroy[i].obstacleName)
            {
                m_Current++;
            }
        }
        if(m_Current >= m_Amount)
        {
            m_Completed = true;
        }
    }
}

[CreateAssetMenu(fileName = "New Condition", menuName = "Condition/New Counter Condition")]
public class CounterCondition : GenericCondition
{
    [SerializeField]
    private int m_Amount;
    private int m_Current;

    public override void Awake()
    {
        base.Awake();
    }

    public void Increment()
    {
        m_Current++;
        if(m_Current > m_Amount)
        {
            m_Completed = true;
        }
    }
}
