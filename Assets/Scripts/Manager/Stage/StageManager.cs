using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField] EndStage endStage;

    [SerializeField] CameraController cameraController;

    [SerializeField] Animator fadeAnim;


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

    // EndStage ÇÔ¼ö
    void ReStartStage()
    {
        while(currentPlayers.Count > 0)
        {
            Destroy(currentPlayers[0].gameObject);
            currentPlayers.RemoveAt(0);
        }
        playerspawnIndex = 0;
        enemyspawnIndex = 0;

        fadeAnim.SetTrigger("FadeOut");
        StartCoroutine(ChangeStage());
        
    }
    IEnumerator StartSpawn()
    {
        while (enemyspawnIndex < enemies.Count||playerspawnIndex<players.Count)
        {
            if (playerspawnIndex < players.Count)
            {
                yield return new WaitForSeconds(playerspawnDelay);
                currentPlayers.Add(playerSpawner.SpawnPlayer(players[playerspawnIndex]));
                playerspawnIndex++;
            }
            if (enemyspawnIndex < enemies.Count)
            {
                yield return new WaitForSeconds(enemyspawnDelay);
                enemySpawner.SpawnEnemy(enemies[enemyspawnIndex]);
                enemyspawnIndex++;
            }
        }
    }

    IEnumerator ChangeStage()
    {
        yield return new WaitForSeconds(1f);
        cameraController.targetTransform = GameObject.Find("StageStartPoint").transform;
        fadeAnim.SetTrigger("FadeIn");
        yield return StartCoroutine(StartSpawn());
    }
}
