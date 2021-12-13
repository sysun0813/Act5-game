using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    SpriteSorter sorter;
    [SerializeField] List<Enemy> enemyObjects;

    private void Awake()
    {
        sorter = FindObjectOfType<SpriteSorter>();
        enemyObjects = new List<Enemy>();
    }
    public void SpawnEnemy(Enemy SpawnedEnemy)
    {
        float randNum = Random.Range(0f, 1f);
        
        try
        {
            Enemy enemy = enemyObjects.Find(x => x.gameObject.activeSelf != true);
            enemy.Name = SpawnedEnemy.Name;
            enemy.maxHP = SpawnedEnemy.maxHP;
            enemy.attackPower = SpawnedEnemy.attackPower;
            enemy.defense = SpawnedEnemy.defense;
            enemy.attackDelay = SpawnedEnemy.attackDelay;
            enemy.moveSpeed = SpawnedEnemy.moveSpeed;
            enemy.attackRange = SpawnedEnemy.attackRange;

            enemy.InitCurrentHp();

            enemy.GetComponent<SpriteRenderer>().sprite = SpawnedEnemy.characterImage;
            enemy.characterImage = SpawnedEnemy.characterImage;
            enemy.GetComponent<Animator>().runtimeAnimatorController = SpawnedEnemy.anim.runtimeAnimatorController;
            
            enemy.GetComponent<SpriteRenderer>().sortingOrder = sorter.GetSortingOrder(enemy.gameObject);

            enemy.hitEffect.GetComponent<SpriteRenderer>().sortingOrder = enemy.GetComponent<SpriteRenderer>().sortingOrder;
            enemy.transform.position = transform.position + (Vector3.up * randNum);
            enemy.gameObject.SetActive(true);

            FindObjectOfType<MainUI>().MatchHpBar(enemy);
        }
        catch
        {
            Enemy enemy = Instantiate(SpawnedEnemy, transform.position + (Vector3.up * randNum), Quaternion.identity);
            enemy.GetComponent<SpriteRenderer>().sortingOrder = sorter.GetSortingOrder(enemy.gameObject);

            enemy.hitEffect.GetComponent<SpriteRenderer>().sortingOrder = enemy.GetComponent<SpriteRenderer>().sortingOrder;
            FindObjectOfType<MainUI>().MatchHpBar(enemy);
            enemyObjects.Add(enemy);
        }
    }

    
}