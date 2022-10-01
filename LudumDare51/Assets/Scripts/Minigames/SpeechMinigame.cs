using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechMinigame : Minigame
{
    public override eType Type { get { return eType.Speech; } }

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
