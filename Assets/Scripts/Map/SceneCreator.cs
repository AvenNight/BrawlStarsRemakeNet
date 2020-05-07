using Photon.Pun;
using UnityEngine;

public class SceneCreator : MonoBehaviourPunCallbacks
{
    [SerializeField] GameObject wall;
    [SerializeField] GameObject grass;
    [SerializeField] GameObject ground;
    [SerializeField] GameObject water;
    [SerializeField] GameObject player;
    [SerializeField] GameObject enemy;
    [SerializeField] GameObject exit;

    private float wallSize => 2.2f;
    private float groundLevel => -1f;
    private Vector3 startingPoint => new Vector3(-9.9f, 0, -9.9f);
    private GameObject environment;
    private GameObject maps;

    public void CreateScene(char[,] map, bool init = true)
    {
        environment = GameObject.FindGameObjectWithTag("Environment");
        maps = GameObject.FindGameObjectWithTag("MapObjects");
        var curPos = startingPoint;
        for (int x = 0; x < map.GetLength(1); x++)
        {
            curPos.z = startingPoint.z;
            for (int y = 0; y < map.GetLength(0); y++)
            {
                // Create Map Object
                var mapObj = GetMapObjectByType(map[map.GetLength(0) - 1 - y, x], curPos);
                CreateObject(mapObj);
                // Create Ground
                if (init && mapObj?.Prefab != water)
                    CreateObject(new MapObjectData(ground, new Vector3(curPos.x, curPos.y + groundLevel, curPos.z), environment));
                curPos.z += wallSize;
            }
            curPos.x += wallSize;
        }
        FindObjectOfType<Spawner>()?.StartSpawner();
    }

    public void Refresh(char[,] map)
    {
        int n = maps.transform.childCount;
        for (int i = 0; i < n; i++)
            Destroy(maps.transform.GetChild(i).gameObject);
        CreateScene(map, false);
    }

    private MapObjectData GetMapObjectByType(char type, Vector3 location)
    {
        switch (type)
        {
            case 'w': return new MapObjectData(wall, location, environment);
            case 'g': return new MapObjectData(grass, location, environment);
            case 's': return new MapObjectData(water, location, environment);
            case 'p': return new MapObjectData(player, location, maps);
            case 'e': return new MapObjectData(enemy, location, maps);
            case 'x': return new MapObjectData(exit, location, maps);
            default: return null;
        }
    }

    private void CreateObject(MapObjectData mapObj)
    {
        if (mapObj == null || CheckPlayer(mapObj)) return;
        var curObj = mapObj.Prefab == player ? // тут переделать под сеть (сетевой/отслеживаемый объект или нет?)
            PhotonNetwork.Instantiate(mapObj.Prefab.name, mapObj.Location, Quaternion.identity) :
            Instantiate(mapObj.Prefab, mapObj.Location, Quaternion.identity);
        curObj.transform.SetParent(mapObj.Parrent.transform, true);
    }

    private bool CheckPlayer(MapObjectData mapObj) // костыль на скорую руку для кнопки Restart, которой в игре не будет
    {
        if (mapObj.Prefab == player)
        {
            var p = GameObject.FindGameObjectWithTag("Player");
            if (p != null)
            {
                p.transform.position = mapObj.Location;
                return true;
            }
        }
        return false;
    }
}