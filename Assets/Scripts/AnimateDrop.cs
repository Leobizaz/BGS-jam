using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AnimateDrop : MonoBehaviour
{
    Animator anim;

    public GameObject[] skins;




    private void Start()
    {
        foreach(GameObject skin in skins)
        {
            skin.SetActive(false);
        }
        int r = Random.Range(0, skins.Length);

        skins[r].SetActive(true);
        skins[r].transform.rotation = Quaternion.Euler(0, 0, Random.Range(-75, 75));




        anim = GetComponent<Animator>();
        anim.speed = 0.01f;
        Invoke("NormalSpeed", Random.Range(0.1f, 0.8f));
    }

    void NormalSpeed()
    {
        anim.speed = 1;
    }




}
