using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField] EndStage endStage;

    [SerializeField] EnemySpawner enemySpawner;

    public float enemyspawnDelay;

    public List<Enemy> enemies;

    int enemyspawnIndex;



    [SerializeField] PlayerSpawner playerSpawner;
    
    public float playerspawnDelay;

    public List<Main_Character> players;
    
    public List<Main_Character> currentPlayers;

    public int playerspawnIndex;


    private void Start()
    {
        endStage.OnEndStage += ReStartStage;
        StartCoroutine(StartSpawn());
    }

    void ReStartStage()
    {
        while(currentPlayers.Count > 0)
        {
            Destroy(currentPlayers[0].gameObject);
            currentPlayers.RemoveAt(0);
        }
        playerspawnIndex = 0;
        enemyspawnIndex = 0;
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
                currentPlayers.Add(playerSpawner.SpawnPlayer(players[playerspawnIndex]));
                playerspawnIndex++;
            }

        }
    }

    
}
