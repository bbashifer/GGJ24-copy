using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Darkener : MonoBehaviour
{
    private Animator anim;
    private static readonly int FadeOut = Animator.StringToHash("FadeOut");

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void StartFadeOut()
    {
        anim.SetTrigger(FadeOut);
    }
}
