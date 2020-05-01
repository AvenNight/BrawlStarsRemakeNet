using System;
using UnityEngine;

public class JoybuttonPlayer : MonoBehaviour
{
    private DynamicJoybutton joybutton;
    public Vector3 Direction => Vector3.forward * joybutton.Vertical + Vector3.right * joybutton.Horizontal;

    private RangeWeapon curWeapon;
    public DrawCone DrawCone;

    public event Action<Vector3> HoldShootNotify;
    public event Action ShootNotify;

    private void Start()
    {
        curWeapon = this.GetComponent<Player>().curWeapon;
        joybutton = FindObjectOfType<DynamicJoybutton>();
        joybutton.PressUpNotify += Joybutton_PressUpNotify;
        joybutton.PressDownNotify += Joybutton_PressDownNotify;
    }

    private void Joybutton_PressDownNotify()
    {
        DrawCone.Distance = curWeapon.Distance;
        DrawCone.Angle = curWeapon.Angle;
    }

    private void Joybutton_PressUpNotify()
    {
        DrawCone.Enable = false;
        if (joybutton.Holded)
            HoldShootNotify?.Invoke(Direction);
        else
            ShootNotify?.Invoke();
    }

    public void FixedUpdate()
    {
        if (joybutton.Holded && joybutton.Input != Vector2.zero && DrawCone.Enable == false)
            DrawCone.Enable = true;
        if (joybutton.Direction == Vector2.zero) return;
        transform.forward = Direction;
    }

    private void OnDestroy()
    {
        joybutton.PressUpNotify -= Joybutton_PressUpNotify;
        joybutton.PressDownNotify -= Joybutton_PressDownNotify;
    }
}