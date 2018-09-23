using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI m_ScoreText;

    private float m_Score;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        m_Score += Time.deltaTime;
        m_ScoreText.text = Mathf.Round(m_Score) + "";
    }

    public void ResetScore()
    {
        m_Score = 0;
    }
}
