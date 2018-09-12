using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lane : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnDrawGizmos()
    {
        Vector3 infinity = Vector3.forward * Mathf.Infinity;
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position - transform.forward * 100f, transform.position + Vector3.forward * 100f);

        Gizmos.color = Color.white;
        Gizmos.DrawCube(transform.position, Vector3.one * 0.1f);
    }
}
