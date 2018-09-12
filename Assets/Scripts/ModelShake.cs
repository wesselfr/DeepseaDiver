using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelShake : MonoBehaviour {

    [SerializeField]
    private float m_TimeBonus, m_Range;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.localPosition = Vector3.up * (Mathf.Cos(Time.time * m_TimeBonus) * m_Range);
    }
}
