using UnityEngine;
using UnityEngine.EventSystems;

public class TableProp : MinigameProp, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    Vector3 m_velocity;
    int m_dragFrame;
    bool m_dragging;
    bool m_thrown;

    public override void ResetState()
    {
        base.ResetState();
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

            Canvas canvas = GetComponentInParent<Canvas>();

            RectTransform rectTransform = (RectTransform)transform;
            Rect rect = rectTransform.rect;            
            rect.position += (Vector2)rectTransform.position;

            if(!rect.Overlaps(canvas.pixelRect))
                gameObject.SetActive(false);
        }
    }
}
