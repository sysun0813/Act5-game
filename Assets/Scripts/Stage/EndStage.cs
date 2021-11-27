using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class EndStage : MonoBehaviour
{
    public event Action OnEndStage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 마지막 지점에 도달하면 이벤트 발생
        OnEndStage();
    }
}
