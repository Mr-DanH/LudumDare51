using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DrinkMenu : MinigameProp, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public DrinkMinigame m_minigame;
    public Vector3 m_largePosition;
    public Vector3 m_largeSize;
    public Vector3 m_largeRotation;

    public Image m_highlight;

    Vector3 m_startPosition;
    Vector3 m_startSize;
    Quaternion m_startRotation;

    Vector3 m_fromSize;
    Vector3 m_fromPosition;
    Quaternion m_fromRotation;
    Vector3 m_targetSize;
    Vector3 m_targetPosition;
    Quaternion m_targetRotation;
    float m_lerpTime;
    
    Drink[] m_drinks;

    Drink m_selected;
    bool m_drinkOrdered;

    void Awake()
    {
        m_startPosition = transform.localPosition;
        m_startSize = transform.localScale;
        m_startRotation = transform.localRotation;
        m_drinks = GetComponentsInChildren<Drink>();
    }


    public override void ResetState()
    {
        base.ResetState();
        m_highlight.gameObject.SetActive(false);
        m_targetSize = m_startSize;
        m_targetPosition = m_startPosition;
        m_targetRotation = m_startRotation;
        m_lerpTime = 1;
        m_drinkOrdered = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(m_drinkOrdered)
            return;

        StartLerp(m_largeSize, m_largePosition, Quaternion.Euler(m_largeRotation));
        m_selected = null;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if(m_drinkOrdered)
            return;

        StartLerp(m_startSize, m_startPosition, m_startRotation);

        if(m_selected != null)
        {
            m_drinkOrdered = true;
            m_minigame.OrderDrink(m_selected);
            m_minigame.PropEvent(m_selected.m_event);
        }
    }

    void StartLerp(Vector3 size, Vector3 pos, Quaternion rot)
    {
        m_lerpTime = 0;
        m_fromSize = transform.localScale;
        m_fromPosition = transform.localPosition;
        m_fromRotation = transform.localRotation;
        m_targetSize = size;
        m_targetPosition = pos;
        m_targetRotation = rot;
    }

    public override void Update()
    {
        base.Update();

        m_lerpTime += Time.deltaTime * 2;
        transform.localScale = Vector3.Lerp(m_fromSize, m_targetSize, m_lerpTime);
        transform.localPosition = Vector3.Lerp(m_fromPosition, m_targetPosition, m_lerpTime);
        transform.localRotation = Quaternion.Lerp(m_fromRotation, m_targetRotation, m_lerpTime);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(m_drinkOrdered)
            return;

        m_selected = (eventData.pointerEnter != null) ? eventData.pointerEnter.GetComponentInParent<Drink>() : null;

        SelectDrink(m_selected);
    }

    public void SelectDrink(Drink selectedDrink)
    {
        m_highlight.gameObject.SetActive(selectedDrink != null);

        if(selectedDrink != null)
        {
            m_highlight.transform.position = selectedDrink.transform.position;
            m_highlight.sprite = selectedDrink.m_sprite;
        }
    }
}
