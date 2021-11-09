using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface EnemyInterface
{
    string Name { get; }

    int MaxHP { get; }

    int Attack { get; }

    int Defense { get; }


}

public class Enemy : MonoBehaviour, EnemyInterface
{
    public string _name;
    public string Name { get { return _name; } }


    public int maxHP;
    public int MaxHP { get { return maxHP; } }


    public int attack;
    public int Attack { get { return attack; } }


    public int defense;
    public int Defense { get { return defense; } }

    public float attackDelay;

    [Header("적 이동속도")]
    public float speed;

    public float radius;

    bool isAttack;

    Collider2D[] players;

    private void Update()
    {
        if ((Physics2D.OverlapCircle(transform.position, radius, LayerMask.GetMask("PlayerCharacter"))))
        {
            players = (Physics2D.OverlapCircleAll(transform.position, radius, LayerMask.GetMask("PlayerCharacter")));
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
        transform.position += Vector3.left * speed * Time.deltaTime;
    }

    void AttackPlayer()
    {
        
        players[0].GetComponent<Main_Character>().nowHP -= attack;
        isAttack = false;

    }
}
