using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Main_Character : Character
{
    bool isAttack;

    private void Start()
    {
        anim = GetComponent<Animator>();
        InitCurrentHp();
    }

    public void PlayerCharacterUpdate()
    {
        // 적 캐릭터가 공격 사거리 안에 들어왔을 때
        if (Physics2D.OverlapCircle(boxCollider.bounds.center, attackRange, LayerMask.GetMask("EnemyCharacter")))
        {
            // 이동 애니메이션 정지
            anim.SetBool("IsMove", false);

            if (targetCharacter == null)
            {
                try
                {
                    // 공격 타겟 설정
                    targetCharacter = Physics2D.OverlapCircleAll(transform.position, attackRange, LayerMask.GetMask("EnemyCharacter"))[0].GetComponent<Enemy>();
                    // 적이 사거리에 들어오자마자 공격하도록 애니메이션 함수 호출
                    PlayAttackAnim();
                }
                catch
                {

                }
            }
            
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
        anim.SetBool("Attack", true);
        isAttack = false;
    }

    public void AttackTarget()
    {
        anim.SetBool("Attack", false);
        Attack(targetCharacter);
    }

    public void FireProjectile()
    {
        anim.SetBool("Attack", false);
        Projectile projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        projectile.ActiveProjectile(this, attackPower, projectilePrefab.GetComponent<SpriteRenderer>().sprite);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(boxCollider.bounds.center, attackRange);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Teleporter"))
        {
            collision.GetComponent<EndOfStage>().anim.SetTrigger("Teleport");
            GameManager.Instance.StopStage();
        }
    }
}
