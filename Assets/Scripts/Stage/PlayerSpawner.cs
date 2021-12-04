using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    SpriteSorter sorter;

    private void Awake()
    {
        sorter = FindObjectOfType<SpriteSorter>();

    }

    public Main_Character SpawnPlayer(Main_Character SpawnedPlayer)
    {
        float randNum = Random.Range(0f, 1f);

        Main_Character player = Instantiate(SpawnedPlayer, transform.position + (Vector3.up * randNum), Quaternion.identity);
        player.GetComponent<SpriteRenderer>().sortingOrder += (int)Mathf.Lerp(3, 0, randNum);
        player.hitEffect.GetComponent<SpriteRenderer>().sortingOrder = player.GetComponent<SpriteRenderer>().sortingOrder;

        return player;
    }

    public void RePositionPlayer(Main_Character player)
    {
        float randNum = Random.Range(0f, 1f);

        player.transform.position = transform.position + (Vector3.up * randNum);

        player.GetComponent<SpriteRenderer>().sortingOrder = sorter.GetSortingOrder(player.gameObject);

    }
}
