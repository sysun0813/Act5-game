using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [Header("�÷��̾� ĳ�������� Ȯ��")]
    public bool isPlayerCharacter;

    [Header("ĳ���� ����")]
    public string Name;

    public float maxHP;

    public float currentHP;

    public float attackPower;

    public float defense;

    public float attackDelay;

    public float moveSpeed;

    public float attackRange;

    [Header("ĳ���� �̹���")]
    public Sprite characterImage;

    public Animator anim;

    //ü�¹�
    /*public GameObject prfHpbar;
    public GameObject canvas;

    RectTransform hpBar;
    public float height = 1.7f;
    */

    [Header("Ÿ�� ĳ����")]
    [SerializeField] protected Character targetCharacter;

    [Header("�ǰ� ����Ʈ")]
    public GameObject hitEffect;

    // ������ �ʱ�ȭ �Ǿ����� Ȯ��
    [HideInInspector]
    public bool isInitialize;

    private void Awake()
    {
        /*canvas = GameObject.Find("Canvas");
        hpBar = Instantiate(prfHpbar, canvas.transform).GetComponent<RectTransform>();*/
    }

    private void Update()
    {
        /*Vector3 _hpbarPos = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y + height, 0));
        hpBar.position = _hpbarPos;*/
    }

    public void MoveCharacter()
    {
        if(isPlayerCharacter)
        {
            transform.position += Vector3.right * moveSpeed * Time.deltaTime; 
        }
        else
        {
            transform.position -= (Vector3.right * moveSpeed * Time.deltaTime);
        }
    }

    public void Attack(Character target)
    {
        target.anim.SetTrigger("Hit");

        target.currentHP -= attackPower - target.defense;

        target.hitEffect.SetActive(true);

        if(target.currentHP <= 0)
        {
            Destroy(target.gameObject);
            //GameObject.Find("SwordMan(Clone)").GetComponent<Enemy>().Destroybar();
            targetCharacter = null;
        }
    }

    public void SetCurrentHp()
    {
        currentHP = maxHP;
    }
}
