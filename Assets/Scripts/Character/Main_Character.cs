using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Main_Character : Character
{
    Enemy hitCollider;
    bool isAttack;

    private void Start()
    {
        currentHP = maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        if(!(Physics2D.OverlapCircle(transform.position, attackRange, LayerMask.GetMask("EnemyCharacter"))))
        {
            isAttack = false;
            Move();
        }
        else
        {
            hitCollider = Physics2D.OverlapCircleAll(transform.position, attackRange, LayerMask.GetMask("EnemyCharacter"))[0].GetComponent<Enemy>();
            if (!isAttack)
            {
                isAttack = true;
                Invoke("Attack", attackDelay);
            }
        }
       
    }

    public void Move()
    {
        transform.position += Vector3.right * moveSpeed * Time.deltaTime;
    }
    private void Attack()
    {
        if (hitCollider.currentHP > 0)
        {
            hitCollider.currentHP -= attackPower;

        }
        else
        {
            // 플레이어 죽음 처리
        }

        isAttack = false;
    }

}
