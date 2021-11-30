using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("���� �Ŵ���")]
    [SerializeField] StageManager stageManager;
    [SerializeField] CharacterManager characterManager;

    [SerializeField] List<Main_Character> playerCharacters;

    int activePlayerCharacterCount = 1;

    private void Start()
    {
        characterManager.InitializeCharacter(playerCharacters[0], "Haohmaru");
    }
}