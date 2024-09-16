using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonFunctions : MonoBehaviour, IPointerDownHandler
{
    public Action OnRightMouseClick;
    public Action OnLeftMouseClick;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            OnRightMouseClick?.Invoke();
        }
        else if (eventData.button == PointerEventData.InputButton.Left)
        {
            OnLeftMouseClick?.Invoke();
        }
    }
}
