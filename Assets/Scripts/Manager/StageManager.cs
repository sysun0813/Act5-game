using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField] EndStage endStage;

    [SerializeField] CameraController cameraController;

    [SerializeField] Animator fadeAnim;

    [Header("�� ������")]
    [SerializeField] EnemySpawner enemySpawner;
    
    [Header("�÷��̾� ������")]
    [SerializeField] PlayerSpawner playerSpawner;

    public List<Main_Character> players;
    
    public List<Main_Character> currentPlayers;

    public int playerspawnIndex;

    [Space(30f)]

    [Header("��")]
    [SerializeField] List<GameObject> maps;

    [Header("�� ĳ����")]
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
        // �÷��̾ �������� ���� �������� �� ����� �Լ� �߰�
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

    // �÷��̾ �������� ���� �������� �� ����� �Լ�
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

    // �÷��̾� ĳ���� Stage ���� �������� ����ġ
    IEnumerator SetPlayerPosition()
    {
        for (int i = 0; i < currentPlayers.Count; i++)
        {
            currentPlayers[i].gameObject.SetActive(true);
            playerSpawner.RePositionPlayer(currentPlayers[i]);
            yield return new WaitForSeconds(1f);
        }
    }

    // �÷��̾� ĳ���� ����
    IEnumerator SpawnPlayers()
    {
        for (int i = 0; i < players.Count; i++)
        {
            currentPlayers.Add(playerSpawner.SpawnPlayer(players[i]));
            yield return new WaitForSeconds(1f);
        }
    }

    // �� ĳ���� ����
    IEnumerator SpawnEnemies(List<Enemy> enemies)
    {
        for(int i = 0; i < enemies.Count; i++)
        {
            enemySpawner.SpawnEnemy(enemies[i]);
            yield return new WaitForSeconds(1f);
        }
    }

    // Stage ��ü
    IEnumerator ChangeStage()
    {
        yield return new WaitForSeconds(1f);
        cameraController.targetTransform = GameObject.Find("StageStartPoint").transform;
        previousStageInfo.map.SetActive(false);
        LoadStage(currentStageInfo);

        // ����óġ �س��� ��
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