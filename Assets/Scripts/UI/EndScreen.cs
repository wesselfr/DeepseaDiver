using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScreen : MonoBehaviour {

    [SerializeField]
    private GameManager m_Manager;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PlayAgainButton()
    {
        m_Manager.PlayAgain();
    }
}
