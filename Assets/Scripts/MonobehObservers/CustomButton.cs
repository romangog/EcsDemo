using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CustomButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{

    public bool _IsPointerDown;
    private bool _IsPointerOn;

    public void OnPointerDown(PointerEventData eventData)
    {
        _IsPointerDown = true;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _IsPointerOn = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _IsPointerOn = false;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _IsPointerDown = false;
    }
}
