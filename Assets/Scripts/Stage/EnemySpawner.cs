using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public void SpawnEnemy(Enemy SpawnedEnemy)
    {
        float randNum = Random.Range(0f, 1f);

        Enemy enemy = Instantiate(SpawnedEnemy, transform.position + (Vector3.up * randNum), Quaternion.identity);
        enemy.GetComponent<SpriteRenderer>().sortingOrder += (int)Mathf.Lerp(3, 0, randNum);

        enemy.hitEffect.GetComponent<SpriteRenderer>().sortingOrder = enemy.GetComponent<SpriteRenderer>().sortingOrder;

    }

    
}