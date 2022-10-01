using UnityEngine;
using UnityEngine.EventSystems;

public class TableProp : MinigameProp, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public RectTransform m_table;

    public float m_gravity = 9.8f;

    Vector3 m_velocity;
    bool m_falling;

    public override void ResetState()
    {
        if(gameObject.activeSelf)
            return;

        base.ResetState();

        Canvas canvas = GetComponentInParent<Canvas>();
        transform.position += Vector3.up * canvas.pixelRect.height;
        m_falling = true;
        m_velocity = Vector3.zero;
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
        }
        else
        {
            m_velocity = Vector3.zero;
        }

        m_falling = true;
    }

    void Update()
    {
        if (m_falling)
        {       
            RectTransform rectTransform = (RectTransform)transform;
            Rect rect = rectTransform.rect;            
            rect.position += (Vector2)rectTransform.position;

            Rect tableRect = m_table.rect;            
            tableRect.position += (Vector2)m_table.position;

            if(rect.Overlaps(tableRect))
            {
                m_falling = false;
                return;
            }

            m_velocity.y -= m_gravity * Time.deltaTime;
            transform.position += m_velocity * Time.deltaTime;
            
            Canvas canvas = GetComponentInParent<Canvas>();
            if(rect.yMax < canvas.pixelRect.yMin || rect.xMax < canvas.pixelRect.xMin || rect.xMin > canvas.pixelRect.xMax)
                gameObject.SetActive(false);
        }
    }
}
