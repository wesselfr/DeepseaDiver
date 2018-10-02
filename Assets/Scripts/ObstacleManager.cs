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

    private bool m_Death = true;

    [SerializeField]
    private LaneManger m_Lanes;

    [SerializeField]
    private float m_TimeForNextObstacle, m_MinTime, m_MaxTime;

    [SerializeField]
    private ScoreManager m_Score;

    private float m_SpawnTimer;

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
        m_SpawnTimer -= Time.deltaTime;
            if (m_SpawnTimer < 0)
            {
                m_SpawnTimer = Mathf.Clamp(m_TimeForNextObstacle - ((m_Score.score / 30) / 10f), m_MinTime, m_MaxTime);

                int obstalces = Random.Range(1, 4);

                int lastX = -1;
                int lastY = -1;

                Stack<Vector2i> lastPositions = new Stack<Vector2i>();

                for (int i = 0; i < obstalces; i++)
                {
                    int x = Random.Range(0, 3);
                    int y = Random.Range(0, 3);
                    Vector2i position = new Vector2(x, y);
                    if (!lastPositions.Contains(position))
                    {
                        SpawnObstacle(m_Lanes.GetPosition(position));
                    }
                    lastPositions.Push(position);
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
        m_SpawnTimer = m_TimeForNextObstacle;
        if (m_ObjectsInUse.Count > 0)
        {
            for (int i = m_ObjectsInUse.Count - 1; i > 0; i--)
            {
                ToObjectPool(m_ObjectsInUse[i]);
                m_ObjectsInUse.RemoveAt(i);
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
            int random = Random.Range(0, m_ObjectPool.Count);
            if (m_ObjectPool.Count > 0)
            {
                ObstaclePair fromPool = m_ObjectPool[random];
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
