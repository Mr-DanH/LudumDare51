using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseMatchesScreen : SingletonMonoBehaviour<ChooseMatchesScreen>
{
    public AlienManager m_alienManager;
    public Transform m_animatedChild;
    public Vector3 m_from;

    Vector3 m_startPos;
    Toggle[] m_toggles;

    public override void Awake()
    {
        base.Awake();
        m_startPos = m_animatedChild.localPosition;
        m_toggles = GetComponentsInChildren<Toggle>();
    }

    void Start()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);

        int toggleIndex = 0;
        m_alienManager.ForEachAlien(
            delegate(OngoingAlienData alien)
            {
                Text text = m_toggles[toggleIndex++].GetComponentInChildren<Text>(); 
                text.text = alien.Data.Name;
            });

        StartCoroutine(Animate(m_from, m_startPos, null));

    }

    public void Next()
    {
        StartCoroutine(Animate(m_startPos, m_startPos + (m_startPos - m_from), ShowNextScreen));
    }

    void ShowNextScreen()
    {
        gameObject.SetActive(false);

        int toggleIndex = 0;
        m_alienManager.ForEachAlien(
            delegate(OngoingAlienData alien)
            {
                alien.PlayerRequestedMatch = m_toggles[toggleIndex++].isOn;
            });
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
