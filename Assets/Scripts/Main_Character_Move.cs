using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface CharacterInterface
{
    string Name { get; }

    float MaxHP { get; }

    float Defense { get; }

    float MoveSpeed { get; }

    float AttackDelay { get; }

    float AttackPower { get; }

    float AttackRange { get; }

}

public class Main_Character_Move : MonoBehaviour , CharacterInterface
{
    [Header("플레이어 스탯")]

    public string _name;
    public string Name { get { return _name; } }


    public float maxHP;
    public float MaxHP { get { return maxHP; } }


    public float attackpower;
    public float AttackPower { get { return attackpower; } }


    public float defense;
    public float Defense { get { return defense; } }

    public float attackdelay;
    public float AttackDelay { get { return attackdelay; } }

    public float movespeed;
    public float MoveSpeed { get { return movespeed; } }

    public float range;
    public float AttackRange { get { return range; } }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!(Physics2D.OverlapCircle(transform.position, range, LayerMask.GetMask("EnemyCharacter"))))
        {
            Move();
        }
       
    }

    public void Move()
    {
        transform.position += Vector3.right * movespeed * Time.deltaTime;
    }
}
