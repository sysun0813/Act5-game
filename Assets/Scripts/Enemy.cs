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

public class Enemy : MonoBehaviour
{
    [Header("�� �̵��ӵ�")]
    public float speed;

    public float radius;

    private void Update()
    {
        if(Physics2D.OverlapCircle(transform.position, radius, LayerMask.GetMask("PlayerCharacter")))
        {
            Debug.Log("�÷��̾� ����");


        }

        else
        {
            Move();

        }
    }

    private void Move()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;
    }
}
