using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectsFinder
{
    protected readonly GameObject thisObj;
    protected readonly List<GameObject> objects;
    public IReadOnlyList<GameObject> Objects => objects.AsReadOnly();

    public GameObject NearestObject => objects
        .Aggregate((a, b) => thisObj.gameObject.GetDistanceTo(a) < thisObj.gameObject.GetDistanceTo(b) ? a : b);

    public Vector3 Direction => NearestObject.transform.position - thisObj.transform.position;

    public float DistanceToNearest => thisObj.gameObject.GetDistanceTo(NearestObject);

    public ObjectsFinder(GameObject from, params string[] tags)
    {
        thisObj = from;
        objects = new List<GameObject>();
        foreach (var tag in tags)
            objects.AddRange(GameObject.FindGameObjectsWithTag(tag));
    }
}