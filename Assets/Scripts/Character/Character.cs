using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [Header("ĳ���� ����")]
    public string Name;

    public float maxHP;

    public float currentHP;

    public float attackPower;

    public float defense;

    public float attackDelay;

    public float moveSpeed;

    public float attackRange;

    [Header("�÷��̾� ĳ�������� Ȯ��")]
    public bool isPlayerCharacter;

    [Header("Ÿ�� ĳ����")]
    protected Character targetCharacter;

    [Header("�ǰ� ����Ʈ")]
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
        Debug.Log($"{target}�� {name}���� {attackPower - target.defense}�� �������� ����");

        if(target.currentHP <= 0)
        {
            target.gameObject.SetActive(false);
            targetCharacter = null;
        }
    }

}
