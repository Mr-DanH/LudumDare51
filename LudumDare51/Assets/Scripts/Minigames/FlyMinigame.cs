using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyMinigame : Minigame
{
    public override eType Type { get { return eType.Fly; } }

    public Transform m_fly;

    public float m_horizontalSpeed = 100;
    public float m_vertSpeed = 25;

    float m_speed;

    public override void ResetMinigame()
    {
        base.ResetMinigame();
    }
    
    public override void AlienArrived(Alien alien)
    {
        base.AlienArrived(alien);

        m_fly.gameObject.SetActive(true);

        m_speed = m_horizontalSpeed * Random.Range(0.75f, 1.25f);
        if(Random.value > 0.5f)
        {
            m_speed *= -1;
            Vector3 pos = m_fly.transform.localPosition;
            pos.x *= -1;
            m_fly.transform.localPosition = pos;        
        }
    }

    public override void AlienLeave()
    {
        base.AlienLeave();
    }

    void Update()
    {
        if(m_fly.gameObject.activeSelf)
            m_fly.transform.position += new Vector3(m_speed, Mathf.Cos(Time.time * Mathf.PI) * m_vertSpeed) * Time.deltaTime;
    }
}
