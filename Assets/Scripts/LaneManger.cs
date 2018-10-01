using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vector2i
{
    int m_X;
    int m_Y;
    public Vector2i(int x, int y)
    {
        m_X = x;
        m_Y = y;
    }
    public Vector2i(float x, float y)
    {
        m_X = Mathf.RoundToInt(x);
        m_Y = Mathf.RoundToInt(y);
    }
    public Vector2i(Vector2 vector)
    {
        m_X = Mathf.RoundToInt(vector.x);
        m_Y = Mathf.RoundToInt(vector.y);
    }

    public static implicit operator Vector2i(Vector2 vector)
    {
        return new Vector2i(vector.x, vector.y);
    }

    public static Vector2i operator+(Vector2i a, Vector2i b)
    {
        return new Vector2i(a.x + b.x, a.y + b.y);
    }

    public int x { get { return m_X; } set { m_X = value; } }
    public int y { get { return m_Y; } set { m_Y = value; } }
}

public class LaneManger : MonoBehaviour {

    [SerializeField]
    private Lane[] m_BottomLanes, m_CenterLanes, m_TopLanes;

    [SerializeField]
    private Vector2 m_StartLane;

    [SerializeField]
    private ObstacleManager m_Obstacles;

    private Lane[,] m_LaneGrid;
    private Vector2i m_CurrentLane;

    [SerializeField]
    private float m_TimeForNextObstacle;

    private float m_SpawnTimer;

    public void Start()
    {

        //Initializes LaneGrid
        m_LaneGrid = new Lane[3, 3];
        //Lane Grid[x,y]
        for(int i = 0; i < m_BottomLanes.Length; i++)
        {
            m_LaneGrid[i, 0] = m_BottomLanes[i];
        }
        for (int i = 0; i < m_CenterLanes.Length; i++)
        {
            m_LaneGrid[i, 1] = m_CenterLanes[i];
        }
        for (int i = 0; i < m_TopLanes.Length; i++)
        {
            m_LaneGrid[i, 2] = m_TopLanes[i];
        }

        //Set player to the center of the bottom lane.
        m_CurrentLane = new Vector2i(1, 0);
    }

    private void CheckAndSetDirection(Vector2 dir)
    {
        m_CurrentLane += dir;
        m_CurrentLane.x = Mathf.Clamp(m_CurrentLane.x, 0, 2);
        m_CurrentLane.y = Mathf.Clamp(m_CurrentLane.y, 0, 2);
    }

    private void Update()
    {
        m_SpawnTimer -= Time.deltaTime;
        if(m_SpawnTimer < 0)
        {
            m_SpawnTimer = m_TimeForNextObstacle;

            int x = Random.Range(0, 3);
            int y = Random.Range(0, 3);
            
            m_Obstacles.SpawnObstacle(m_LaneGrid[x, y].transform.position);
            
        }
    }

    public Vector3 MoveUp()
    {
        CheckAndSetDirection(Vector2.up);
        return m_LaneGrid[m_CurrentLane.x, m_CurrentLane.y].transform.position;
    }

    public Vector3 MoveDown()
    {
        CheckAndSetDirection(Vector2.down);
        return m_LaneGrid[m_CurrentLane.x, m_CurrentLane.y].transform.position;
    }

    public Vector3 MoveLeft()
    {
        CheckAndSetDirection(Vector2.left);
        return m_LaneGrid[m_CurrentLane.x, m_CurrentLane.y].transform.position;
    }

    public Vector3 MoveRight()
    {
        CheckAndSetDirection(Vector2.right);
        return m_LaneGrid[m_CurrentLane.x, m_CurrentLane.y].transform.position;
    }

    public Vector3 StartPosition()
    {
        Vector2i start = new Vector2i(m_StartLane);
        m_CurrentLane = m_StartLane;
        return m_LaneGrid[start.x, start.y].transform.position;
    }

    
}
