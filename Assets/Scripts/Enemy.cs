using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface EnemyInterface
{
    string Name { get; }

    float MaxHP { get; }
   
    float NowHP { get; }

    float Attack { get; }

    float Defense { get; }


}

public class Enemy : MonoBehaviour, EnemyInterface
{
    public string _name;
    public string Name { get { return _name; } }


    public float maxHP;
    public float MaxHP { get { return maxHP; } }

    public float nowHP;
    public float NowHP { get { return nowHP; } }


    public float attack;
    public float Attack { get { return attack; } }


    public float defense;
    public float Defense { get { return defense; } }

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
