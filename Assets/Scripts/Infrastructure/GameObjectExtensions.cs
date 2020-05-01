using UnityEngine;

public static class GameObjectExtensions
{
    public static float GetDistanceTo(this GameObject from, GameObject to) =>
        Vector3.Distance(from.transform.position, to.transform.position);
}