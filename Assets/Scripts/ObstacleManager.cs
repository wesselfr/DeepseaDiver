using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public struct ObstaclePair
{
    GameObject m_ObstacleObject;
    GenericObstacle m_Data;

    public ObstaclePair(GameObject obstacleObject, GenericObstacle data)
    {
        m_ObstacleObject = obstacleObject;
        m_Data = data;
    }

    public GameObject obstacle { get { return m_ObstacleObject; } }
    public GenericObstacle data { get { return m_Data; } }
}

public class ObstacleManager : MonoBehaviour
{
    [SerializeField]
    private GenericObstacle[] m_Obstacles;
    private Stack<ObstaclePair> m_ObjectPool;

    private Stack<ObstaclePair> m_ObjectsInUse;

    private float m_ResetTime = 6f;

    [SerializeField]
    private Transform m_ObjectPoolPosition;

    // Use this for initialization
    void Start()
    {
        InitializeObjectPool();
    }

    public void InitializeObjectPool()
    {
        m_ObjectsInUse = new Stack<ObstaclePair>();
        m_ObjectPool = new Stack<ObstaclePair>();
        for(int i = 0; i < m_Obstacles.Length; i++)
        {
            for(int j = 0; j < m_Obstacles[i].objectPoolAmount; j++)
            {
                GameObject obstacle = Instantiate(m_Obstacles[i].obstacleObject, m_ObjectPoolPosition);
                ObstaclePair obstaclePair = new ObstaclePair(obstacle, m_Obstacles[i]);
                m_ObjectPool.Push(obstaclePair);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        m_ResetTime -= Time.deltaTime;
        if (m_ObjectsInUse.Count > 0)
        {
            ObstaclePair[] updatePosition = m_ObjectsInUse.ToArray();
            for (int i = 0; i <  updatePosition.Length; i++)
            {
                GameObject obstacle = updatePosition[i].obstacle;
                updatePosition[i].obstacle.transform.position = obstacle.transform.position + -Vector3.forward * 10f * Time.deltaTime;
            }
            if (m_ResetTime < 0)
            {
                m_ResetTime = 3f;
                ToObjectPool(m_ObjectsInUse.Pop());
            }
        }
    }

    void ToObjectPool(ObstaclePair pair)
    {
        pair.obstacle.gameObject.transform.position = m_ObjectPoolPosition.position;
        pair.obstacle.gameObject.SetActive(false);
        m_ObjectPool.Push(pair);
    }

    public void SpawnObstacle(Vector3 position)
    {
        ObstaclePair fromPool = m_ObjectPool.Pop();
        fromPool.obstacle.transform.position = new Vector3(position.x, position.y, 0);
        fromPool.obstacle.gameObject.SetActive(true);
        m_ObjectsInUse.Push(fromPool);
    }

}
