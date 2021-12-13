using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float attack;

    public SpriteRenderer spriteRenderer;

    Character owner;

    bool isPlayer;

    public void ActiveProjectile(Character owner, float attack, Sprite sprite)
    {
        this.attack = attack;
        this.owner = owner;
        this.isPlayer = owner.isPlayerCharacter;
        GetComponent<SpriteRenderer>().sprite = sprite;
        gameObject.SetActive(true);
    }

    private void LateUpdate()
    {
        if(isPlayer)
        {
            transform.position += Vector3.right * Time.deltaTime * 20f;
        }
        else
        {
            transform.position += Vector3.left* Time.deltaTime * 20f;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isPlayer)
        {
            if(collision.gameObject.layer == LayerMask.NameToLayer("EnemyCharacter"))
            {
                Enemy enemy = collision.GetComponent<Enemy>();
                enemy.currentHP -= (int)attack;

                enemy.hitEffect.SetActive(true);

                enemy.hpBar.SetHp(enemy.currentHP, enemy.maxHP);

                if (enemy.currentHP <= 0)
                {
                    enemy.anim.SetTrigger("Die");
                    StartCoroutine(enemy.Die());
                    enemy.hpBar.DisableHpBar();
                    owner.targetCharacter = null;
                }
                else
                {
                    enemy.anim.SetTrigger("Hit");
                }
                Destroy(gameObject);
            }
        }
        else
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("PlayerCharacter"))
            {
                Main_Character player = collision.GetComponent<Main_Character>();
                player.currentHP -= (int)attack;

                player.hitEffect.SetActive(true);

                player.hpBar.SetHp(player.currentHP, player.maxHP);

                if (player.currentHP <= 0)
                {
                    player.anim.SetTrigger("Die");
                    StartCoroutine(player.Die());
                    player.hpBar.DisableHpBar();
                    owner.targetCharacter = null;
                }
                else
                {
                    player.anim.SetTrigger("Hit");
                }
                Destroy(gameObject);
            }
        }
    }


}
