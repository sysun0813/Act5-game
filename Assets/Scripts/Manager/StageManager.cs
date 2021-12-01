using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField] EndOfStage endOfStage;

    [SerializeField] CameraController cameraController;

    public Animator fadeAnim;

    [Header("�� ������")]
    [SerializeField] EnemySpawner enemySpawner;
    
    [Header("�÷��̾� ������")]
    [SerializeField] PlayerSpawner playerSpawner;

    [Space(30f)]

    [Header("��")]
    [SerializeField] List<GameObject> maps;

    [Header("�� ĳ����")]
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
        // �÷��̾ �������� ���� �������� �� ����� �Լ� �߰�
        endOfStage.OnFinishStage += StageResult;


    }

    public void StartStage(List<Main_Character> playerCharacters)
    {
        LoadStage();
        StartCoroutine(SetPlayerPosition(playerCharacters));
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

    public void LoadStage()
    {
        if (stageNum > 1)
        {
            previousStageInfo.map.SetActive(false);
        }
        currentStageInfo.map.SetActive(true);
    }

    // �÷��̾ �������� ���� �������� �� ����� �Լ�
    void StageResult()
    {
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

    public void StartSpawn()
    {
        StartCoroutine(SpawnEnemies(currentStageInfo.enemies));

    }


    // �÷��̾� ĳ���� Stage ���� �������� ����ġ
    public IEnumerator SetPlayerPosition(List<Main_Character> characters)
    {
        for (int i = 0; i < characters.Count; i++)
        {
            characters[i].gameObject.SetActive(true);
            playerSpawner.RePositionPlayer(characters[i]);
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
        //previousStageInfo.map.SetActive(false);
        LoadStage();

        // ����óġ �س��� ��
        endOfStage.GetComponent<BoxCollider2D>().enabled = true;

        fadeAnim.SetTrigger("FadeIn");
        //yield return StartCoroutine(SetPlayerPosition());
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