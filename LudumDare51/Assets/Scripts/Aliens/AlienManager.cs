using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienManager : MonoBehaviour
{
    [SerializeField] private AlienVisualsData visualsData;
    [SerializeField] private int numberAliens = 10;
    
    private List<OngoingAlienData> alienData = new List<OngoingAlienData>();

    public int NumAliens { get { return numberAliens; } }

    public OngoingAlienData GetNextAlien(int round)
    {
        return alienData.RandomElement<OngoingAlienData>(match: x=>x.NumDates < round);
    }

    public void ResetAliens()
    {
        alienData.Clear();
        GenerateAliens();
    }

    private void Start()
    {
        if (alienData.Count == 0)
            GenerateAliens();
    }

    private void GenerateAliens()
    {
        List<AlienVisuals> visuals = visualsData.GenerateAlienVisuals(numberAliens);
        // todo - get traits

        for (int i = 0; i < numberAliens; i++)
        {
            AlienData newData = new AlienData(visuals[i]);
            alienData.Add(new OngoingAlienData(newData));
        }
    }

    public void ForEachAlien(System.Action<OngoingAlienData> callback)
    {
        foreach(var alien in alienData)
            callback(alien);
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

    public OngoingAlienData(AlienData data)
    {
        _alienData = data;
        _attraction = 0;
        _numDates = 0;
    }

}