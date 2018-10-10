using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeObstacles : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Contains("Obstacle"))
        {
            other.gameObject.GetComponent<Obstacle>().Fade();
        }
    }
}
    
