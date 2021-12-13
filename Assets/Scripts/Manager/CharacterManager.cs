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
            characterSlot.transform.GetChild(0).localPosition = character.transform.GetChild(0).localPosition;
            characterSlot.transform.GetChild(1).localPosition = character.transform.GetChild(1).localPosition;
            characterSlot.transform.GetChild(1).localScale = character.transform.GetChild(1).localScale;
            if(character.projectilePrefab != null)
            {
                characterSlot.projectilePrefab = character.projectilePrefab;
            }
            else
            {
                characterSlot.projectilePrefab = null;
            }
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
