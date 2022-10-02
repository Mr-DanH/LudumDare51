using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienManager : MonoBehaviour
{
    [SerializeField] private AlienVisualsData visualsData;
    [SerializeField] private AlienTraits alienTraits;
    [SerializeField] private int numberAliens = 10;
    
    private List<OngoingAlienData> alienData = new List<OngoingAlienData>();
    private OngoingAlienData _currentAlienDate;

    public int NumAliens { get { return numberAliens; } }

    public OngoingAlienData GetNextAlien(int round)
    {
        _currentAlienDate?.DateEnded();
        _currentAlienDate = alienData.RandomElement<OngoingAlienData>(match: x=>x.NumDates < round);
        _currentAlienDate.DateStarted();
        return _currentAlienDate;
    }

    public void OnMinigameComplete(eMinigameEvent minigameEvent)
    {
        _currentAlienDate.EventHappened(minigameEvent);
    }

    public void ResetAliens()
    {
        alienData.Clear();
        GenerateAliens();
    }

    public void ForEachAlien(System.Action<OngoingAlienData> callback)
    {
        foreach(var alien in alienData)
            callback(alien);
    }

    private void Start()
    {
        if (alienData.Count == 0)
            GenerateAliens();
    }

    private void GenerateAliens()
    {
        List<AlienVisuals> visuals = visualsData.GenerateAlienVisuals(numberAliens);
        List<AlienTraits.Trait> traits = alienTraits.GenerateTraits();

        AlienNameGenerator nameGen = GetComponent<AlienNameGenerator>();
        nameGen.ResetNames(numberAliens);

        for (int i = 0; i < numberAliens; i++)
        {
            AlienData newData = new AlienData(visuals[i], traits, nameGen.GetName(i));
            alienData.Add(new OngoingAlienData(newData));
        }
    }

}

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
            Debug.Log($"EventHappened {minigameEvent}");
            AdjustAttraction(foundTrait.IsPositive);
            foundTrait.Complete();
        }
    }

    public void DateEnded()
    {
        List<AlienTraits.Trait> uncompletedTraits = Data.Traits.FindAll(x=>!x.IsCompleted && x.IsPositive);
        foreach(var trait in uncompletedTraits)
        {
            Debug.Log($"Uncompleted Trait {trait}. OH NO!");
            AdjustAttraction(isPositive: false);
        }
    }

    private void AdjustAttraction(bool isPositive)
    {
        _attraction += isPositive ? 1 : -100;
    }

}