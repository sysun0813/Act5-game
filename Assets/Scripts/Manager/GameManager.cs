using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EGameState
{
    Run, Stop
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public EGameState gameState;

    [Header("하위 매니저")]
    [SerializeField] StageManager stageManager;
    [SerializeField] CharacterManager characterManager;

    [SerializeField] List<Main_Character> characterSlots;
    [SerializeField] List<Main_Character> playerCharacters;

    [SerializeField] CameraController cameraController;
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
        playerCharacters.Add(characterManager.InitializeCharacter(characterSlots[0], "Haohmaru"));
        stageManager.InitStage(true);
        stageManager.StartStage(playerCharacters);

    }

    private void Update()
    {
        switch(gameState)
        {
            case EGameState.Run:
                cameraController.CameraLateUpdate(playerCharacters);
                for(int i = 0; i < playerCharacters.Count; i++)
                {
                    playerCharacters[i].PlayerCharacterUpdate();
                }
                break;

            case EGameState.Stop:

                break;
        }
    }

    public void ProgressStage(bool won)
    {
        StartCoroutine(ReadyToStartStage(won));
    }

    public void StopStage()
    {
        gameState = EGameState.Stop;
        for(int i = 0; i < playerCharacters.Count; i++)
        {
            playerCharacters[i].anim.SetBool("IsMove", false);
        }
    }

    IEnumerator ReadyToStartStage(bool won)
    {
        yield return new WaitForSeconds(1f);
        stageManager.InitStage(won);
        for(int i = 0; i <playerCharacters.Count; i++)
        {
            playerCharacters[i].SetCurrentHp();
        }
        stageManager.fadeAnim.SetTrigger("FadeIn");
        stageManager.StartStage(playerCharacters);
        gameState = EGameState.Run;
    }
}