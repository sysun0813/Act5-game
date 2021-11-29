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
    
    [Header("플레이어 스포너")]
    [SerializeField] PlayerSpawner playerSpawner;

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
        // 플레이어가 스테이지 끝에 도달했을 때 실행될 함수 추가
        endStage.OnEndStage += UpdateStage;

        currentStageInfo = MakeStage(currentStage);
        nextStageInfo = MakeStage(currentStage + 1);

        LoadStage(currentStageInfo);

    }

    public void StartSpawn()
    {
        StartCoroutine(SpawnPlayers());
        StartCoroutine(SpawnEnemies(currentStageInfo.enemies));

    }

    void LoadStage(StageInformation stageInfo)
    {
        stageInfo.map.SetActive(true);
    }

    // 플레이어가 스테이지 끝에 도달했을 때 실행될 함수
    void UpdateStage()
    {
        currentStage++;

        previousStageInfo = currentStageInfo;
        currentStageInfo = nextStageInfo;
        nextStageInfo = MakeStage(currentStage + 1);

        for(int i = 0; i < currentPlayers.Count; i++)
        {
            currentPlayers[i].gameObject.SetActive(false);
        }

        fadeAnim.SetTrigger("FadeOut");

        StartCoroutine(ChangeStage());
    }

    // 플레이어 캐릭터 Stage 시작 지점으로 재위치
    IEnumerator SetPlayerPosition()
    {
        for (int i = 0; i < currentPlayers.Count; i++)
        {
            currentPlayers[i].gameObject.SetActive(true);
            playerSpawner.RePositionPlayer(currentPlayers[i]);
            yield return new WaitForSeconds(1f);
        }
    }

    // 플레이어 캐릭터 생성
    IEnumerator SpawnPlayers()
    {
        for (int i = 0; i < players.Count; i++)
        {
            currentPlayers.Add(playerSpawner.SpawnPlayer(players[i]));
            yield return new WaitForSeconds(1f);
        }
    }

    // 적 캐릭터 생성
    IEnumerator SpawnEnemies(List<Enemy> enemies)
    {
        for(int i = 0; i < enemies.Count; i++)
        {
            enemySpawner.SpawnEnemy(enemies[i]);
            yield return new WaitForSeconds(1f);
        }
    }

    // Stage 교체
    IEnumerator ChangeStage()
    {
        yield return new WaitForSeconds(1f);
        cameraController.targetTransform = GameObject.Find("StageStartPoint").transform;
        previousStageInfo.map.SetActive(false);
        LoadStage(currentStageInfo);

        // 응급처치 해놓은 거
        endStage.GetComponent<BoxCollider2D>().enabled = true;

        fadeAnim.SetTrigger("FadeIn");
        yield return StartCoroutine(SetPlayerPosition());
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