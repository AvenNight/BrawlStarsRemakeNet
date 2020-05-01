using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    [SerializeField] private SceneCreator sceneCreator;
    [SerializeField] private NavMeshSurface navMesh;

    private char[,] curMap = Levels.Level5;

    void Start()
    {
        sceneCreator.CreateScene(curMap);
        navMesh.BuildNavMesh();
        //Time.timeScale = 2f;
    }

    public void Refresh()
    {
        sceneCreator.Refresh(curMap);
    }

    void Update()
    {

    }
}