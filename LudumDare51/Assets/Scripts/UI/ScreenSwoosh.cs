using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenSwoosh : MonoBehaviour
{    
    public Transform m_animatedChild;
    public Vector3 m_from;
    
    Vector3 m_startPos;

    public void Awake()
    {
        m_startPos = m_animatedChild.localPosition;
    }

    public void AnimateOn()
    {
        StartCoroutine(Animate(m_from, m_startPos, null));
    }

    public void AnimateOff(System.Action callback)
    {
        StartCoroutine(Animate(m_startPos, m_startPos + (m_startPos - m_from), callback));
    }

    IEnumerator Animate(Vector3 from, Vector3 to, System.Action callback)
    {
        float t = 0;
        while(t < 1)
        {
            t += Time.deltaTime;
            Vector3 pos = Vector3.Lerp(from, to, -Mathf.Cos(t * Mathf.PI));

            m_animatedChild.localPosition = pos;
            yield return null;
        }

        m_animatedChild.localPosition = to;

        callback?.Invoke();
    }
}