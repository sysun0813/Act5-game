using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField] EnemySpawner enemySpawner;

    public float spawnDelay;

    [SerializeField] List<GameObject> enemies;

    int spawnIndex;

    private void Start()
    {
        StartCoroutine(StartSpawn());
    }

    IEnumerator StartSpawn()
    {
        while(spawnIndex < enemies.Count)
        {
            yield return new WaitForSeconds(spawnDelay);
            enemySpawner.SpawnEnemy(enemies[spawnIndex]);
            spawnIndex++;
        }
    }
}
