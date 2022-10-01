using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minigame : MonoBehaviour
{
    public enum eType
    {
        Unknown,

        Speech,
        TableProp,
        Drink,
        Handshake,
        Fly,
        Strength
    }

    public virtual eType Type { get; }

    protected Game.Alien m_alien;

    public virtual void ResetMinigame()
    {
    }

    public virtual void AlienArrived(Game.Alien alien)
    {
        m_alien = alien;
    }
}
