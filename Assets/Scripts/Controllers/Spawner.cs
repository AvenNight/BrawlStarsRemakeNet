using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Player playerPrefab;
    [SerializeField] private EnemyWithAI enemyPrefab;
    private Player curPlayer;
    private Vector3 playerPosition;

    public void StartSpawner()
    {
        curPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        playerPosition = curPlayer.transform.position;
        curPlayer.DeathNotify += Respawn;
    }

    private void Respawn() => StartCoroutine("RespawnPlayer");

    private IEnumerator RespawnPlayer()
    {
        yield return new WaitForSeconds(3);
        if (GameObject.FindGameObjectWithTag("Player") == null)
        {
            curPlayer = Instantiate(playerPrefab, playerPosition, Quaternion.identity);
            curPlayer.transform.SetParent(GameObject.FindGameObjectWithTag("MapObjects").transform, true);
            curPlayer.DeathNotify += Respawn;
        }
    }

    private void OnDestroy() => curPlayer.DeathNotify -= Respawn;
}