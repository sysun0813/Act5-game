using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpBar : MonoBehaviour
{
    public Transform target;

    [SerializeField] RectTransform rectTransform;

    void LateUpdate()
    {
        Vector3 hpBarPos = target.GetComponent<BoxCollider2D>().bounds.center + (Vector3.up * 1.5f);
        transform.position = Camera.main.WorldToScreenPoint(hpBarPos);
    }

    public void InitHp()
    {
        rectTransform.localScale = Vector3.one;

    }

    public void SetHp(int currentHp, int MaxHp)
    {
        float ratio = (float)currentHp / MaxHp;
        rectTransform.localScale = new Vector3(ratio, 1, 1);
    }

    public void DisableHpBar()
    {
        target = null;
        rectTransform.localScale = Vector3.one;
        FindObjectOfType<MainUI>().AddHpBar(this);
        gameObject.SetActive(false);
    }
}
