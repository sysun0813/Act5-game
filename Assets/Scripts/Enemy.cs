using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
