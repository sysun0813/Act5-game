using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    SpriteSorter sorter;


    private void Awake()
    {
        sorter = FindObjectOfType<SpriteSorter>();

    }
    public void SpawnEnemy(Enemy SpawnedEnemy)
    {
        float randNum = Random.Range(0f, 1f);

        Enemy enemy = Instantiate(SpawnedEnemy, transform.position + (Vector3.up * randNum), Quaternion.identity);
        enemy.GetComponent<SpriteRenderer>().sortingOrder = sorter.GetSortingOrder(enemy.gameObject);

        enemy.hitEffect.GetComponent<SpriteRenderer>().sortingOrder = enemy.GetComponent<SpriteRenderer>().sortingOrder;

    }

    
}