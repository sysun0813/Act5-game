using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [Header("캐릭터 스탯")]
    public string Name;

    public float maxHP;

    public float currentHP;

    public float attackPower;

    public float defense;

    public float attackDelay;

    public float moveSpeed;

    public float attackRange;

    [Header("플레이어 캐릭터인지 확인")]
    public bool isPlayerCharacter;

    [Header("타겟 캐릭터")]
    protected Character targetCharacter;

    [Header("피격 이팩트")]
    public GameObject hitEffect;



    public void MoveCharacter()
    {
        if(isPlayerCharacter)
        {
            transform.position += Vector3.right * moveSpeed * Time.deltaTime; 
        }
        else
        {
            transform.position -= (Vector3.right * moveSpeed * Time.deltaTime);
        }
    }

    public void Attack(Character target)
    {
        target.currentHP -= attackPower - target.defense;

        target.hitEffect.SetActive(true);
        Debug.Log($"{target}이 {name}에게 {attackPower - target.defense}의 데미지를 받음");

        if(target.currentHP <= 0)
        {
            target.gameObject.SetActive(false);
            targetCharacter = null;
        }
    }

}
