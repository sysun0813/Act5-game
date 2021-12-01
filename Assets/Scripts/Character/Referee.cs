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
        refe.position = new Vector2(cam.position.x + 4f, transform.position.y);
    }

    private void Update()
    {
        //if (stageManager.currentPlayers.Count > 0)
        //{
        //    tf = true;
        //    refe.position = new Vector2(stageManager.currentPlayers[0].transform.position.x + 4.0f, transform.position.y);
        //}
        //else if (tf = true && (stageManager.players.Count == 0 || true /*true 대신 아군 깃발 베었는지 참거짓 코드 추가*/ ))
        //{
        //    //깃발 오른쪽으로 들기
        //}
        //else if (tf = true && (stageManager.current.Count == 0 || true /*true 대신 적군 깃발 베었는지 참거짓 코드 추가*/))
        //{
        //    //깃발 왼쪽으로 들기
        //}

        //else
        //{
        //    refe.position = new Vector2(cam.position.x + 4f, -2.66f);
        //}

    }
}