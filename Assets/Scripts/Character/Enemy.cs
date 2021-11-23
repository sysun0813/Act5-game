using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{

    CircleCollider2D circleCollider;

    bool isAttack;


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
            if(targetCharacter == null)
            {
                try
                {
                    targetCharacter = Physics2D.OverlapCircleAll(transform.position, attackRange, LayerMask.GetMask("PlayerCharacter"))[0].GetComponent<Main_Character>();
                    PlayAttackAnim();

                }
                catch
                {
                    Debug.Log("¹¹¾ß ÀÌ »õ³¢´Â");
                }
            }

            anim.SetBool("IsMove", false);
            if(!isAttack && targetCharacter != null)
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

    void PlayAttackAnim()
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
