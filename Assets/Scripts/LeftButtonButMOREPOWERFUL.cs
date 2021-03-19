using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LeftButtonButMOREPOWERFUL : Button
{
    public static bool pressed;
    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
        pressed = true;
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
        pressed = false;
    }
}
