using UnityEngine;
using UnityEngine.EventSystems;

public class SpeechButton : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public SpeechMinigame m_minigame;

    public void OnDrag(PointerEventData eventData)
    {
        m_minigame.OnDrag(eventData.position - (Vector2)transform.position);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        m_minigame.OnPress();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        m_minigame.OnRelease();
    }
}
