using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Fly : MinigameProp, IPointerDownHandler
{
    public FlyMinigame m_minigame;

    public void OnPointerDown(PointerEventData eventData)
    {
        gameObject.SetActive(false);
        m_minigame.PropEvent(eMinigameEvent.FlyKilled);
    }
}
