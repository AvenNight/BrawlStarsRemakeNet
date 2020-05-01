using UnityEngine;

public class DrawCone : MonoBehaviour
{
    [HideInInspector] public float Angle;
    [HideInInspector] public float Distance;
    [HideInInspector] public bool Enable;

    [SerializeField] public Color Color;
    private LineRenderer lineRenderer;

    private Transform tf;

    private void Awake()
    {
        tf = this.transform;
        gameObject.AddComponent<LineRenderer>();
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.widthMultiplier = 0.05f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = Color;
        lineRenderer.endColor = Color;
        lineRenderer.positionCount = 4;
    }

    private void FixedUpdate()
    {
        lineRenderer.enabled = Enable;
        if (!Enable) return;
        Quaternion upRayRotation = Quaternion.AngleAxis(-Angle / 2, Vector3.up);
        Quaternion downRayRotation = Quaternion.AngleAxis(Angle / 2, Vector3.up);

        Vector3 upRayDirection = upRayRotation * tf.forward * Distance;
        Vector3 downRayDirection = downRayRotation * tf.forward * Distance;

        //lineRenderer.SetPosition(0, tf.position);
        //lineRenderer.SetPosition(1, tf.position + tf.forward * Distance);
        //lineRenderer.endWidth = (upRayDirection - downRayDirection).magnitude; // так было бы прикольнее, если бы не поворот лицом к камере =)

        lineRenderer.SetPosition(0, tf.position);
        lineRenderer.SetPosition(1, tf.position + upRayDirection);
        lineRenderer.SetPosition(2, tf.position + downRayDirection);
        lineRenderer.SetPosition(3, tf.position);
    }
}