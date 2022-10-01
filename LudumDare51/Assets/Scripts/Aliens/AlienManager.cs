using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienManager : MonoBehaviour
{
    [SerializeField] private AlienVisualsData visualsData;
    [SerializeField] private int numberAliens = 10;
    // todo stats

    private List<OngoingAlienData> alienData;

    public OngoingAlienData GetNextAlien(int round)
    {
        return alienData.RandomElement<OngoingAlienData>(match: x=>x.NumDates < round);
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
}

public class OngoingAlienData
{
    private AlienData _alienData;
    private float _attraction;
    private int _numDates;

    public int NumDates { get { return _numDates; } }

    public OngoingAlienData(AlienData data)
    {
        _alienData = data;
        _attraction = 0;
        _numDates = 0;
    }

}
