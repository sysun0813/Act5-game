using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class EndStage : MonoBehaviour
{
    public event Action OnEndStage;
    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }


    public void FinishStage()
    {
        GetComponent<BoxCollider2D>().enabled = false;
        anim.SetTrigger("Teleport");
    }
    
    public void EndTeleportAnim()
    {
        OnEndStage();
    }
}
