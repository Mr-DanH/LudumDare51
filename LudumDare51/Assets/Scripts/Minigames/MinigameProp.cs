using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameProp : MonoBehaviour
{    
    public RectTransform m_table;

    Vector3 m_pos;
    Vector3 m_scale;
    Quaternion m_rot;
    bool m_active;
    
    Vector3 m_velocity;
    protected bool m_falling;
    
    const float GRAVITY = 2000;

    public virtual void StoreState()
    {
        m_pos = transform.localPosition;
        m_scale = transform.localScale;
        m_rot = transform.localRotation;
        m_active = gameObject.activeSelf;
    }

    public virtual void ResetState()
    {
        transform.localPosition = m_pos;
        transform.localScale = m_scale;
        transform.localRotation = m_rot;
        gameObject.SetActive(m_active);
    }

    public void DropOnscreen()
    {        
        Canvas canvas = GetComponentInParent<Canvas>();
        transform.position += Vector3.up * canvas.pixelRect.height * Random.Range(1, 1.5f);
        m_falling = true;
        m_velocity = Vector3.zero;
    }

    public void Throw(Vector3 velocity)
    {
        m_velocity = velocity;
        m_falling = true;
    }
    
    public virtual void Update()
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

            m_velocity.y -= GRAVITY * Time.deltaTime;
            transform.position += m_velocity * Time.deltaTime;
            
            Canvas canvas = GetComponentInParent<Canvas>();
            if(rect.yMax < canvas.pixelRect.yMin || rect.xMax < canvas.pixelRect.xMin || rect.xMin > canvas.pixelRect.xMax)
            {
                gameObject.SetActive(false);
                LeavePlayArea();
            }
        }
    }

    public virtual void LeavePlayArea()
    {

    }
}
