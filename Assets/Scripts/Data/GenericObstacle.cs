using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewObstacle", menuName = "Obstacles/New")]
public class GenericObstacle : ScriptableObject {

    [SerializeField]
    private string m_ObstacleName;

    [Header("Positions and size")]
    [SerializeField]
    private int m_Width;
    [SerializeField]
    private int m_Height;
    [SerializeField]
    private bool[] m_Positions = new bool[9];
    private bool[,] m_PossiblePositions;

    [Header("Model and art settings")]
    [SerializeField]
    private GameObject m_Prefab;

    [Header("Appearence")]
    [SerializeField]
    private int m_ObjectPoolAmount;
    [SerializeField]
    private float m_AdditionalSpeed;

    [Header("Scripts")]
    [SerializeField]
    private DamageBehavior m_Damage;

    public string obstacleName { get { return m_ObstacleName; } }

    public int objectPoolAmount { get { return m_ObjectPoolAmount; } }
    public GameObject obstacleObject { get { return m_Prefab; } }
    public DamageBehavior damageBehavior { get { return m_Damage; } }

    public int widht { get { return m_Width; } }
    public int height { get { return m_Height; } }

    public float additionalSpeed { get { return m_AdditionalSpeed; } }

    public void Setup()
    {
        m_PossiblePositions = new bool[3, 3];
        int index = -1;
        for (int y = 0; y < 3; y++)
        {
            for (int x = 0; x < 3; x++)
            {
                index++;
                m_PossiblePositions[x, y] = m_Positions[index];
            }
        }
    }
    public bool CanSpawn(Vector2i position)
    {
        if(m_PossiblePositions == null) { Setup(); }
        return m_PossiblePositions[position.x, position.y];
    }


}
