using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Referee : MonoBehaviour
{
    public Transform cam;

    private Transform refe;

    private bool tf = false;

    public Animator anim;


    GameManager gameManager;


    private void Start()
    {
        tf = false;
        anim = GetComponent<Animator>();
        refe = GetComponent<Transform>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        refe.position = new Vector2(cam.position.x + 4f, transform.position.y);
    }

    private void Update()
    {
        if (gameManager.playerCharacters.Count > 0)
        {
            tf = true;

            if(gameManager.playerCharacters[0].transform.position.x + 4.0f < 22.0f)
            {
                refe.position = new Vector2(gameManager.playerCharacters[0].transform.position.x + 4.0f, transform.position.y);
            }

            
        }
        else if (tf = true && (false || true /*true 대신 아군 깃발 베었는지 참거짓 코드 추가*/ ))
        {
            //깃발 오른쪽으로 들기
        }
        else if (tf = true && (gameManager.playerCharacters.Count == 0 || true /*true 대신 적군 깃발 베었는지 참거짓 코드 추가*/))
        {
            //Invoke("PlayWwinAnim",0f);
        }

        else
        {
            refe.position = new Vector2(cam.position.x + 4f, -2.66f);
        }

    }


    private void PlayWwinAnim()
    {
        //anim.SetBool("wwin", true);
        
    }


    private void PlayRwinAnim()
    {
        //anim.SetBool("rwin", true);
    }

}