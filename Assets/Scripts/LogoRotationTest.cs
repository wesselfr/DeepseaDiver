﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogoRotationTest : MonoBehaviour {

    [SerializeField]
    private RectTransform m_Logo;

    [SerializeField]
    private float m_Range, m_UpRange;

    [SerializeField]
    private float m_LerpSpeed, m_Speed;

    private Vector2 m_Origin;
    private Vector2 m_OriginalEuler;

    void Start()
    {
        m_Origin = m_Logo.localPosition;
        Input.gyro.enabled = true;
        m_OriginalEuler = GyroToUnity(Input.gyro.attitude).eulerAngles;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        m_Logo.localPosition = Vector3.Slerp(m_Origin, m_Origin + Vector2.right * Mathf.Clamp(Input.acceleration.x * m_Range * m_Speed, -m_Range, m_Range) + Vector2.up * Mathf.Clamp(Input.acceleration.y * m_UpRange * m_Speed, -m_UpRange, m_UpRange), m_LerpSpeed * Time.fixedDeltaTime);
    }
    void Update()
    {
        //Vector3 euler = GyroToUnity(Input.gyro.attitude).eulerAngles;
        //euler.x = 0;
        //euler.y = 0;
        //transform.rotation = Quaternion.Euler(euler);
        //transform.position = new Vector3(euler.z, 0, 0) * 10f * Time.deltaTime;
        //m_Logo.localPosition = Vector2.Lerp(m_Logo.localPosition, m_Origin + ((new Vector2(euler.y, euler.x) - new Vector2(m_OriginalEuler.y, m_OriginalEuler.x)) * 10f), 0.2f);

        //m_Logo.localPosition = Vector3.Slerp(m_Origin, m_Origin + Vector2.right * Mathf.Clamp(Input.acceleration.x * m_Range * m_Speed, -m_Range, m_Range) + Vector2.up * Mathf.Clamp(Input.acceleration.y * m_UpRange * m_Speed, -m_UpRange, m_UpRange), m_LerpSpeed * Time.deltaTime);
    }

    private void OnGUI()
    {
        //GUI.skin.label.fontSize = Screen.width / 40;

        //GUILayout.Label("Gyro attitude: " + Input.gyro.attitude);
    }

    private Quaternion GyroToUnity(Quaternion q)
    {
        return new Quaternion(q.x, q.y, -q.z, -q.w);
    }
}
