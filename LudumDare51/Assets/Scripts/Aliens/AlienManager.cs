using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienManager : SingletonMonoBehaviour<AlienManager>
{
    [SerializeField] private AlienVisualsData visualsData;
    [SerializeField] private AlienTraits alienTraits;
    [SerializeField] private int numberAliens = 10;
    [SerializeField] private Vector2 numRangeTraits = new Vector2(2,4);
    
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
        AlienNameGenerator nameGen = GetComponent<AlienNameGenerator>();
        nameGen.ResetNames(numberAliens);

        for (int i = 0; i < numberAliens; i++)
        {
            List<AlienTraits.Trait> traits = alienTraits.GenerateTraits(Random.Range((int)numRangeTraits.x, (int)numRangeTraits.y+1));
            AlienData newData = new AlienData(visuals[i], traits, nameGen.GetName(i));
            alienData.Add(new OngoingAlienData(newData));
        }
    }

}