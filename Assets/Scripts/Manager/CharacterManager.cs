using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    [SerializeField] CharacterList characterList;



    public Main_Character InitializeCharacter(Main_Character characterSlot, string characterName)
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
        characterSlot.GetComponent<Animator>().runtimeAnimatorController = character.anim.runtimeAnimatorController;
        characterSlot.gameObject.SetActive(true);

        return characterSlot;
    }
}
