using UnityEngine;

public class MapObjectData
{
    public GameObject Prefab { get; }
    public Vector3 Location { get; }
    public GameObject Parrent { get; }

    public MapObjectData(GameObject prefab, Vector3 location, GameObject parrent)
    {
        Prefab = prefab;
        Location = location;
        Parrent = parrent;
    }
}
