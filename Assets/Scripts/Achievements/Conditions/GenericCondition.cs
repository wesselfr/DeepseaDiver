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



