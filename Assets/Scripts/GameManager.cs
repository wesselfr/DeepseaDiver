using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class GameManager : MonoBehaviour {

    public delegate void GameManagerEvent();
    public static GameManagerEvent OnGameStart;
    public static GameManagerEvent OnLostFocus;
    public static GameManagerEvent OnRegainFocus;

    private int m_GlobalPlays = 0;
    private static bool m_PlayerAlive;

	// Use this for initialization
	void Start () {
        m_PlayerAlive = true;
        Player.onPlayerDeath += PlayerDeath;

        AnalyticsEvent.debugMode = Application.isEditor;

        DataPrivacy.Initialize();
        
        if (m_GlobalPlays == 0)
        {
            //First Play
        }

        //When first playing;
        if(OnGameStart != null)
        {
            OnGameStart();
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!m_PlayerAlive)
            {
                Application.Quit();
            }
            else
            {
                //Show Pause Screen
            }
        }

        if (!Application.isFocused)
        {
            if(OnLostFocus != null)
            {
                OnLostFocus();
            }
        }
	}

    public void PlayAgain()
    {
        m_GlobalPlays++;
        
        m_PlayerAlive = true;
        if (OnGameStart != null)
        {
            OnGameStart();
        }
    }

    public void PlayerDeath()
    {
        m_PlayerAlive = false;
    }

    public static bool playerAlive { get { return m_PlayerAlive; } }
}
