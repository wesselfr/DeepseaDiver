using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;

public class AchievementsManager : MonoBehaviour {

    [SerializeField]
    private GameManager m_Manager;
    [SerializeField]
    private GenericAchievement[] m_AchievementsInUse;

    private Dictionary<string, GenericAchievement> m_Achievements;

    public delegate void AchievementEvent(string achievementName);


	// Use this for initialization
	void Start () {
        m_Achievements = new Dictionary<string, GenericAchievement>();
	    //Load all Achievements
        for(int i = 0; i < m_AchievementsInUse.Length; i++)
        {
            GenericAchievement achievement = Instantiate(m_AchievementsInUse[i]);
            for(int j = 0; j < achievement.conditions.Length; j++)
            {
                achievement.conditions[j] = Instantiate(achievement.conditions[j]);
                achievement.conditions[j].Awake();
            }
            
            m_Achievements.Add(m_AchievementsInUse[i].achievementName, achievement);
        }

        ScoreManager.OnCheckAchievements += ShowAchievementsOnEndRun;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ShowAchievementsOnEndRun()
    {
        for (int i = 0; i < m_AchievementsInUse.Length; i++)
        {
            GenericCondition[] conditions = m_AchievementsInUse[i].conditions;
            bool completedAllConditions = true;
            for(int j = 0; j < conditions.Length; j++)
            {
                if (conditions[j].state)
                {
                    Debug.Log(conditions[j].name + " Completed!");
                }
                else { completedAllConditions = false; }
            }
            if (completedAllConditions)
            {
                Debug.Log("Completed Achievement: " + m_AchievementsInUse[i].name);
                if (m_Manager.settings.useGooglePlay)
                {
                    if (PlayGamesPlatform.Instance.localUser.authenticated)
                    {
                        PlayGamesPlatform.Instance.ReportProgress(m_AchievementsInUse[i].achievementName, 100d, succes => { Debug.Log("Achievement Unlocked"); });
                    }
                }
            }
        }
    }
}
