using UnityEngine;
using System.Collections;

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
        if (m_Current > m_Amount)
        {
            m_Completed = true;
        }
    }
}
