using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    // 캐릭터 도감
    [SerializeField] CharacterList characterList;



    public Main_Character InitializeCharacter(Main_Character characterSlot, string characterName)
    {
        if (!characterSlot.isInitialize)
        {

            Main_Character character = characterList.GetCharacter(characterName);
            characterSlot.Name = character.Name;
            characterSlot.maxHP = character.maxHP;
            characterSlot.attackPower = character.attackPower;
            characterSlot.defense = character.defense;
            characterSlot.attackDelay = character.attackDelay;
            characterSlot.moveSpeed = character.moveSpeed;
            characterSlot.attackRange = character.attackRange;

            characterSlot.GetComponent<SpriteRenderer>().sprite = character.characterImage;
            characterSlot.characterImage = character.characterImage;
            characterSlot.GetComponent<Animator>().runtimeAnimatorController = character.anim.runtimeAnimatorController;
            FindObjectOfType<MainUI>().MatchHpBar(characterSlot);
            characterSlot.isInitialize = true;

            return characterSlot;
        }
        else
        {
            return null;
        }
    }
}
