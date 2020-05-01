using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class DynamicJoybutton : DynamicJoystick
{
    [HideInInspector]
    public bool Pressed;
    [HideInInspector]
    public bool Holded;
    [SerializeField]
    private float holdTimeSec;

    public event Action PressDownNotify;
    public event Action PressUpNotify;

    public override void OnPointerDown(PointerEventData eventData)
    {
        PressDownNotify?.Invoke();
        Pressed = true;
        StartCoroutine("HooldingTime");
        base.OnPointerDown(eventData);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        PressUpNotify?.Invoke();
        Pressed = false;
        Holded = false;
        StopCoroutine("HooldingTime");
        base.OnPointerUp(eventData);
    }

    private IEnumerator HooldingTime()
    {
        yield return new WaitForSeconds(holdTimeSec);
        Holded = Pressed;
        //Holded = input != Vector2.zero && Pressed;
    }
}
