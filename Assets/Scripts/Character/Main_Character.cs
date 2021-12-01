using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Main_Character : Character
{
    [SerializeField] BoxCollider2D boxCollider;

    bool isAttack;


    public GameObject prfHpbar;
    public GameObject canvas;

    RectTransform hpBar;
    private float height = 1f;

    Image nowHPbar;

    private void Start()
    {
        canvas = GameObject.Find("Canvas");
        hpBar = Instantiate(prfHpbar, canvas.transform).GetComponent<RectTransform>();
        anim = GetComponent<Animator>();
        currentHP = maxHP;
        nowHPbar = hpBar.transform.GetChild(0).GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 _hpbarPos = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x-0.5f, transform.position.y + height, 0));
        hpBar.position = _hpbarPos;
        nowHPbar.fillAmount = (float)currentHP / (float)maxHP;

        
        if (Physics2D.OverlapCircle(boxCollider.bounds.center, attackRange, LayerMask.GetMask("EnemyCharacter")))
        {
            if (targetCharacter == null)
            {
                try
                {
                    targetCharacter = Physics2D.OverlapCircleAll(transform.position, attackRange, LayerMask.GetMask("EnemyCharacter"))[0].GetComponent<Enemy>();
                    PlayAttackAnim();
                }
                catch
                {

                }
            }

            anim.SetBool("IsMove", false);
            if (!isAttack && targetCharacter != null)
            {
                isAttack = true;
                Invoke("PlayAttackAnim", attackDelay);
            }
        }
        //else if(Physics2D.OverlapCircle(boxCollider.bounds.center, attackRange, LayerMask.GetMask("TeleportStone")))
        //{ 
        //    anim.SetBool("IsMove", false);
        //    Physics2D.OverlapCircleAll(boxCollider.bounds.center, attackRange, LayerMask.GetMask("TeleportStone"))[0].GetComponent<EndStage>().FinishStage();
        //}
        else
        {
            isAttack = false;
            anim.SetBool("IsMove", true);

            MoveCharacter();

            CancelInvoke("PlayAttackAnim");
        }
    }

    private void PlayAttackAnim()
    {
        anim.SetTrigger("Attack");
        isAttack = false;
    }

    public void AttackTarget()
    {
        Attack(targetCharacter);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(boxCollider.bounds.center, attackRange);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Teleporter"))
        {
            collision.GetComponent<EndOfStage>().anim.SetTrigger("Teleport");
        }
    }
}
