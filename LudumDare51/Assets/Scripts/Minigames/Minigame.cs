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

    public event System.Action<eMinigameEvent> onMinigameComplete;

    public virtual eType Type { get; }

    public List<MinigameProp> m_props;

    protected OngoingAlienData m_alienData;

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

    public virtual void AlienArrived(Alien alien, OngoingAlienData alienData)
    {
        m_alienData = alienData;
    }

    public virtual void AlienLeave()
    {
        m_alienData = null;
    }

    public void PropEvent(eMinigameEvent minigameEvent)
    {
        onMinigameComplete?.Invoke(minigameEvent);
    }
}
