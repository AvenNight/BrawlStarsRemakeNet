using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CreatureFinder : ObjectsFinder
{
    public IReadOnlyList<GameObject> VisibleObjects => objects
        .Where(c => c.TryGetComponent(out IInvisible invisibility) && !invisibility.Invisible)
        .ToList()
        .AsReadOnly();

    public GameObject NearestVisibleObj => VisibleObjects
        .Aggregate((a, b) => thisObj.gameObject.GetDistanceTo(a) < thisObj.gameObject.GetDistanceTo(b) ? a : b);

    public Vector3 DirectionToVisible =>
        NearestVisibleObj.transform.position - thisObj.transform.position;

    public CreatureFinder(GameObject from, params string[] tags) : base(from, tags)
    {
        foreach (var e in objects)
        {
            if (e.TryGetComponent(out Creature creature))
                creature.DeathNotify += () => objects.Remove(e);
            else
                throw new ArgumentException($"GameObject with tag {tags} is not a 'Creature'");
        }
    }
}