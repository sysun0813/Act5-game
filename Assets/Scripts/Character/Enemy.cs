using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : Character
{

    [SerializeField] BoxCollider2D boxCollider;

    bool isAttack;

    public GameObject prfHpbar;
    public GameObject canvas;

    RectTransform hpBar;
    private float height = 0.8f;

    Image nowHPbar;

    //private float epower;


    private void Start()
    {
        //epower = GameObject.Find("����(Clone)").GetComponent<Main_Character>().attackPower;
        canvas = GameObject.Find("Canvas");
        hpBar = Instantiate(prfHpbar, canvas.transform).GetComponent<RectTransform>();
        anim = GetComponent<Animator>();
        SetCurrentHp();
        nowHPbar= hpBar.transform.GetChild(0).GetComponent<Image>();
    }

    private void Update()
    {
        Vector3 _hpbarPos = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x+0.5f, transform.position.y + height, 0));
        hpBar.position = _hpbarPos;
        nowHPbar.fillAmount = (float)currentHP / (float)maxHP;

        if (currentHP <= 0)
        {
            Destroybar();
        }


        if ((Physics2D.OverlapCircle(boxCollider.bounds.center, attackRange, LayerMask.GetMask("PlayerCharacter"))))
        {
            if(targetCharacter == null)
            {
                try
                {
                    targetCharacter = Physics2D.OverlapCircleAll(transform.position, attackRange, LayerMask.GetMask("PlayerCharacter"))[0].GetComponent<Main_Character>();
                    PlayAttackAnim();

                }
                catch
                {
                    Debug.Log("���� �� ������");
                }
            }

            anim.SetBool("IsMove", false);
            if(!isAttack && targetCharacter != null)
            {
                isAttack = true;
                Invoke("PlayAttackAnim", attackDelay);
            }
        }
        else
        {
            isAttack = false;
            anim.SetBool("IsMove", true);

            MoveCharacter();

            CancelInvoke("PlayAttackAnim");
        }
    }

    void PlayAttackAnim()
    {
        anim.SetBool("Attack", true);
        isAttack = false;
    }

    public void AttackTarget()
    {
        anim.SetBool("Attack", false);
        Attack(targetCharacter);
        /*if (currentHP <= epower)
        {
            Destroybar();
        }*/
    }

    public void Destroybar()
    {
        nowHPbar.gameObject.SetActive(false);
        hpBar.gameObject.SetActive(false);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(boxCollider.bounds.center, attackRange);
    }
}
