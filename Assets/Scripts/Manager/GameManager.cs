using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("하위 매니저")]
    [SerializeField] StageManager stageManager;
    [SerializeField] CharacterManager characterManager;

    [SerializeField] List<Main_Character> playerCharacters;

    int activePlayerCharacterCount = 1;

    private void Start()
    {
        characterManager.InitializeCharacter(playerCharacters[0], "Haohmaru");
    }
}