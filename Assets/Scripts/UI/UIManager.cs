using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {

    [SerializeField]
    private GameObject m_ScoreHolder;

    [SerializeField]
    private GameObject m_EndRunUI;

	// Use this for initialization
	void Start () {
        Player.onPlayerDeath += PlayerDeath;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void PlayerDeath()
    {
        m_ScoreHolder.SetActive(false);
        m_EndRunUI.SetActive(true);
        StartCoroutine(ScaleEffect(m_EndRunUI, 7f));
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
