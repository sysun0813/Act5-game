using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    Animator anim;

    CircleCollider2D circleCollider;


    bool isAttack;

    Main_Character player;

    private void Start()
    {
        anim = GetComponent<Animator>();
        circleCollider = GetComponent<CircleCollider2D>();
        currentHP = maxHP;
    }

    private void Update()
    {
        if ((Physics2D.OverlapCircle(circleCollider.bounds.center, attackRange, LayerMask.GetMask("PlayerCharacter"))))
        {
            if(player == null)
            {
                player = Physics2D.OverlapCircleAll(transform.position, attackRange, LayerMask.GetMask("PlayerCharacter"))[0].GetComponent<Main_Character>();
                AttackPlayer();
            }
            anim.SetBool("IsMove", false);
            if(!isAttack && player != null)
            {
                isAttack = true;
                Invoke("AttackPlayer", attackDelay);
            }
        }
        else
        {
            isAttack = false;
            anim.SetBool("IsMove", true);

            MoveCharacter(false);

            CancelInvoke("AttackPlayer");
        }
    }

    void AttackPlayer()
    {
        anim.SetTrigger("Attack");
        player.currentHP -= attackPower;

        if (player.currentHP <= 0)
        {
            // 플레이어 죽음 처리
            player.gameObject.SetActive(false);
            player = null;
        }
        isAttack = false;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(circleCollider.bounds.center, attackRange);
    }
}
