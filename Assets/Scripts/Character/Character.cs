using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [Header("플레이어 캐릭터인지 확인")]
    public bool isPlayerCharacter;

    //[Header("원거리 캐릭터인지 확인")]
    //public bool isLongRangedCharacter;

    [Header("캐릭터 스탯")]
    public string Name;

    public int maxHP;

    public int currentHP;

    public float attackPower;

    public float defense;

    public float attackDelay;

    public float moveSpeed;

    public float attackRange;

    [Header("캐릭터 이미지")]
    public Sprite characterImage;

    public Animator anim;

    [HideInInspector]
    public HpBar hpBar;

    public BoxCollider2D boxCollider;

    public SpriteRenderer spriteRenderer;

    [Header("타겟 캐릭터")]
    public Character targetCharacter;

    [Header("피격 이팩트")]
    public GameObject hitEffect;

    [Header("발사체 프리팹")]
    public Projectile projectilePrefab;

    // 정보가 초기화 되었는지 확인
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
