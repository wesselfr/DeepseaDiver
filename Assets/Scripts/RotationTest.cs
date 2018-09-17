using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Input.gyro.enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 euler = GyroToUnity(Input.gyro.attitude).eulerAngles;
        euler.x = 0;
        euler.y = 0;
        transform.rotation =  Quaternion.Euler(euler);
        //transform.position = new Vector3(euler.z, 0, 0) * 10f * Time.deltaTime;
        //transform.position += euler * 10f * Time.deltaTime;
	}

    private void OnGUI()
    {
        GUI.skin.label.fontSize = Screen.width / 40;

        GUILayout.Label("Gyro attitude: " + Input.gyro.attitude);
    }

    private static Quaternion GyroToUnity(Quaternion q)
    {
        return new Quaternion(q.x, q.y, -q.z, -q.w);
    }
}
