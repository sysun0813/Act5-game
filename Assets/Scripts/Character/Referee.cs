using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Referee : MonoBehaviour
{
    public Transform cam;

    private Transform refe;

    private bool tf = false;

    StageManager stageManager;

    private void Start()
    {
        refe = GetComponent<Transform>();
        stageManager = GameObject.Find("StageManager").GetComponent<StageManager>();
        refe.position = new Vector2(cam.position.x + 4f, -2.66f);
    }

    private void Update()
    {
        if (stageManager.currentPlayers.Count > 0)
        {
            tf = true;
            refe.position = new Vector2(stageManager.currentPlayers[0].transform.position.x + 4.0f, -2.66f);
        }
        else if (tf = true && (stageManager.players.Count == 0 || true /*true ��� �Ʊ� ��� �������� ������ �ڵ� �߰�*/ ))
        {
            //��� ���������� ���
        }
        else if (tf = true && (stageManager.enemies.Count == 0 || true /*true ��� ���� ��� �������� ������ �ڵ� �߰�*/))
        {
            //��� �������� ���
        }

        else
        {
            refe.position = new Vector2(cam.position.x + 4f, -2.66f);
        }

    }
}