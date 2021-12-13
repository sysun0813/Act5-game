using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : Character
{
    bool isAttack;

    private void Start()
    {
        anim = GetComponent<Animator>();
        InitCurrentHp();
    
    }

    private void Update()
    {
        if ((Physics2D.OverlapCircle(boxCollider.bounds.center, attackRange, LayerMask.GetMask("PlayerCharacter"))))
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
        anim.SetBool("Attack", true);
        isAttack = false;
    }

    public void AttackTarget()
    {
        anim.SetBool("Attack", false);
        Attack(targetCharacter);
        
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(boxCollider.bounds.center, attackRange);
    }
}
