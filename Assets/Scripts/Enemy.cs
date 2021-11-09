using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    Animator anim;

    bool isAttack;

    Main_Character player;

    private void Start()
    {
        anim = GetComponent<Animator>();
        currentHP = maxHP;
    }

    private void Update()
    {
        if ((Physics2D.OverlapCircle(transform.position, attackRange, LayerMask.GetMask("PlayerCharacter"))))
        {
            player = Physics2D.OverlapCircleAll(transform.position, attackRange, LayerMask.GetMask("PlayerCharacter"))[0].GetComponent<Main_Character>();
            anim.SetBool("IsWalk", false);
            if(!isAttack)
            {
                isAttack = true;
                Invoke("AttackPlayer", attackDelay);
            }
        }
        else
        {
            isAttack = false;
            anim.SetBool("IsWalk", true);
            Move();
            CancelInvoke("AttackPlayer");
        }

        
    }

    private void Move()
    {
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;
    }

    void AttackPlayer()
    {
        if (player.currentHP > 0)
        {
            anim.SetTrigger("Attack");
            player.currentHP -= attackPower;

        }
        else
        {
            // 플레이어 죽음 처리
        }
        isAttack = false;

    }
}
