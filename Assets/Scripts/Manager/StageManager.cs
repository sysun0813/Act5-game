using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField] EnemySpawner enemySpawner;

    public float enemyspawnDelay;

    public List<GameObject> enemies;

    int enemyspawnIndex;



    [SerializeField] PlayerSpawner playerSpawner;

    public float playerspawnDelay;

    public List<GameObject> players;

    int playerspawnIndex;


    private void Start()
    {
        StartCoroutine(StartSpawn());
    }

    IEnumerator StartSpawn()
    {
        while(enemyspawnIndex < enemies.Count||playerspawnIndex<players.Count)
        {
            if (enemyspawnIndex < enemies.Count)
            {
                yield return new WaitForSeconds(enemyspawnDelay);
                enemySpawner.SpawnEnemy(enemies[enemyspawnIndex]);
                enemyspawnIndex++;
            }


            if (playerspawnIndex < players.Count)
            {
                yield return new WaitForSeconds(playerspawnDelay);
                playerSpawner.SpawnPlayer(players[playerspawnIndex]);
                playerspawnIndex++;
            }

        }
    }
}
