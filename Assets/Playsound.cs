using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playsound : MonoBehaviour
{
    public bool random;
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
        if (random)
        {
            audioS.pitch = Random.Range(0.7f, 1.4f);
            audioS.Play();
        }
        else
        {
            audioS.Play();
        }
    }
}
