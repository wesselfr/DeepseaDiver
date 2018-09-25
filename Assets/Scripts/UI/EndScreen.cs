using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScreen : MonoBehaviour {

    [SerializeField]
    private GameManager m_Manager;


    public void PlayAgainButton()
    {
        m_Manager.PlayAgain();
    }
}
