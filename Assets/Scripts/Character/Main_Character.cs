using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Main_Character : Character
{
    CircleCollider2D circleCollider;

    Animator anim;

    Enemy enemyplayer;
    bool isAttack;


    private void Start()
    {
        circleCollider = GetComponent<CircleCollider2D>();
        anim = GetComponent<Animator>();
        currentHP = maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        if(!(Physics2D.OverlapCircle(circleCollider.bounds.center, attackRange, LayerMask.GetMask("EnemyCharacter"))))
        {
            isAttack = false;
            MoveCharacter(true);
            anim.SetBool("IsMove", true);
            CancelInvoke("Attack");
        }
        else
        {
            enemyplayer = Physics2D.OverlapCircleAll(transform.position, attackRange, LayerMask.GetMask("EnemyCharacter"))[0].GetComponent<Enemy>();
            if (!isAttack)
            {
                isAttack = true;
                Invoke("Attack", attackDelay);
            }
            anim.SetBool("IsMove", false);

        }

    }

    private void Attack()
    {
        anim.SetTrigger("Attack");
        if (enemyplayer.currentHP > 0)
        {
            enemyplayer.currentHP -= attackPower;

        }
        else
        {
            enemyplayer.gameObject.SetActive(false);
        }

        isAttack = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(circleCollider.bounds.center, attackRange);
    }
}
