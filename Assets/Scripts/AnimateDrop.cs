using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AnimateDrop : MonoBehaviour
{
    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        anim.speed = 0.01f;
        Invoke("NormalSpeed", Random.Range(0.1f, 0.8f));
    }

    void NormalSpeed()
    {
        anim.speed = 1;
    }




}
