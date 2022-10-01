using UnityEngine;
using UnityEngine.EventSystems;

public class StrengthButton : MonoBehaviour, IPointerDownHandler
{
    public StrengthMinigame m_minigame;

    public void OnPointerDown(PointerEventData eventData)
    {
        m_minigame.OnPress();
    }
}
