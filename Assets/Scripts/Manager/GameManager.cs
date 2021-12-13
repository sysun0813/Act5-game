using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EGameState
{
    Run, Stop
}

public class GameManager : MonoBehaviour
{
    // 싱글톤
    public static GameManager Instance;

    // 게임 상태
    public EGameState gameState;

    [Header("하위 매니저")]
    [SerializeField] StageManager stageManager;             // 스테이지 매니저
    [SerializeField] CharacterManager characterManager;     // 캐릭터 매니저

    [SerializeField] List<Main_Character> characterSlots;   // 캐릭터 슬롯
    public List<Main_Character> playerCharacters; // 현재 출전 중인 캐릭터

    [SerializeField] CameraController cameraController;     // 카메라 컨트롤러
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
        // 하오마루 캐릭터 추가
        //playerCharacters.Add(characterManager.InitializeCharacter(characterSlots[0], "Haohmaru"));
        playerCharacters.Add(characterManager.InitializeCharacter(characterSlots[0], "Dokan Ota"));

        // 스테이지 설정
        stageManager.InitStage(true);

        // 스테이지 시작
        stageManager.StartStage(playerCharacters);

    }

    private void Update()
    {
        switch(gameState)
        {
            // Run일 때 
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

            // Stop일 때
            case EGameState.Stop:

                break;
        }
    }

    // 스테이지 진행시키는 함수
    public void ProgressStage(bool won)
    {
        StartCoroutine(ReadyToStartStage(won));
    }

    // 스테이지 멈추는 함수
    public void StopStage()
    {
        gameState = EGameState.Stop;
        for(int i = 0; i < playerCharacters.Count; i++)
        {
            playerCharacters[i].anim.SetBool("IsMove", false);
        }
    }

    // 스테이지를 준비하는 코루틴
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