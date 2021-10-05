using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_Character_Move : MonoBehaviour
{
    public float speed=1;
    public float radius;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!(Physics2D.OverlapCircle(transform.position, radius, LayerMask.GetMask("EnemyCharacter"))))
        {
            Move();
        }
       
    }

    public void Move()
    {
        transform.position += Vector3.right * speed * Time.deltaTime;
    }
}
