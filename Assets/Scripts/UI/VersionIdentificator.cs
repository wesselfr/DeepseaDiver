using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VersionIdentificator : MonoBehaviour {

    [SerializeField]
    private TextMeshProUGUI m_VersionText;

	// Use this for initialization
	void Start () {
        m_VersionText.text = "Version: " + Application.version;
	}
}
