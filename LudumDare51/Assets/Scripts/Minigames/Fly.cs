using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Fly : MinigameProp, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        gameObject.SetActive(false);
    }
}
