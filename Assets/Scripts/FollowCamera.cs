using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour {

    [SerializeField][Tooltip("Changes automatic on startup.")]
    private float m_CameraHeigh;

    [SerializeField]
    private Transform m_Player;

    private Vector3 m_Original;

    private void Start()
    {
        m_Original = transform.position;
    }

    // Update is called once per frame
    void Update () {
        Vector3 camPos = m_Original;
        
        camPos.y = m_Player.transform.position.y;
        transform.position = camPos;
	}
}
