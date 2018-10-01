using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Linq;

public enum GameState
{
    MainMenu,
    Settings,
    Tutorial,
    Playing,
    Dead
}

[System.Serializable]
public class SaveData
{
    public int totalPlays;
    public int score;
    public byte[] hash;
}

public class GameManager : MonoBehaviour {

    [SerializeField]
    private ScoreManager m_ScoreManager;

    public delegate void GameManagerEvent();
    public delegate void GameDataEvent(SaveData data);

    public static GameManagerEvent OnGameLoaded;
    public static GameManagerEvent OnGameStart;
    public static GameManagerEvent OnLostFocus;
    public static GameManagerEvent OnRegainFocus;

    //Tutorial
    public static GameManagerEvent OnStartTutorial;
    public static GameManagerEvent OnTutorialNext;
    public static GameManagerEvent OnTutorialCompleted;
    public static GameManagerEvent OnTutorialSkipped;

    public static GameManagerEvent OnFirstPlay;
    public static GameManagerEvent OnMainMenu;

    public static GameDataEvent OnGameDataLoad;

    private int m_GlobalPlays = 0;

    private GameState m_GameState;

    private bool m_HasFocus;

	// Use this for initialization
	void Start () {
        m_GameState = GameState.MainMenu;
        Player.onPlayerDeath += PlayerDeath;
        Application.quitting += SaveData;

        AnalyticsEvent.debugMode = Application.isEditor;

        DataPrivacy.Initialize();

        m_HasFocus = true;

        LoadData();
        if(OnGameLoaded != null) { OnGameLoaded(); }

        if (m_GlobalPlays == 0)
        {
            //First Play
            //Aks for tutorial maybe?
            if(OnFirstPlay != null)
            {
                OnFirstPlay();
            }
        }

	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (m_GameState == GameState.MainMenu || m_GameState == GameState.Dead)
            {
                Application.Quit();
            }
            else
            {
                //Show Pause Screen
                //If we ever gonna implement one
                //Maybe just pause the game and continue on touch.
            }
        }

        //Focus lost
        if (!Application.isFocused && m_HasFocus)
        {
            m_HasFocus = false;
            if(OnLostFocus != null)
            {
                OnLostFocus();
            }
            SaveData();
        }
        //Focus regaind
        if(Application.isFocused && !m_HasFocus)
        {
            m_HasFocus = true;
            if(OnRegainFocus != null)
            {
                OnRegainFocus();
            }
        }
	}

    public void PlayAgain()
    {
        m_GlobalPlays++;

        m_GameState = GameState.Playing;
        if (OnGameStart != null)
        {
            AnalyticsEvent.GameStart();
            OnGameStart();
        }
    }

    public void StartTutorial()
    {
        AnalyticsEvent.TutorialStart();
        if(OnStartTutorial != null)
        {
            OnStartTutorial();
        }
    }

    public void TutorialCompleted()
    {
        AnalyticsEvent.TutorialComplete();
        if(OnTutorialCompleted != null)
        {
            OnTutorialCompleted();
        }
    }

    public void TutorialCanceld()
    {
        AnalyticsEvent.TutorialSkip();
        if(OnTutorialSkipped != null)
        {
            OnTutorialSkipped();
        }
    }

    public void PlayerDeath()
    {
        m_GameState = GameState.Dead;
        AnalyticsEvent.GameOver();
        SaveData();
    }

    private void LoadData()
    {
        string location = Application.persistentDataPath;
        location = Path.Combine(location, "save.dat");

        if (File.Exists(location))
        {
            using (StreamReader reader = new StreamReader(location))
            {
                string json = reader.ReadLine();
                SaveData data = JsonUtility.FromJson<SaveData>(json);
                reader.Close();

                using (SHA256 sha = SHA256.Create())
                {
                    List<byte> byteList = new List<byte>();
                    byteList.Add(Convert.ToByte(data.totalPlays));
                    byteList.Add(Convert.ToByte(data.score));

                    byte[] hash = sha.ComputeHash(byteList.ToArray());

                    if (hash.SequenceEqual(data.hash))
                    {
                        if (OnGameDataLoad != null)
                        {
                            OnGameDataLoad(data);
                            m_GlobalPlays = data.totalPlays;
                            Debug.Log("Global Plays: " + m_GlobalPlays);
                        }
                    }
                    else
                    {
                        SaveData();
                    }
                }
            }
        }
        else
        {
            SaveData();
        }
    }

    private void SaveData()
    {
        string location = Application.persistentDataPath;
        location = Path.Combine(location, "save.dat");

        SaveData data = new SaveData();
        data.totalPlays = m_GlobalPlays;
        data.score = Mathf.RoundToInt(m_ScoreManager.highScore);

        using (SHA256 sha = SHA256.Create())
        {
            List<byte> byteList = new List<byte>();
            byteList.Add(Convert.ToByte(data.totalPlays));
            byteList.Add(Convert.ToByte(data.score));

            data.hash = sha.ComputeHash(byteList.ToArray());
        }

        using (StreamWriter writer = new StreamWriter(location))
        {
            writer.WriteLine(JsonUtility.ToJson(data));
            writer.Close();
        }

        Debug.Log(location);
    }

    //public static bool playerAlive { get { return m_PlayerAlive; } }
}
