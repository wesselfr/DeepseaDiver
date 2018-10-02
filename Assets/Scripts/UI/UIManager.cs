using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {

    [Header("Gameplay")]
    [SerializeField]
    private GameObject m_InGameUI;

    [SerializeField]
    private GameObject m_EndRunUI;

    [SerializeField]
    private GameObject m_PauseScreen;

    [Header("Menu's")]
    [SerializeField]
    private GameObject m_MainMenuUI;

    [SerializeField]
    private GameObject m_SettingsUI;

    [Header("Tutorials")]
    [SerializeField]
    private GameObject m_FirstPlayUI;


	// Use this for initialization
	void Start () {
        Player.onPlayerDeath += PlayerDeath;
        GameManager.OnGameStart += SwitchToGameView;
        GameManager.OnFirstPlay += ShowFirstPlayUI;
        GameManager.OnStartTutorial += HideFirstPlayUI;
        GameManager.OnLostFocus += LostFocus;
        GameManager.OnRegainFocus += RegainFocus;

        m_MainMenuUI.SetActive(true);
	}
	
    void LostFocus()
    {
        m_PauseScreen.SetActive(true);
    }
    void RegainFocus()
    {
        m_PauseScreen.SetActive(false);
    }

    void ShowFirstPlayUI()
    {
        m_InGameUI.SetActive(false);
        m_EndRunUI.SetActive(false);
        m_MainMenuUI.SetActive(true);
        m_SettingsUI.SetActive(false);
        m_FirstPlayUI.SetActive(true);
        StartCoroutine(ScaleEffect(m_FirstPlayUI, 5f));
    }

    public void HideFirstPlayUI()
    {
        m_InGameUI.SetActive(false);
        m_EndRunUI.SetActive(false);
        m_MainMenuUI.SetActive(true);
        m_SettingsUI.SetActive(false);
        m_FirstPlayUI.SetActive(false);
    }

    void PlayerDeath()
    {
        m_InGameUI.SetActive(false);
        m_EndRunUI.SetActive(true);
        StartCoroutine(ScaleEffect(m_EndRunUI, 7f));
    }

    void SwitchToGameView()
    {
        m_InGameUI.SetActive(true);
        m_EndRunUI.SetActive(false);
        m_MainMenuUI.SetActive(false);
        m_FirstPlayUI.SetActive(false);
        m_SettingsUI.SetActive(false);
    }


    IEnumerator ScaleEffect(GameObject panel, float speed)
    {
        panel.transform.localScale = Vector3.zero;
        float scale = 0f;
        while (scale < 1) 
        {
            scale += (0.01f * speed);
            panel.transform.localScale = Vector3.one * scale;
            yield return new WaitForSeconds(0.01f);
        }
    }
}
