using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndScreen : MonoBehaviour {

    [SerializeField]
    private GameManager m_Manager;

    [SerializeField]
    private TextMeshProUGUI m_Score, m_HighScore, m_NewHighScoreText;

    private void Start()
    {
        ScoreManager.OnEndRun += DisplayScore;
        ScoreManager.OnNewHighScore += ShowNewHighScore;
        GameManager.OnGameStart += ResetScreen;
    }

    public void DisplayScore(float score, float high)
    {
        m_Score.text = Mathf.Round(score) + "";
        m_HighScore.text = "Best: " + Mathf.Round(high);
    }

    public void PlayAgainButton()
    {
        m_Manager.PlayAgain();
    }

    void ShowNewHighScore(float score)
    {
        m_NewHighScoreText.text = "New";
    }


    void ResetScreen()
    {
        m_NewHighScoreText.text = "";
    }
}
