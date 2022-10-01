using UnityEngine;
using UnityEngine.EventSystems;

public class DrinkMenu : MinigameProp, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public DrinkMinigame m_minigame;
    public Vector3 m_largePosition;

    public Transform m_highlight;

    Vector3 m_startPosition;

    Vector3 m_targetSize;
    Vector3 m_targetPosition;
    
    Drink[] m_drinks;

    Drink m_selected;

    void Awake()
    {
        m_startPosition = transform.localPosition;
        m_drinks = GetComponentsInChildren<Drink>();
    }


    public override void ResetState()
    {
        base.ResetState();
        m_targetSize = Vector3.one * 0.5f;
        m_targetPosition = m_startPosition;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        m_targetSize = Vector3.one;
        m_targetPosition = m_largePosition;
        m_selected = null;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        m_targetSize = Vector3.one * 0.5f;
        m_targetPosition = m_startPosition;

        if(m_selected != null)
        {
            m_minigame.OrderDrink(m_selected);
            m_minigame.PropEvent(m_selected.m_event);
        }
    }

    void Update()
    {
        transform.localScale = Vector3.one * Mathf.MoveTowards(transform.localScale.x, m_targetSize.x, Time.deltaTime);

        transform.localPosition = Vector3.MoveTowards(transform.localPosition, m_targetPosition, (m_startPosition - m_largePosition).magnitude * Time.deltaTime * 2);
    }

    public void OnDrag(PointerEventData eventData)
    {
        m_selected = (eventData.pointerEnter != null) ? eventData.pointerEnter.GetComponentInParent<Drink>() : null;

        SelectDrink(m_selected);
    }

    public void SelectDrink(Drink selectedDrink)
    {
        m_highlight.gameObject.SetActive(selectedDrink != null);

        if(selectedDrink != null)
            m_highlight.transform.position = selectedDrink.transform.position;
    }
}
