using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewObstacle", menuName = "Obstacles/New")]
public class GenericObstacle : MonoBehaviour { 

    [SerializeField]
    [GridSelector(3,3)]
    public bool[,] m_Size = new bool[4,4];
    [GridSelector(3,3)]
    public bool[,] m_Position;

    [SerializeField][GridSelector(3,3)]
    public bool m_Test;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
