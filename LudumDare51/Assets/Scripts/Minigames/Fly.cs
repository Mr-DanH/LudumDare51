using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Fly : MinigameProp, IPointerDownHandler
{
    public FlyMinigame m_minigame;

    public override void ResetState()
    {
        return;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        m_minigame.PropEvent(eMinigameEvent.FlyKilled);

        float angle = Random.Range(Mathf.PI * -0.25f, Mathf.PI * 0.25f);
        Vector3 velocity = new Vector3(Mathf.Sin(angle), Mathf.Cos(angle), 0) * 800;
        velocity.x *= 0.5f;

        Throw(velocity, new Vector3(0,0, Mathf.Sign(velocity.x) * -180));
    }
}
