using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Volume : MonoBehaviour
{
    AudioSource audioS;

    public static float globalvolume;
    public bool menu;

    private void Start()
    {
        audioS = GetComponent<AudioSource>();
        audioS.volume = globalvolume;
    }

    private void Update()
    {
        if (menu)
        {
            audioS.volume = globalvolume;
        }
    }
}
