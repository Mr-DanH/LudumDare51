using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void EventHappened(eMinigameEvent minigameEvent)
    {
        AlienTraits.Trait foundTrait = Data.Traits.Find(x=>x.Events.Contains(minigameEvent));
        if (foundTrait != null)
        {
            Debug.Log($"EventHappened {minigameEvent}, thoughts {foundTrait.Description}");
            AdjustAttraction(foundTrait.IsPositive);
            foundTrait.Complete();
        }
    }

    public void DateEnded()
    {
        List<AlienTraits.Trait> uncompletedTraits = Data.Traits.FindAll(x=>!x.IsCompleted && x.IsPositive);
        foreach(var trait in uncompletedTraits)
        {
            Debug.Log($"Uncompleted Trait {trait.Description}. OH NO!");
            AdjustAttraction(isPositive: false);
        }
    }

    private void AdjustAttraction(bool isPositive)
    {
        _attraction += isPositive ? 1 : -100;
    }

}
