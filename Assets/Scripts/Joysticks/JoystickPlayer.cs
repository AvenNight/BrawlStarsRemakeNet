using Photon.Pun;
using System;
using System.Linq;
using UnityEngine;

public class JoystickPlayer : MonoBehaviour
{
    private Joystick joystick;
    private PhotonView photonView;

    [SerializeField]
    private float speed;
    [SerializeField]
    private Rigidbody rb;

    private void Start()
    {
        joystick = FindObjectsOfType<DynamicJoystick>().Single(j => !(j is DynamicJoybutton));
        photonView = GetComponent<PhotonView>();
    }

    public void FixedUpdate()
    {
        if (!photonView.IsMine) return;

        var direction = Vector3.forward * joystick.Vertical + Vector3.right * joystick.Horizontal
            + new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        rb.velocity = direction * speed;

        if (direction == Vector3.zero) return;
        transform.forward = direction;
    }
}