using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewObstacle", menuName = "Obstacles/New")]
public class GenericObstacle : ScriptableObject { 

    [Header("Positions and size")]
    [SerializeField]
    private bool[] m_Size = new bool[9];
    [SerializeField]
    private bool[] m_Positions = new bool[9];

    [Header("Model and art settings")]
    [SerializeField]
    private GameObject m_Prefab;

    [Header("Appearence")]
    [SerializeField]
    private int m_ObjectPoolAmount;


    public int objectPoolAmount { get { return m_ObjectPoolAmount; } }
    public GameObject obstacleObject { get { return m_Prefab; } }
    
}
