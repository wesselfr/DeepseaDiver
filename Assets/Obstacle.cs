using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour {
    [SerializeField]
    private GameObject m_Root;

    public void DisableObject()
    {
        m_Root.SetActive(false);
    }
}
