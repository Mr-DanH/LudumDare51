using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatchResultsScreen : SingletonMonoBehaviour<MatchResultsScreen>
{
    public Text m_descriptionText;
    public Transform m_matchParent;

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

        int numMatches = 0;

        AlienManager.Instance.ForEachAlien(
            delegate(OngoingAlienData alien)
            {
                if(alien.AlienRequestedMatch && alien.PlayerRequestedMatch)
                {
                    Transform matchEntry = m_matchParent.GetChild(numMatches);
                    matchEntry.gameObject.SetActive(true);

                    matchEntry.GetComponentInChildren<Text>().text = alien.Data.Name;
                    ++numMatches;
                }
            });

        for(int i = numMatches; i < m_matchParent.childCount; ++i)
            m_matchParent.GetChild(i).gameObject.SetActive(false);

        if(numMatches == 1)
            m_descriptionText.text = "Congratulations, you got a match, could this be true love?";
        else if(numMatches > 1)
            m_descriptionText.text = "Congratulations, you got several matches, time to clear your schedule!";
        else
            m_descriptionText.text = "Unfortunately you didn't have any matches this time. Better luck next time!";

        m_screenSwoosh.AnimateOn();
    }

    public void Next()
    {
        m_screenSwoosh.AnimateOff(NextCallback);
    }

    void NextCallback()
    {
        gameObject.SetActive(false);
        TitleScreen.Instance.Show();
    }
}
