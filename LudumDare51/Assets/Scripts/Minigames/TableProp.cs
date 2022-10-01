using UnityEngine;
using UnityEngine.EventSystems;

public class TableProp : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    Vector3 m_velocity;
    int m_dragFrame;
    bool m_dragging;
    bool m_thrown;

    void ResetProp()
    {
        m_thrown = false;
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
            m_velocity = eventData.delta / Time.deltaTime;
            m_thrown = true;
        }
        else
        {

        }
    }

    void Update()
    {
        if (m_thrown)
        {
            transform.position += m_velocity * Time.deltaTime;
        }
    }
}
