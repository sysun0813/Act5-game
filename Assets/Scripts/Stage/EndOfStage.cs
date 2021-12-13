using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EndOfStage : MonoBehaviour
{
    public event Action OnFinishStage;
    public Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void FinishStage()
    {
        GetComponent<BoxCollider2D>().enabled = false;
    }

    public void EndTeleportAnim()
    {
        OnFinishStage();
    }

}
