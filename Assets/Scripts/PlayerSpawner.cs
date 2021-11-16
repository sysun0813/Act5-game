using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject SpawnPlayer(GameObject SpawnedPlayer)
    {
        float randNum = Random.Range(0f, 1f);

        SpriteRenderer playerSpriteRenderer = Instantiate(SpawnedPlayer, transform.position + (Vector3.up * randNum), Quaternion.identity).transform.GetChild(0).GetComponent<SpriteRenderer>();
        playerSpriteRenderer.sortingOrder += (int)Mathf.Lerp(3, 0, randNum);


        return playerSpriteRenderer.gameObject;
    }
}
