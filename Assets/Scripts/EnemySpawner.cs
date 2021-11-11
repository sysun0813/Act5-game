using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public void SpawnEnemy(GameObject SpawnedEnemy)
    {
        float randNum = Random.Range(0f, 1f);

        SpriteRenderer enemySpriteRenderer = Instantiate(SpawnedEnemy, transform.position + (Vector3.up * randNum), Quaternion.identity).GetComponent<SpriteRenderer>();
        enemySpriteRenderer.sortingOrder += (int)Mathf.Lerp(3, 0, randNum);
    }
}