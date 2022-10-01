using UnityEngine;
using UnityEngine.EventSystems;

public class Drink : MonoBehaviour//, IDragHandler
{
    public void OnDrag(PointerEventData eventData)
    {
        GetComponentInParent<DrinkMenu>().OnDrag(eventData);
    }

    public void Highlight(bool selected)
    {
    }
}
