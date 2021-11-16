using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public void SpawnPlayer(GameObject SpawnedPlayer)
    {
        float randNum = Random.Range(0f, 1f);

        SpriteRenderer playerSpriteRenderer = Instantiate(SpawnedPlayer, transform.position + (Vector3.up * randNum), Quaternion.identity).GetComponent<SpriteRenderer>();
        playerSpriteRenderer.sortingOrder += (int)Mathf.Lerp(3, 0, randNum);
    }
}
