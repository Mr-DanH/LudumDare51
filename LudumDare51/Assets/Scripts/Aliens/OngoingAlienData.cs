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

    List<eMinigameEvent> allDateEvents = new List<eMinigameEvent>();

    public void EventHappened(eMinigameEvent minigameEvent)
    {
        allDateEvents.Add(minigameEvent);
    }

    public void DateEnded()
    {
        Data.Traits.ForEach(JudgeTraitPass);
    }

    private void JudgeTraitPass(AlienTraits.Trait trait)
    {
        var intersectedIncludedEvents = allDateEvents.Intersect(trait.IncludedEvents);
        var intersectedOmittedEvents = allDateEvents.Intersect(trait.OmittedEvents);

        bool hasIncludedAll = intersectedIncludedEvents.Count() == trait.IncludedEvents.Count();
        bool hasOmmittedAll = intersectedOmittedEvents.Count() == trait.OmittedEvents.Count();

        AdjustAttraction(hasIncludedAll && hasOmmittedAll);
    }

    private void AdjustAttraction(bool isPositive)
    {
        _attraction += isPositive ? 1 : -100;
    }

}
