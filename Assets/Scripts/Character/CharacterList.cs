using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterList : MonoBehaviour
{
    [SerializeField] Main_Character haohmaru;
    [SerializeField] Main_Character dokanOta;

    public Main_Character Haohmaru() { return haohmaru; }

    public Main_Character GetCharacter(string characterName)
    {
        switch(characterName)
        {
            case "Haohmaru":
                return haohmaru;

            case "Dokan Ota":
                return dokanOta;

            default:
                return null;
        }
    }
}
