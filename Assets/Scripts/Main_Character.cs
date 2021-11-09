using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface CharacterInterface
{
    string Name { get; }

    float MaxHP { get; }
    float NowHP { get; }
    float Defense { get; }

    float MoveSpeed { get; }

    float AttackDelay { get; }

    float AttackPower { get; }

    float AttackRange { get; }

}

public class Main_Character : MonoBehaviour , CharacterInterface
{
    [Header("플레이어 스탯")]

    public string _name;
    public string Name { get { return _name; } }


    public float maxHP;
    public float MaxHP { get { return maxHP; } }

    public float nowHP;
    public float NowHP { get { return nowHP; } }


    public float attackPower;
    public float AttackPower { get { return attackPower; } }


    public float defense;
    public float Defense { get { return defense; } }

    public float attackDelay;
    public float AttackDelay { get { return attackDelay; } }

    public float moveSpeed;
    public float MoveSpeed { get { return moveSpeed; } }

    public float range;
    public float AttackRange { get { return range; } }

    Collider2D[] hitColliders;
    bool isAttack;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!(Physics2D.OverlapCircle(transform.position, range, LayerMask.GetMask("EnemyCharacter"))))
        {
            isAttack = false;
            Move();
        }
        else
        {
            hitColliders = Physics2D.OverlapCircleAll(transform.position, range, LayerMask.GetMask("EnemyCharacter"));
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
        hitColliders[0].GetComponent<Enemy>().nowHP -= attackPower;
        isAttack = false;
    }

}
