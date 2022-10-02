using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatchResultsScreen : SingletonMonoBehaviour<MatchResultsScreen>
{
    public Text m_descriptionText;
    public Transform m_matchParent;

    ScreenSwoosh m_screenSwoosh;

    readonly string[] PICKUP_LINES = {
        "{0} would like to be in your orbital path",
        "{0} can't think of anyone they'd rather cryo-sleep with",
        "{0} can't escape your gravitational attraction",
        "{0} would watch five simultaneous sunsets with you",
        "{0} is ready to launch a relationship with you",
        "{0} thinks you're super-nova",
        "{0} declares war on your heart",
        "{0} was lost in space, by which they mean your eyes",
        "{0} thinks you're light-years ahead of the rest",
        "{0} has agreed to not destroy your planet"
    };

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

        List<string> lines = new List<string>(PICKUP_LINES);

        AlienManager.Instance.ForEachAlien(
            delegate(OngoingAlienData alien)
            {
                if(alien.AlienRequestedMatch && alien.PlayerRequestedMatch)
                {
                    Transform matchEntry = m_matchParent.GetChild(numMatches);
                    matchEntry.gameObject.SetActive(true);

                    Text text = matchEntry.GetComponentInChildren<Text>();

                    text.color = Color.Lerp(alien.Data.Visuals.Colouring.SkinColour, Color.black, 0.25f);

                    if(lines.Count == 0)
                        lines.AddRange(PICKUP_LINES);

                    int lineIndex = Random.Range(0, lines.Count);
                    text.text = string.Format(lines[lineIndex], alien.Data.Name);
                    lines.RemoveAt(lineIndex);

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
