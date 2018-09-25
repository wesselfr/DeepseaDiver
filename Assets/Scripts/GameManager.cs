using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class GameManager : MonoBehaviour {

    public delegate void GameManagerEvent();
    public static GameManagerEvent OnGameReset;

    private int m_GlobalPlays;
    private static bool m_PlayerAlive;

	// Use this for initialization
	void Start () {
        m_PlayerAlive = true;
        Player.onPlayerDeath += PlayerDeath;

        AnalyticsEvent.debugMode = Application.isEditor;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PlayAgain()
    {
        m_GlobalPlays++;
        m_PlayerAlive = true;
        if (OnGameReset != null)
        {
            OnGameReset();
        }
    }

    public void PlayerDeath()
    {
        m_PlayerAlive = false;
    }

    public static bool playerAlive { get { return m_PlayerAlive; } }
}
