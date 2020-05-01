using UnityEngine;

class InvisibilityAlly : MonoBehaviour, IInvisible
{
    public bool Invisible { get; set; }
    private bool canInvisible;
    private MeshRenderer meshRenderer;
    private Color startColor;
    [SerializeField] private float fade;

    private void Start()
    {
        canInvisible = TryGetComponent(out meshRenderer);
        startColor = meshRenderer.material.color;
    }

    public void SetInvisible(bool invisible)
    {
        if (!canInvisible) return;
        meshRenderer.material.color = new Color(startColor.r, startColor.g, startColor.b, invisible ? fade : 1f);
        Invisible = invisible;
    }
}