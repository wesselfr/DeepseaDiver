using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI m_ScoreText;

    [SerializeField]
    private TextMeshProUGUI m_BestText;

    private float m_Score;
    private float m_HighScore;

    private bool m_Playing;

    // Use this for initialization
    void Start()
    {
        m_Playing = true;
        Player.onPlayerDeath += OnDeath;
        GameManager.OnGameStart += StartPlaying;
    }

    public void OnDeath()
    {
        m_Playing = false;
        if(m_Score > m_HighScore)
        {
            m_HighScore = m_Score;
        }
        ResetScore();
        m_BestText.text = "Best: " + Mathf.RoundToInt(m_HighScore);
    }

    // Update is called once per frame
    void Update()
    {
        if (m_Playing)
        {
            m_Score += Time.deltaTime;
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
}
