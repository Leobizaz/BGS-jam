using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Volume : MonoBehaviour
{
    AudioSource audioS;

    [Range(0, 1)]
    public static float globalvolume = 1;

    private void Start()
    {
        audioS = GetComponent<AudioSource>();
        audioS.volume = globalvolume;
    }
}
