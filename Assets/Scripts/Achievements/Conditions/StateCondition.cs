using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "New Condition", menuName = "Condition/New State Condition")]
public class StateCondition : GenericCondition
{
    public void SetState(bool state)
    {
        m_Completed = state;
    }
}
