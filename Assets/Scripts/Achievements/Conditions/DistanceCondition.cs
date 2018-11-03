using UnityEngine;
using System.Collections;

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
        if (score >= m_Distance)
        {
            m_Completed = true;
        }
    }
}