using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EGameState
{
    Run, Stop
}

public class GameManager : MonoBehaviour
{
    // �̱���
    public static GameManager Instance;

    // ���� ����
    public EGameState gameState;

    [Header("���� �Ŵ���")]
    [SerializeField] StageManager stageManager;             // �������� �Ŵ���
    [SerializeField] CharacterManager characterManager;     // ĳ���� �Ŵ���

    [SerializeField] List<Main_Character> characterSlots;   // ĳ���� ����
    public List<Main_Character> playerCharacters; // ���� ���� ���� ĳ����

    [SerializeField] CameraController cameraController;     // ī�޶� ��Ʈ�ѷ�
    int activePlayerCharacterCount = 1;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        // �Ͽ����� ĳ���� �߰�
        //playerCharacters.Add(characterManager.InitializeCharacter(characterSlots[0], "Haohmaru"));
        playerCharacters.Add(characterManager.InitializeCharacter(characterSlots[0], "Dokan Ota"));

        // �������� ����
        stageManager.InitStage(true);

        // �������� ����
        stageManager.StartStage(playerCharacters);

    }

    private void Update()
    {
        switch(gameState)
        {
            // Run�� �� 
            case EGameState.Run:
                cameraController.CameraLateUpdate(playerCharacters);
                for(int i = 0; i < playerCharacters.Count; i++)
                {
                    if(playerCharacters[i].gameObject.activeSelf)
                    {
                        playerCharacters[i].PlayerCharacterUpdate();
                    }
                }
                break;

            // Stop�� ��
            case EGameState.Stop:

                break;
        }
    }

    // �������� �����Ű�� �Լ�
    public void ProgressStage(bool won)
    {
        StartCoroutine(ReadyToStartStage(won));
    }

    // �������� ���ߴ� �Լ�
    public void StopStage()
    {
        gameState = EGameState.Stop;
        for(int i = 0; i < playerCharacters.Count; i++)
        {
            playerCharacters[i].anim.SetBool("IsMove", false);
        }
    }

    // ���������� �غ��ϴ� �ڷ�ƾ
    IEnumerator ReadyToStartStage(bool won)
    {
        yield return new WaitForSeconds(1f);
        stageManager.InitStage(won);
        for(int i = 0; i < playerCharacters.Count; i++)
        {
            playerCharacters[i].InitCurrentHp();
            playerCharacters[i].hpBar.InitHp();
            if(i > 0)
            {
                playerCharacters[i].gameObject.SetActive(false);
            }
        }
        stageManager.fadeAnim.SetTrigger("FadeIn");
        stageManager.StartStage(playerCharacters);
        gameState = EGameState.Run;
    }
}