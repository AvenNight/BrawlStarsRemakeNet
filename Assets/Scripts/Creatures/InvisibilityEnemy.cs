using System.Collections.Generic;
using UnityEngine;

class InvisibilityEnemy : MonoBehaviour, IInvisible
{
    public bool Invisible { get; set; }
    private bool canInvisible;
    private MeshRenderer meshRenderer;
    private Color startColor;
    private List<GameObject> childs;
    [SerializeField] private float fade;


    private void Start()
    {
        canInvisible = TryGetComponent(out meshRenderer);
        startColor = meshRenderer.material.color;
        childs = new List<GameObject>();
        int n = this.transform.childCount;
        for (int i = 0; i < n; i++)
            childs.Add(this.transform.GetChild(i).gameObject);
    }

    public void SetInvisible(bool invisible)
    {
        if (!canInvisible) return;
        meshRenderer.material.color = new Color(startColor.r, startColor.g, startColor.b, invisible ? fade : 1f);
        foreach (var c in childs)
            c.SetActive(!invisible);
        Invisible = invisible;
    }
}