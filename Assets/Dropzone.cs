using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dropzone : MonoBehaviour
{
    public GameObject popupWin;
    int collected = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Coletavel")
        {
            Destroy(collision.gameObject);
            collected++;
        }
    }

    private void Update()
    {
        if(collected >= 9)
        {
            Debug.Log("Win");
            popupWin.SetActive(true);
        }
    }

}
