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

    public List<MinigameProp> m_props;

    protected Alien m_alien;

    public virtual void Awake()
    {
        foreach(var prop in m_props)
            prop.StoreState();
    }

    public virtual void ResetMinigame()
    {
        foreach(var prop in m_props)
            prop.ResetState();
    }

    public virtual void AlienArrived(Alien alien)
    {
        m_alien = alien;
    }

    public virtual void AlienLeave()
    {

    }
}
