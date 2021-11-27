using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField] EndStage endStage;

    [SerializeField] CameraController cameraController;

    [SerializeField] Animator fadeAnim;

    [Header("적 스포너")]
    [SerializeField] EnemySpawner enemySpawner;
    public float enemyspawnDelay;

    public List<Enemy> enemies;

    int enemyspawnIndex;


    [Header("플레이어 스포너")]
    [SerializeField] PlayerSpawner playerSpawner;
    
    public float playerspawnDelay;

    public List<Main_Character> players;
    
    public List<Main_Character> currentPlayers;

    public int playerspawnIndex;

    [Space(30f)]

    [Header("맵")]
    [SerializeField] List<GameObject> maps;

    [Header("적 캐릭터")]
    [SerializeField] List<Enemy> easyEnemies;
    [SerializeField] List<Enemy> normalEnemies;
    //[SerializeField] List<Enemy> hardEnemies;
    [SerializeField] List<Enemy> bossEnemies;

    int currentStage = 1;

    [SerializeField] StageInformation previousStageInfo;
    [SerializeField] StageInformation currentStageInfo;
    [SerializeField] StageInformation nextStageInfo;
    [SerializeField] StageInformation returnStageInfo;

    private void Start()
    {
        endStage.OnEndStage += LoadStage;

        currentStageInfo = MakeStage(currentStage);
        nextStageInfo = MakeStage(currentStage + 1);

        StartCoroutine(StartSpawn());
    }

    // EndStage 함수
    void LoadStage()
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

    StageInformation MakeStage(int stageNum)
    {
        StageInformation stage = new StageInformation
        {
            map = maps[Random.Range(0, maps.Count)],
            enemies = new List<Enemy>()
        };

        if(stageNum < 10)
        {
            stage.enemies.Add(GetRandomEnemy(easyEnemies));
        }
        else if (stageNum < 50)
        {
            for(int i = 0; i < 2; i++)
            {
                stage.enemies.Add(GetRandomEnemy(easyEnemies));
            }
        }
        else if(stageNum == 50)
        {
            stage.enemies.Add(GetRandomEnemy(bossEnemies));
            for(int i = 0; i < 2; i++)
            {
                stage.enemies.Add(GetRandomEnemy(easyEnemies));
            }
        }
        else if(stageNum < 100)
        {
            int randCount = Random.Range(3, 5);
            for(int i = 0; i < randCount; i++)
            {
                stage.enemies.Add(GetRandomEnemy(easyEnemies));
            }
        }

        return stage;
    }

    Enemy GetRandomEnemy(List<Enemy> enemiesList)
    {
        return enemiesList[Random.Range(0, enemiesList.Count)];
    }

}

[System.Serializable]
public class StageInformation
{
    public GameObject map;
    public List<Enemy> enemies;
}