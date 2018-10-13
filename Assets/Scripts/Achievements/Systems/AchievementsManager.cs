using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementsManager : MonoBehaviour {

    [SerializeField]
    private GenericAchievement[] m_AchievementsInUse;
    private Dictionary<string, GenericAchievement> m_Achievements;

    public delegate void AchievementEvent(string achievementName);


	// Use this for initialization
	void Start () {
	    //Load all Achievements
        for(int i = 0; i < m_AchievementsInUse.Length; i++)
        {
            m_Achievements.Add(m_AchievementsInUse[i].achievementName, m_AchievementsInUse[i]);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
