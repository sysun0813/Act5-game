using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField] MainUI mainUI;

    [SerializeField] EndOfStage endOfStage;

    [SerializeField] CameraController cameraController;

    public Animator fadeAnim;

    [Header("적 스포너")]
    [SerializeField] EnemySpawner enemySpawner;
    
    [Header("플레이어 스포너")]
    [SerializeField] PlayerSpawner playerSpawner;

    [Space(30f)]

    [Header("맵")]
    [SerializeField] List<GameObject> maps;

    [Header("적 캐릭터")]
    [SerializeField] List<Enemy> easyEnemies;
    [SerializeField] List<Enemy> normalEnemies;
    //[SerializeField] List<Enemy> hardEnemies;
    [SerializeField] List<Enemy> bossEnemies;

    int stageNum = 1;

    [SerializeField] StageInformation previousStageInfo;
    [SerializeField] StageInformation currentStageInfo;
    [SerializeField] StageInformation nextStageInfo;
    [SerializeField] StageInformation returnStageInfo;

    private void Start()
    {
        // 플레이어가 스테이지 끝에 도달했을 때 실행될 함수 추가
        endOfStage.OnFinishStage += StageResult;

    }

    public void InitStage(bool won)
    {
        if(currentStageInfo.map == null)
        {
            currentStageInfo = MakeStageInfo(stageNum);
            nextStageInfo = MakeStageInfo(stageNum + 1);
        }
        else
        {
            if(won)
            {
                previousStageInfo = currentStageInfo;
                currentStageInfo = nextStageInfo;
                nextStageInfo = MakeStageInfo(stageNum + 1);
            }
            else
            {
                nextStageInfo = currentStageInfo;
                currentStageInfo = previousStageInfo;
                previousStageInfo = MakeStageInfo(stageNum - 1);
            }
        }
    }
    public void StartStage(List<Main_Character> playerCharacters)
    {
        LoadStage();
        StartCoroutine(SetPlayerPosition(playerCharacters));
        StartCoroutine(SpawnEnemies(currentStageInfo.enemies));

        mainUI.SetCharacterList(playerCharacters);
        mainUI.IndicateStage(stageNum);
        endOfStage.GetComponent<BoxCollider2D>().enabled = true;
    }

    public void LoadStage()
    {
        if (stageNum > 1)
        {
            previousStageInfo.map.SetActive(false);
        }
        currentStageInfo.map.SetActive(true);
    }

    // 플레이어가 스테이지 끝에 도달했을 때 실행될 함수
    void StageResult()
    {
        fadeAnim.gameObject.SetActive(true);
        fadeAnim.SetTrigger("FadeOut");
        FinishStage(true);
    }

    void FinishStage(bool won)
    {
        if(won)
        {
            stageNum++;
        }
        else
        {
            stageNum--;
        }
        GameManager.Instance.ProgressStage(won);
    }

    // 플레이어 캐릭터 Stage 시작 지점으로 재위치
    public IEnumerator SetPlayerPosition(List<Main_Character> characters)
    {
        for (int i = 0; i < characters.Count; i++)
        {
            characters[i].gameObject.SetActive(true);
            playerSpawner.RePositionPlayer(characters[i]);
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


    StageInformation MakeStageInfo(int stageNum)
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