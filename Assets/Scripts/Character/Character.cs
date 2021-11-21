using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [Header("Ä³¸¯ÅÍ ½ºÅÈ")]
    public string Name;

    public float maxHP;

    public float currentHP;

    public float attackPower;

    public float defense;

    public float attackDelay;

    public float moveSpeed;

    public float attackRange;

    public void MoveCharacter(bool isPlayer)
    {
        if(isPlayer)
        {
            transform.position += Vector3.right * moveSpeed * Time.deltaTime; 
        }
        else
        {
            transform.position -= (Vector3.right * moveSpeed * Time.deltaTime);
        }
    }
}
