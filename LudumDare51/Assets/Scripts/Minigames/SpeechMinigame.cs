using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechMinigame : Minigame
{
    public override eType Type { get { return eType.Speech; } }

    public GameObject m_leftAlienSpeech;
    public GameObject m_rightAlienSpeech;

    public CanvasGroup m_leftPlayerSpeech;
    public CanvasGroup m_rightPlayerSpeech;

    float m_time = float.MaxValue;
    
    public enum eState
    {
        Off,
        Waiting,
        Left,
        Right
    } 
    eState m_state;

    eState m_dragState;

    public override void ResetMinigame()
    {
        base.ResetMinigame();

        m_leftAlienSpeech.SetActive(false);
        m_rightAlienSpeech.SetActive(false);
        m_leftPlayerSpeech.gameObject.SetActive(false);
        m_rightPlayerSpeech.gameObject.SetActive(false);
    }
    
    public override void AlienArrived(Game.Alien alien)
    {
        base.AlienArrived(alien);

        m_time = Time.time + 2;
        m_state = eState.Waiting;

    }

    public override void AlienLeave()
    {     
        base.AlienLeave();

        m_leftAlienSpeech.SetActive(false);
        m_rightAlienSpeech.SetActive(false);
        m_leftPlayerSpeech.gameObject.SetActive(false);
        m_rightPlayerSpeech.gameObject.SetActive(false);
        
        m_state = eState.Off;
    }

    void Update()
    {
        if(Time.time > m_time)
        {
            if(m_state == eState.Waiting)
            {
                m_time = float.MaxValue;
                m_state = (Random.value > 0.5f) ? eState.Left : eState.Right;

                m_leftAlienSpeech.SetActive(m_state == eState.Left);
                m_rightAlienSpeech.SetActive(m_state == eState.Right);
            }
            else
            {
                m_leftAlienSpeech.SetActive(false);
                m_rightAlienSpeech.SetActive(false);
                m_leftPlayerSpeech.gameObject.SetActive(false);
                m_rightPlayerSpeech.gameObject.SetActive(false);
            }

        }
    }

    public void OnPress()
    {
        m_leftPlayerSpeech.gameObject.SetActive(true);
        m_rightPlayerSpeech.gameObject.SetActive(true);

        m_leftPlayerSpeech.alpha = 0.3f;
        m_rightPlayerSpeech.alpha = 0.3f;

        m_dragState = eState.Waiting;
    }

    public void OnDrag(Vector2 totalDrag)
    {
        if(totalDrag.x < -50)
        {
            m_leftPlayerSpeech.alpha = 0.7f;
            m_rightPlayerSpeech.alpha = 0.3f;
            m_dragState = eState.Left;
        }
        else if(totalDrag.x > 50)
        {
            m_leftPlayerSpeech.alpha = 0.3f;
            m_rightPlayerSpeech.alpha = 0.7f;
            m_dragState = eState.Right;
        }
        else
        {
            m_leftPlayerSpeech.alpha = 0.3f;
            m_rightPlayerSpeech.alpha = 0.3f;
            m_dragState = eState.Waiting;
        }
    }

    public void OnRelease()
    {
        m_leftPlayerSpeech.alpha = (m_dragState == eState.Left) ? 1 : 0;
        m_rightPlayerSpeech.alpha = (m_dragState == eState.Right) ? 1 : 0;

        m_time = Time.time + 1;
    }
}