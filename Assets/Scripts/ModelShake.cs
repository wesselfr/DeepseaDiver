using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelShake : MonoBehaviour {

    [Header("Up/Down Movement")]
    [SerializeField]
    private float m_TimeBonus;

    [SerializeField]
    private float m_Range;

    [Header("Normal Side Movement")]
    [SerializeField]
    private float m_SideTime;
    [SerializeField]
    private float m_SideRange;

    [Header("Switch Effect")]
    [SerializeField]
    private float m_SwitchTime;
    [SerializeField]
    private float m_SideTimeBonus;
    [SerializeField]
    private float m_SideRangeBonus;

    private float m_CurrentSwitchTime;
    private float m_CurrentSide;

    [SerializeField]
    [Header("DEBUG")]
    private bool m_Switch, m_StartSwitch, m_EndSwitch;

    private float m_SwitchTimer;
    private float m_StartSwitchTimer;
    private float m_EndSwitchTimer;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.localPosition = Vector3.up * (Mathf.Cos(Time.time * m_TimeBonus) * m_Range);
        if (!m_Switch)
        {
            transform.localPosition += Vector3.left * (Mathf.Cos(Time.time * (m_SideTime / 2)) + Mathf.Sin(Time.time * (m_SideTime / 2)) / 2f) * m_SideRange;
        }
        else
        {
            m_SwitchTimer -= Time.deltaTime;
            transform.localPosition += Vector3.left * (Mathf.Cos(Time.time * ((m_CurrentSwitchTime) / 2)) + Mathf.Sin(Time.time * ((m_CurrentSwitchTime) / 2)) / 2f) * (m_CurrentSide);
            if(m_SwitchTimer <= 0)
            {
                m_Switch = false;
                m_EndSwitchTimer = 1f;
                m_EndSwitch = true;
            }
        }

        if (m_StartSwitch)
        {
            m_StartSwitchTimer = 1f;
            m_SwitchTimer = m_SwitchTime;
        }
        if(m_StartSwitchTimer > 0)
        {
            m_StartSwitchTimer -= Time.deltaTime;
            m_CurrentSide = Mathf.Lerp(m_SideRange + m_SideRangeBonus, m_SideRange, m_StartSwitchTimer);
            m_CurrentSwitchTime = Mathf.Lerp(m_SideTime + m_SideTimeBonus, m_SideTime, m_StartSwitchTimer);
            if (m_StartSwitchTimer < 0)
            {
                m_Switch = true;
            }

        }
        if(m_EndSwitchTimer > 0)
        {
            m_EndSwitchTimer -= Time.deltaTime;
            m_CurrentSide = Mathf.Lerp(m_SideRange, m_SideRange + m_SideRangeBonus, m_EndSwitchTimer);
            m_CurrentSwitchTime = Mathf.Lerp(m_SideTime, m_SideTime + m_SideTimeBonus, m_EndSwitchTimer);
            if (m_EndSwitchTimer < 0) { m_EndSwitch = false; }
        }
    }
}
