using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Passos : MonoBehaviour
{
    public AudioClip[] sons;
    AudioSource audioS;

    private void Start()
    {
        audioS = GetComponent<AudioSource>();
    }

    public void Passo()
    {
        audioS.PlayOneShot(sons[Random.Range(0, sons.Length)]);
    }

}
