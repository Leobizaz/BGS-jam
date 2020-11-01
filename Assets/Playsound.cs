using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playsound : MonoBehaviour
{
    public bool onEnable;
    AudioSource audioS;
    private void OnEnable()
    {
        if (onEnable)
        {
            audioS = GetComponent<AudioSource>();
            audioS.Play();
        }
    }

    public void PlaySound()
    {
        audioS = GetComponent<AudioSource>();
        audioS.Play();
    }
}
