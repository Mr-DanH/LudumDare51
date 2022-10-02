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
    Vector3 m_angularVelocity;
    bool m_falling;
    bool m_hasFell;
    
    const float GRAVITY = 2000;

    public Vector3 StartPos { get { return m_pos; } }
    public bool IsFalling { get { return m_falling; } }

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
        Throw(velocity, Vector3.zero);
    }
    public void Throw(Vector3 velocity, Vector3 angularVelocity)
    {
        m_velocity = velocity;
        m_angularVelocity = angularVelocity;
        m_falling = true;
        m_hasFell = false;
    }

    public Rect GetWorldRect()
    {
        RectTransform rectTransform = (RectTransform)transform;
        Rect rect = rectTransform.rect;
        rect.position *= (Vector2)rectTransform.lossyScale; 
        rect.size *= (Vector2)rectTransform.lossyScale;           
        rect.position += (Vector2)rectTransform.position;
        return rect;
    }

    public Rect GetCanvasRect()
    {
        Canvas canvas = GetComponentInParent<Canvas>();
        return canvas.pixelRect;
    }
    
    public virtual void Update()
    {
        if (m_falling)
        {       
            Rect rect = GetWorldRect();

            if(m_table != null)
            {
                Rect tableRect = m_table.rect;  
                tableRect.position *= (Vector2)m_table.lossyScale;  
                tableRect.size *= (Vector2)m_table.lossyScale;         
                tableRect.position += (Vector2)m_table.position;

                if(rect.Overlaps(tableRect))
                {
                    if(m_hasFell)
                    {
                        float yOverlap = tableRect.yMax - rect.yMin;
                        transform.position += Vector3.up * yOverlap;
                    }
                    m_falling = false;
                    return;
                }
            }

            m_velocity.y -= GRAVITY * Time.deltaTime;
            transform.position += m_velocity * Time.deltaTime;
            transform.rotation *= Quaternion.AngleAxis(m_angularVelocity.magnitude * Time.deltaTime, m_angularVelocity);
            
            Rect canvasRect = GetCanvasRect();
            if(rect.yMax < canvasRect.yMin || rect.xMax < canvasRect.xMin || rect.xMin > canvasRect.xMax)
            {
                m_falling = false;
                gameObject.SetActive(false);
                LeavePlayArea();
            }

            m_hasFell = true;
        }
    }

    public virtual void LeavePlayArea()
    {

    }
}
