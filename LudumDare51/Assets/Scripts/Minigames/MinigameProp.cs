using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameProp : MonoBehaviour
{
    Vector3 m_pos;
    Vector3 m_scale;
    Quaternion m_rot;
    bool m_active;

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
}
