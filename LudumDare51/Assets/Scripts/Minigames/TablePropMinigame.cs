using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TablePropMinigame : Minigame
{
    public override eType Type { get { return eType.TableProp; } }

    public override void ResetMinigame()
    {

    }
    
    public override void AlienArrived(Game.Alien alien)
    {
        base.AlienArrived(alien);

    }

    void Update()
    {

    }
}