using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSlot : MonoBehaviour
{
    [SerializeField] Image characterImage;

    [SerializeField] Animator anim;

    [SerializeField] Transform HpBar;

    [SerializeField] Text levelText;


    public void SetData(Main_Character character)
    {
        characterImage.sprite = character.characterImage;
        anim.SetTrigger($"{character.Name}");

    }
}
