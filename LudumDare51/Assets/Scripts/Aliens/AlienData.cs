using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienData 
{
    private AlienVisuals _visuals;
    
    public AlienVisuals Visuals { get { return _visuals; } }

    public string Name { get; private set; }

    public AlienData(AlienVisuals visuals, string name)
    {
        _visuals = visuals;
        Name = name;
    }
}
