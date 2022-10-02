using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreen : SingletonMonoBehaviour<TitleScreen>
{    
    ScreenSwoosh m_screenSwoosh;

    public override void Awake()
    {
        base.Awake();
        m_screenSwoosh = GetComponent<ScreenSwoosh>();
    }

    void Start()
    {
        Show();
    }

    public void Show()
    {
        gameObject.SetActive(true);
        m_screenSwoosh.AnimateOn();
    }

    public void StartGame()
    {
        AlienManager.Instance.ResetAliens();
        m_screenSwoosh.AnimateOff(StartGameCallback);
    }

    void StartGameCallback()
    {
        gameObject.SetActive(false);
        Game.Instance.StartGame();
    }
}
