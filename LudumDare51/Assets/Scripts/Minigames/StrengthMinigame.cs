using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrengthMinigame : Minigame
{
    public override eType Type { get { return eType.Strength; } }

    public GameObject m_targetObject;

    public override void ResetMinigame()
    {
        base.ResetMinigame();
    }
    
    public override void AlienArrived(Game.Alien alien)
    {
        base.AlienArrived(alien);

    }

    public override void AlienLeave()
    {     
        base.AlienLeave();
    }

    void Update()
    {
    }

    public void OnPress()
    {
        m_targetObject.gameObject.SetActive(true);
    }
}
