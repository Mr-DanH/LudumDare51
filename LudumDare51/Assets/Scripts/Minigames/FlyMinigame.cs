using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyMinigame : Minigame
{
    public override eType Type { get { return eType.Fly; } }

    public Transform m_fly;

    public float m_horizontalSpeed = 100;
    public float m_vertSpeed = 25;

    public override void ResetMinigame()
    {
        base.ResetMinigame();

        //m_leftAlienSpeech.SetActive(false);
    }
    
    public override void AlienArrived(Alien alien)
    {
        base.AlienArrived(alien);

        m_fly.gameObject.SetActive(true);

    }

    public override void AlienLeave()
    {
        base.AlienLeave();
    }

    void Update()
    {
        if(m_fly.gameObject.activeSelf)
            m_fly.transform.position += new Vector3(m_horizontalSpeed, Mathf.Cos(Time.time * Mathf.PI) * m_vertSpeed) * Time.deltaTime;
    }

    // public void OnPress()
    // {
    //     m_leftPlayerSpeech.gameObject.SetActive(true);
    //     m_rightPlayerSpeech.gameObject.SetActive(true);

    //     m_leftPlayerSpeech.alpha = 0.3f;
    //     m_rightPlayerSpeech.alpha = 0.3f;

    //     m_dragState = eState.Waiting;
    // }
}
