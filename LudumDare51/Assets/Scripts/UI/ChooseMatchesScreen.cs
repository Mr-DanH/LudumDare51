using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseMatchesScreen : SingletonMonoBehaviour<ChooseMatchesScreen>
{
    public Transform m_animatedChild;
    public Vector3 m_from;

    Vector3 m_startPos;
    Toggle[] m_toggles;
    ScreenSwoosh m_screenSwoosh;

    public override void Awake()
    {
        base.Awake();
        m_startPos = m_animatedChild.localPosition;
        m_toggles = GetComponentsInChildren<Toggle>();
        m_screenSwoosh = GetComponent<ScreenSwoosh>();
    }

    void Start()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);

        int toggleIndex = 0;
        AlienManager.Instance.ForEachAlien(
            delegate(OngoingAlienData alien)
            {
                Text text = m_toggles[toggleIndex++].GetComponentInChildren<Text>(); 
                text.text = alien.Data.Name;
            });

        m_screenSwoosh.AnimateOn();
    }

    public void Next()
    {
        m_screenSwoosh.AnimateOff(ShowNextScreen);
    }

    void ShowNextScreen()
    {
        gameObject.SetActive(false);

        int toggleIndex = 0;
        AlienManager.Instance.ForEachAlien(
            delegate(OngoingAlienData alien)
            {
                alien.PlayerRequestedMatch = m_toggles[toggleIndex++].isOn;
            });

        MatchResultsScreen.Instance.Show();
    }
}
