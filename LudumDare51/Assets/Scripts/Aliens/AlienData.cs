using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienData 
{
    private AlienVisuals _visuals;
    private List<AlienTraits.Trait> _traits;
    
    public AlienVisuals Visuals { get { return _visuals; } }
    public List<AlienTraits.Trait> Traits { get { return _traits; } }

    public string Name { get; private set; }

    public AlienData(AlienVisuals visuals, List<AlienTraits.Trait> traits, string name)
    {
        _visuals = visuals;
        Name = name;
        _traits = traits;
    }
}
