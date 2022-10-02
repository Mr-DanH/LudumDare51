using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyMinigame : Minigame
{
    public override eType Type { get { return eType.Fly; } }

    public Fly m_fly;

    public float m_horizontalSpeed = 100;
    public float m_vertSpeed = 25;

    Vector2 m_velocity;
    bool m_isLooping;
    int m_lastSizeRoll = 2;

    public override void ResetMinigame()
    {
        base.ResetMinigame();
    }
    
    public override void AlienArrived(Alien alien)
    {
        base.AlienArrived(alien);

        if(m_fly.gameObject.activeSelf)
            return;

        m_fly.gameObject.SetActive(true);
        m_fly.transform.localPosition = m_fly.StartPos;

        RandomiseSpeedScale(Random.value > 0.5f);

        if(m_velocity.x < 0)
        {
            Vector3 pos = m_fly.transform.localPosition;
            pos.x *= -1;
            m_fly.transform.localPosition = pos;        
        }

        m_isLooping = true;
    }

    void RandomiseSpeedScale(bool goLeft)
    {
        m_velocity.x = m_horizontalSpeed;
        m_velocity.y = m_vertSpeed;

        if(goLeft)
            m_velocity.x *= -1;

        int roll = Random.Range(0,2);
        if(roll >= m_lastSizeRoll)
            ++roll;
        m_lastSizeRoll = roll;

        switch(roll)
        {
            case 0:
                m_fly.transform.localScale = Vector3.one * 0.5f;
                m_velocity *= 0.5f;
                break;
            case 1:
                m_fly.transform.localScale = Vector3.one;
                break;
            case 2:
                m_fly.transform.localScale = Vector3.one * 2f;
                m_velocity *= 2;
                break;
        }
    }

    public override void AlienLeave()
    {
        base.AlienLeave();
    }

    void Update()
    {
        if(!m_fly.gameObject.activeSelf)
            return;

        Vector3 flyPos = m_fly.transform.position;
        flyPos += new Vector3(m_velocity.x, Mathf.Cos(Time.time * Mathf.PI) * m_velocity.y) * Time.deltaTime;

        if(m_isLooping)
        {
            flyPos += new Vector3(m_velocity.x * 2 * Mathf.Cos((Time.time + 0.5f) * Mathf.PI), 0, 0) * Time.deltaTime;
        }

        m_fly.transform.position = flyPos;

        Rect rect = m_fly.GetWorldRect();
        Rect canvasRect = m_fly.GetCanvasRect();
        float canvasWidth = canvasRect.width;
        canvasRect.xMin -= canvasWidth * 0.1f;
        canvasRect.xMax += canvasWidth * 0.1f;

        if(m_velocity.x > 0)
        {
            if(rect.xMin > canvasRect.xMax)
                RandomiseSpeedScale(true);
        }
        else
        {
            if(rect.xMax < canvasRect.xMin )
                RandomiseSpeedScale(false);
        }
    }
}
