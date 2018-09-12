using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField]
    private LaneManger m_Lanes;

    [SerializeField]
    private float m_SwipeSensivity;

    [SerializeField]
    private float m_TransitionTime;
    private float m_CurrentTransitionTimer;

    private Vector3 m_TouchPosition;
    private Vector3 m_TouchDelta;

    private Vector3 m_GoalPosition;
    private float m_LerpTime;

    private bool m_Debug;

    private Vector3 m_TouchStart;
    private Vector3 m_TouchEnd;

	// Use this for initialization
	void Start () {
        m_Debug = Application.isEditor;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.touchCount > 0)
        {

            //if(Input.GetTouch(0).phase == TouchPhase.Began)
                if (Input.GetTouch(0).phase == TouchPhase.Moved)
                {
                    m_TouchPosition = Input.GetTouch(0).position;
                    m_TouchDelta = Input.GetTouch(0).deltaPosition;
                    if (m_CurrentTransitionTimer <= 0)
                    {
                    if (m_TouchDelta.y > m_SwipeSensivity) { MoveUp(); }
                    if (m_TouchDelta.y < -m_SwipeSensivity) { MoveDown(); }
                    if (m_TouchDelta.x > m_SwipeSensivity) { MoveRight(); }
                    if (m_TouchDelta.x < -m_SwipeSensivity) { MoveLeft(); }
                    }
                }


            //if (Input.GetTouch(0).phase == TouchPhase.Stationary || Input.GetTouch(0).phase == TouchPhase.Ended || Input.GetTouch(0).phase == TouchPhase.Canceled)
            //{
            //    //if (m_CurrentTransitionTimer <= 0)
            //    //{
            //        if (m_TouchDelta.y > m_SwipeSensivity) { MoveUp(); }
            //        if (m_TouchDelta.y < -m_SwipeSensivity) { MoveDown(); }
            //        if (m_TouchDelta.x > m_SwipeSensivity) { MoveRight(); }
            //        if (m_TouchDelta.x < -m_SwipeSensivity) { MoveLeft(); }
            //    //}
            //}
        }
        if (m_CurrentTransitionTimer <= 0)
        {

            if (m_Debug)
            {
                if (Input.GetKeyDown(KeyCode.W))
                {
                    MoveUp();
                }
                if (Input.GetKeyDown(KeyCode.S))
                {
                    MoveDown();
                }
                if (Input.GetKeyDown(KeyCode.A))
                {
                    MoveLeft();
                }
                if (Input.GetKeyDown(KeyCode.D))
                {
                    MoveRight();
                }
            }
        }
        else
        {
            m_CurrentTransitionTimer -= Time.deltaTime;

            transform.position = Vector3.Slerp(transform.position, m_GoalPosition, m_LerpTime*Time.deltaTime);
        }
	}

    private void CallculateLerpTime()
    {
        m_LerpTime = Vector3.Distance(transform.position, m_GoalPosition) / m_TransitionTime;
    }

    public void MoveUp()
    {
        //transform.position += Vector3.up * 2f;
        //transform.position.Set(transform.position.x, Mathf.Clamp(transform.position.y, 1.3f, 5.3f), transform.position.z);
        m_GoalPosition = m_Lanes.MoveUp();
        m_TouchDelta = Vector3.zero;
        m_CurrentTransitionTimer = m_TransitionTime;
        CallculateLerpTime();
    }

    public void MoveDown()
    {
        //transform.position += Vector3.down * 2f;
        //transform.position.Set(transform.position.x, Mathf.Clamp(transform.position.y, 1.3f, 5.3f), transform.position.z);
        m_GoalPosition = m_Lanes.MoveDown();
        m_TouchDelta = Vector3.zero;
        m_CurrentTransitionTimer = m_TransitionTime;
        CallculateLerpTime();
    }

    public void MoveRight()
    {
        m_GoalPosition = m_Lanes.MoveRight();
        m_TouchDelta = Vector3.zero;
        m_CurrentTransitionTimer = m_TransitionTime;
        CallculateLerpTime();
    }

    public void MoveLeft()
    {
        m_GoalPosition = m_Lanes.MoveLeft();
        m_TouchDelta = Vector3.zero;
        m_CurrentTransitionTimer = m_TransitionTime;
        CallculateLerpTime();
    }

    private void OnDrawGizmos()
    {
        
    }
}
