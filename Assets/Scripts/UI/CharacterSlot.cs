using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSlot : MonoBehaviour
{
    [SerializeField] Image characterImage;

    [SerializeField] Animator anim;

    [SerializeField] RectTransform HpBar;

    [SerializeField] Text levelText;

    Main_Character character;
    public void SetData(Main_Character character)
    {
        characterImage.sprite = character.characterImage;
        anim.SetTrigger($"{character.Name}");
        this.character = character;
    }

    private void LateUpdate()
    {
        if(character != null)
        {
            float ratio = (float)character.currentHP / character.maxHP;
            HpBar.localScale = new Vector3(ratio, 1f, 1f);
        }
    }
}
