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
    public uint version; //In order to load correct data with updates.
    public uint totalPlays;
    public int score;
    public uint credits;
    public byte[] hash;
}

[System.Serializable]
public class SettingsData
{
    public uint version; //In order to load correct data with updates.
    public bool vibrateOnDeath;
    public bool useGooglePlay; //Possibility to stop syncing with google play services.
    public bool usePostProcessing; //This option is meant for low end devices.
    public float musicVolume;
    public float fxVolume;
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
    public static GameManagerEvent OnSettingLoaded;

    private uint m_GlobalPlays = 0;
    private uint m_Credits = 0;

    private GameState m_GameState;
    private SettingsData m_Settings;

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
        LoadAndApplySettings();
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
                    byteList.Add(Convert.ToByte(data.version));
                    byteList.Add(Convert.ToByte(data.totalPlays));
                    byteList.Add(Convert.ToByte(data.score));
                    byteList.Add(Convert.ToByte(data.credits));

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
            byteList.Add(Convert.ToByte(data.version));
            byteList.Add(Convert.ToByte(data.totalPlays));
            byteList.Add(Convert.ToByte(data.score));
            byteList.Add(Convert.ToByte(data.credits));

            data.hash = sha.ComputeHash(byteList.ToArray());
        }

        using (StreamWriter writer = new StreamWriter(location))
        {
            writer.WriteLine(JsonUtility.ToJson(data));
            writer.Close();
        }

        Debug.Log(location);
    }

    private void LoadAndApplySettings()
    {
        string location = Application.persistentDataPath;
        location = Path.Combine(location, "settings.dat");

        if (File.Exists(location))
        {
            using (StreamReader reader = new StreamReader(location))
            {
                string json = reader.ReadLine();
                SettingsData data = JsonUtility.FromJson<SettingsData>(json);
                reader.Close();

                m_Settings = data;
                if(OnSettingLoaded != null)
                {
                    OnSettingLoaded();
                }
                Debug.Log("Settings loaded. Location: " + location);
            }
        }
        else
        {
            SaveSettings();
        }

    }

    private void SaveSettings()
    {
        if (m_Settings != null)
        {
            string location = Application.persistentDataPath;
            location = Path.Combine(location, "settings.dat");
            using (StreamWriter writer = new StreamWriter(location))
            {
                writer.WriteLine(JsonUtility.ToJson(m_Settings, true));
                writer.Close();
                Debug.Log("Settings save at: " + location);
            }
        }
        else
        {
            //Generate standard settings.
            m_Settings = new SettingsData();
            m_Settings.useGooglePlay = true;
            m_Settings.usePostProcessing = true;
            m_Settings.vibrateOnDeath = true;
            m_Settings.fxVolume = 100f;
            m_Settings.musicVolume = 100f;

            //Save generated data.
            SaveSettings();
        }
    }

    public SettingsData settings { get { return m_Settings; } }
    //public static bool playerAlive { get { return m_PlayerAlive; } }
}
