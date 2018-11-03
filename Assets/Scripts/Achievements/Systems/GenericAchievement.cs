using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System;


[CreateAssetMenu(fileName = "New Achievement", menuName = "Achievements/New")]
public class GenericAchievement : ScriptableObject {

    [Header("Basic Information")]
    [SerializeField]
    private string m_AchievementName;

    [SerializeField]
    private int m_AchievementID;

    [SerializeField]
    private string m_AchievementTitle;

    [SerializeField]
    private string m_AchievementDiscritpion;

    [SerializeField]
    private Sprite m_AchievementIcon;

    [Header("Conditions")]
    [SerializeField]
    private GenericCondition[] m_Conditions;

    [Header("Rewards and Extra's")]
    [SerializeField]
    private int m_PearlsReward;

    [Header("Google Play Games")]
    [SerializeField]
    private string m_GooglePlayID;

    [SerializeField]
    private bool m_UsesIncrement;

    #region Accesors
    public string achievementName { get { return m_AchievementName; } }

    public GenericCondition[] conditions { get { return m_Conditions; } }
    #endregion
}
