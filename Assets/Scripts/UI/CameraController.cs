using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform Target;
    public float Speed;

    private Transform camera;
    private Vector3 offset;


    private void Start()
    {
        camera = this.transform;
        offset = camera.position;
        if (Target != null)
            camera.position = Target.position + offset;
    }

    private void LateUpdate()
    {
        if (Target != null)
            camera.position = Vector3.Lerp(camera.position, Target.position + offset, Speed * Time.deltaTime);
    }
}