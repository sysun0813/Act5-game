using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterList : MonoBehaviour
{
    [SerializeField] Main_Character haohmaru;
    [SerializeField] Main_Character dokanOta;
    [SerializeField] Main_Character holyBeastSoldier;
    [SerializeField] Main_Character musashiMiyamoto;

    public Main_Character Haohmaru() { return haohmaru; }

    public Main_Character GetCharacter(string characterName)
    {
        switch(characterName)
        {
            case "Haohmaru":
                return haohmaru;

            case "Dokan Ota":
                return dokanOta;

            case "Holy Beast Soldier":
                return holyBeastSoldier;

            case "Musashi Miyamoto":
                return musashiMiyamoto;
            default:
                return null;
        }
    }
}
