using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [Header("�÷��̾� ĳ�������� Ȯ��")]
    public bool isPlayerCharacter;

    //[Header("���Ÿ� ĳ�������� Ȯ��")]
    //public bool isLongRangedCharacter;

    [Header("ĳ���� ����")]
    public string Name;

    public int maxHP;

    public int currentHP;

    public float attackPower;

    public float defense;

    public float attackDelay;

    public float moveSpeed;

    public float attackRange;

    [Header("ĳ���� �̹���")]
    public Sprite characterImage;

    public Animator anim;

    [HideInInspector]
    public HpBar hpBar;

    public BoxCollider2D boxCollider;

    public SpriteRenderer spriteRenderer;

    [Header("Ÿ�� ĳ����")]
    public Character targetCharacter;

    [Header("�ǰ� ����Ʈ")]
    public GameObject hitEffect;

    [Header("�߻�ü ������")]
    public Projectile projectilePrefab;

    // ������ �ʱ�ȭ �Ǿ����� Ȯ��
    [HideInInspector]
    public bool isInitialize;

    public void MoveCharacter()
    {
        if(currentHP > 0)
        {
            if (isPlayerCharacter)
            {
                transform.position += Vector3.right * moveSpeed * Time.deltaTime;
            }
            else
            {
                transform.position -= (Vector3.right * moveSpeed * Time.deltaTime);
            }
        }
    }

    public void Attack(Character target)
    {
        target.currentHP -= (int)(attackPower - target.defense);

        target.hitEffect.SetActive(true);

        target.hpBar.SetHp(target.currentHP, target.maxHP);

        if(target.currentHP <= 0)
        {
            target.anim.SetTrigger("Die");
            StartCoroutine(target.Die());
            target.hpBar.DisableHpBar();
            targetCharacter = null;
        }
        else
        {
            target.anim.SetTrigger("Hit");
        }
    }

    public void InitCurrentHp()
    {
        currentHP = maxHP;
    }

    public void StartDie()
    {
        StartCoroutine(Die());
    }

    public IEnumerator Die()
    {
        boxCollider.enabled = false;
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
        boxCollider.enabled = true;
        yield return null;
    }
}
