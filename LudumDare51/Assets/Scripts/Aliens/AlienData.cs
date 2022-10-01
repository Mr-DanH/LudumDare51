using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienData 
{
    private AlienVisuals _visuals;
    
    public AlienVisuals Visuals { get { return _visuals; } }

    public AlienData(AlienVisuals visuals)
    {
        _visuals = visuals;
    }
}
