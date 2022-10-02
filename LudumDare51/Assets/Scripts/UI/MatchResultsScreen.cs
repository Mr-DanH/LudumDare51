using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchResultsScreen : SingletonMonoBehaviour<MatchResultsScreen>
{
    ScreenSwoosh m_screenSwoosh;

    public override void Awake()
    {
        base.Awake();
        m_screenSwoosh = GetComponent<ScreenSwoosh>();
    }

    void Start()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);

        m_screenSwoosh.AnimateOn();
    }

    public void Next()
    {
        m_screenSwoosh.AnimateOff(null);
    }
}
