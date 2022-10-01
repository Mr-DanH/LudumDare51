using UnityEngine;
using UnityEngine.EventSystems;

public class StrengthItem : MonoBehaviour, IPointerDownHandler
{
    public StrengthMinigame m_minigame;

    public float m_damageShakeOffset = 10;
    public float m_damageShakeTime = 0.25f;

    int m_health;

    Vector3 m_startPos;
    float m_offsetTime;

    void Awake()
    {
        m_startPos = transform.localPosition;
    }

    void ResetProp()
    {
        m_health = 10;
        m_offsetTime = 0;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        --m_health;
        m_offsetTime = 1;

        if(m_health <= 0)
            gameObject.SetActive(false);
    }

    void Update()
    {
        m_offsetTime = Mathf.MoveTowards(m_offsetTime, 0, Time.deltaTime / m_damageShakeTime);
        float angle = 2 * Mathf.PI * Random.value;
        transform.localPosition = m_startPos + (new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * m_offsetTime * m_damageShakeOffset);
    }
}
