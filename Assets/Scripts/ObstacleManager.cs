using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObstaclePair
{
    GameObject m_ObstacleObject;
    GenericObstacle m_Data;
    float m_TimeLeft;

    public ObstaclePair(GameObject obstacleObject, GenericObstacle data)
    {
        m_ObstacleObject = obstacleObject;
        m_Data = data;
        m_TimeLeft = 4f;
    }

    public void SetLifetime(float time)
    {
        m_TimeLeft = time;
    }
    public void RemoveTime(float delta)
    {
        m_TimeLeft -= delta;
    }

    public GameObject obstacle { get { return m_ObstacleObject; } }
    public GenericObstacle data { get { return m_Data; } }
    public float timeLeft { get { return m_TimeLeft; } set { m_TimeLeft = value; } }
}

public class ObstacleManager : MonoBehaviour
{
    [SerializeField]
    private GenericObstacle[] m_Obstacles;
    private List<ObstaclePair> m_ObjectPool;

    private List<ObstaclePair> m_ObjectsInUse;

    [SerializeField]
    private Transform m_ObjectPoolPosition;

    private bool m_Death;

    // Use this for initialization
    void Start()
    {
        InitializeObjectPool();
        Player.onPlayerDeath += OnDeath;
        GameManager.OnGameStart += OnReset;
    }

    public void InitializeObjectPool()
    {
        m_ObjectsInUse = new List<ObstaclePair>();
        m_ObjectPool = new List<ObstaclePair>();
        for(int i = 0; i < m_Obstacles.Length; i++)
        {
            for(int j = 0; j < m_Obstacles[i].objectPoolAmount; j++)
            {
                GameObject obstacle = Instantiate(m_Obstacles[i].obstacleObject, m_ObjectPoolPosition);
                ObstaclePair obstaclePair = new ObstaclePair(obstacle, m_Obstacles[i]);
                m_ObjectPool.Add(obstaclePair);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_Death)
        {
            if (m_ObjectsInUse.Count > 0)
            {
                for (int i = 0; i < m_ObjectsInUse.Count - 1; i++)
                {
                    GameObject obstacle = m_ObjectsInUse[i].obstacle;
                    m_ObjectsInUse[i].obstacle.transform.position = obstacle.transform.position + -Vector3.forward * 10f * Time.deltaTime;
                    m_ObjectsInUse[i].RemoveTime(Time.deltaTime);

                    if(m_ObjectsInUse[i].timeLeft <= 0)
                    {
                        ToObjectPool(m_ObjectsInUse[i]);
                        m_ObjectsInUse.RemoveAt(i);
                    }
                }
            }
        }
    }

    void OnDeath()
    {
        m_Death = true;
    }

    void OnReset()
    {
        m_Death = false;

        if (m_ObjectsInUse.Count > 0)
        {
            for (int i = 0; i < m_ObjectsInUse.Count; i++)
            {
                ToObjectPool(m_ObjectsInUse[i]);
                m_ObjectsInUse.Remove(m_ObjectsInUse[i]);
            }
        }
    }

    void ToObjectPool(ObstaclePair pair)
    {
        pair.obstacle.gameObject.transform.position = m_ObjectPoolPosition.position;
        pair.obstacle.gameObject.SetActive(false);
        m_ObjectPool.Add(pair);
    }

    public void SpawnObstacle(Vector3 position)
    {
        if (!m_Death)
        {
            int random = Random.Range(0, m_ObjectPool.Count - 1);
            ObstaclePair fromPool = m_ObjectPool[random];
            if (m_ObjectPool.Count > 0)
            {
                m_ObjectPool.RemoveAt(random);

                fromPool.obstacle.transform.position = new Vector3(position.x, position.y, 20);
                fromPool.obstacle.gameObject.SetActive(true);
                fromPool.SetLifetime(4);
                m_ObjectsInUse.Add(fromPool);
            }
            else
            {
                Debug.LogError("Pool Empty...");
            }
        }
    }

}
