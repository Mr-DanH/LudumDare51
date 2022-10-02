using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class OngoingAlienData
{
    private AlienData _alienData;
    private float _attraction;
    private int _numDates;

    public AlienData Data { get { return _alienData; } }
    public int NumDates { get { return _numDates; } }
    public bool PlayerRequestedMatch { get; set; }
    public bool AlienRequestedMatch { get { return _attraction > 0; } }
    private List<eMinigameEvent> _allDateEvents = new List<eMinigameEvent>();

    public OngoingAlienData(AlienData data)
    {
        _alienData = data;
        _attraction = 0;
        _numDates = 0;
    }

    public void DateStarted()
    {
        _numDates++;
    }

    public void EventHappened(eMinigameEvent minigameEvent)
    {
        _allDateEvents.Add(minigameEvent);
    }

    public eEventEmotion GetEmotionAboutEvent(eMinigameEvent minigameEvent)
    {
        eEventEmotion emotion = eEventEmotion.Neutral;

        foreach(var trait in Data.Traits)
        {
            bool isIncluded = trait.IncludedEvents.Contains(minigameEvent);
            bool isOmitted = trait.OmittedEvents.Contains(minigameEvent);

            if (isIncluded)
            {
                emotion = eEventEmotion.Happy;
            }
            else if (isOmitted)
            {
                emotion = eEventEmotion.Sad;
            }

            if (emotion != eEventEmotion.Neutral)
            {
                break;
            }
        }

        return emotion;
    }

    public void DateEnded()
    {
        Data.Traits.ForEach(JudgeTraitPass);
    }

    private void JudgeTraitPass(AlienTraits.Trait trait)
    {
        var intersectedIncludedEvents = _allDateEvents.Intersect(trait.IncludedEvents);
        var intersectedOmittedEvents = _allDateEvents.Intersect(trait.OmittedEvents);

        bool hasIncludedAll = intersectedIncludedEvents.Count() == trait.IncludedEvents.Count();
        bool hasOmmittedAll = intersectedOmittedEvents.Count() == trait.OmittedEvents.Count();

        AdjustAttraction(hasIncludedAll && hasOmmittedAll);
    }

    private void AdjustAttraction(bool isPositive)
    {
        _attraction += isPositive ? 1 : -100;
    }

}
