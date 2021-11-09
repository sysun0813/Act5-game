using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    
    bool isAttack;

    Main_Character player;

    private void Start()
    {
        currentHP = maxHP;
    }

    private void Update()
    {
        if ((Physics2D.OverlapCircle(transform.position, attackRange, LayerMask.GetMask("PlayerCharacter"))))
        {
            player = Physics2D.OverlapCircleAll(transform.position, attackRange, LayerMask.GetMask("PlayerCharacter"))[0].GetComponent<Main_Character>();
            if(!isAttack)
            {
                isAttack = true;
                Invoke("AttackPlayer", attackDelay);
            }
        }
        else
        {
            isAttack = false;
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
            player.currentHP -= attackPower;

        }
        else
        {
            // 플레이어 죽음 처리
        }
        isAttack = false;

    }
}
