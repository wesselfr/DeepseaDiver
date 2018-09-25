using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableLevel : MonoBehaviour {

    [SerializeField]
    GameObject[] m_Objects;

    [SerializeField]
    private float m_Speed;

    private bool m_Scrolling;

    private void Start()
    {
        Player.onPlayerDeath += StopScrol;
        GameManager.OnGameStart += StartScrol;
    }

    void StopScrol()
    {
        m_Scrolling = false;
    }
    void StartScrol()
    {
        m_Scrolling = true;
    }
    // Update is called once per frame
    void Update () {
        if (m_Scrolling)
        {
            for (int i = 0; i < m_Objects.Length; i++)
            {
                if (m_Objects[i].transform.position.z < -46)
                {
                    int last = i - 1;
                    if (last < 0) { last = m_Objects.Length - 1; }
                    m_Objects[i].transform.position = new Vector3(m_Objects[i].transform.position.x, m_Objects[i].transform.position.y, m_Objects[last].transform.position.z + 12);
                }
                m_Objects[i].transform.position += -Vector3.forward * m_Speed * Time.deltaTime;
            }
        }
	}
}
