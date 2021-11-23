using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class EndStage : MonoBehaviour
{
    public event Action OnEndStage;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        //Debug.Log("¿Í");
        OnEndStage();
    }
}
