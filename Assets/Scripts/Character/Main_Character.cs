using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Main_Character : Character
{
    CircleCollider2D circleCollider;

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
        //if(!(Physics2D.OverlapCircle(circleCollider.bounds.center, attackRange, LayerMask.GetMask("EnemyCharacter"))))
        //{
        //    isAttack = false;
        //    MoveCharacter();
        //    anim.SetBool("IsMove", true);
        //    CancelInvoke("AttackEnemy");
        //}
        //else
        //{
        //    targetCharacter = Physics2D.OverlapCircleAll(transform.position, attackRange, LayerMask.GetMask("EnemyCharacter"))[0].GetComponent<Enemy>();
        //    if (!isAttack)
        //    {
        //        isAttack = true;
        //        Invoke("AttackEnemy", attackDelay);
        //    }
        //    anim.SetBool("IsMove", false);

        //}
        if ((Physics2D.OverlapCircle(circleCollider.bounds.center, attackRange, LayerMask.GetMask("EnemyCharacter"))))
        {
            if (targetCharacter == null)
            {
                try
                {
                    targetCharacter = Physics2D.OverlapCircleAll(transform.position, attackRange, LayerMask.GetMask("EnemyCharacter"))[0].GetComponent<Enemy>();
                    PlayAttackAnim();

                }
                catch
                {

                }
            }

            anim.SetBool("IsMove", false);
            if (!isAttack && targetCharacter != null)
            {
                isAttack = true;
                Invoke("PlayAttackAnim", attackDelay);
            }
        }
        else
        {
            isAttack = false;
            anim.SetBool("IsMove", true);

            MoveCharacter();

            CancelInvoke("PlayAttackAnim");
        }
    }

    private void PlayAttackAnim()
    {
        anim.SetTrigger("Attack");
        isAttack = false;
    }
    public void AttackTarget()
    {
        targetCharacter.anim.SetTrigger("Hit");
        Attack(targetCharacter);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(circleCollider.bounds.center, attackRange);
    }


    
}
