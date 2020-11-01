using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorOnEnable : MonoBehaviour
{
    Animator anim;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        anim.Play("cursor_onEnable");
    }
}
