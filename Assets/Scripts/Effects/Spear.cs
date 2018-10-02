using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Spear : MonoBehaviour {

    [SerializeField]
    private float m_Speed;

    public void Initialize()
    {
        GetComponent<Rigidbody>().velocity += Vector3.forward * m_Speed;
        gameObject.transform.Rotate(Vector3.up * 180);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Contains("Obstacle"))
        {
            other.gameObject.GetComponent<Obstacle>().DisableObject();
            Destroy(this.gameObject);
        }
    }
}
