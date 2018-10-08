using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Analytics;
using TMPro;
using System.Collections.Generic;

public class ScoreManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI m_ScoreText;

    [SerializeField]
    private TextMeshProUGUI m_BestText;

    [SerializeField]
    private float m_ScoreMultiplier;

    public delegate void ScoreEvent(float score, float high);
    public delegate void HighScoreEvent(float highScore);
    public delegate void GameEvent();
    public static HighScoreEvent OnNewHighScore;
    public static ScoreEvent OnEndRun;
    public static GameEvent OnDifficultyIncrease;

    private float m_Score;
    private float m_HighScore;

    private bool m_Playing;

    // Use this for initialization
    void Start()
    {
        m_Playing = false;
        Player.onPlayerDeath += OnDeath;
        GameManager.OnGameStart += StartPlaying;
        GameManager.OnGameDataLoad += SetScores;
    }

    void SetScores(SaveData data)
    {
        m_HighScore = data.score;
        Debug.Log("Best Score: " +  data.score);
        m_BestText.text = "Best: " + Mathf.RoundToInt(m_HighScore);
    }

    void OnDeath()
    {
        m_Playing = false;
        if(m_Score > m_HighScore)
        {
            m_HighScore = m_Score;
            if(OnNewHighScore != null)
            {
                OnNewHighScore(m_HighScore);
            }
        }
        if (OnEndRun != null)
        {
            OnEndRun(m_Score, m_HighScore);
        }

        Dictionary<string, object> data = new Dictionary<string, object>();
        data.Add("score", m_Score);
        data.Add("high_score", m_HighScore);

        Analytics.CustomEvent("run_completed", new Dictionary<string, object> { {"score", m_Score },{ "high_score", m_HighScore} });

        ResetScore();
        m_BestText.text = "Best: " + Mathf.RoundToInt(m_HighScore);
    }

    // Update is called once per frame
    void Update()
    {
        if (m_Playing)
        {
            m_Score += Time.deltaTime * m_ScoreMultiplier;
            m_ScoreText.text = Mathf.Round(m_Score) + "";
        }
    }

    public void ResetScore()
    {
        m_Score = 0;
    }

    public void StartPlaying()
    {
        m_Playing = true;
    }

    public float score { get { return m_Score; } }
    public float highScore { get { return m_HighScore; } }
}
