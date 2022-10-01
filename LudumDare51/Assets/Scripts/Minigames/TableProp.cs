using UnityEngine;
using UnityEngine.EventSystems;

public class TableProp : MinigameProp, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public TablePropMinigame m_minigame;
    public eMinigameEvent m_event;

    public override void ResetState()
    {
        if(gameObject.activeSelf)
            return;

        base.ResetState();

        DropOnscreen();
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position += (Vector3)eventData.delta;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if(eventData.delta != Vector2.zero)
        {
            Throw(eventData.delta / Time.deltaTime);
        }
        else
        {
            Throw(Vector3.zero);
        }
    }

    
    public override void LeavePlayArea()
    {
        m_minigame.PropEvent(m_event);
    }
}
