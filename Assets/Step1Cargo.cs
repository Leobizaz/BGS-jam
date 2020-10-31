using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Step1Cargo : MonoBehaviour
{
    public GameObject CageCenter;
    bool picked;
    public Collider2D col;
    bool beingPicked;
    float xVelocity;
    float yVelocity;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Cursor" && !picked)
        {
            picked = true;
            PickUP();
        }

        if (collision.gameObject.tag == "CageCenter")
        {
            beingPicked = false;
            col.enabled = true;
            gameObject.layer = 14;
        }
    }

    private void Update()
    {
        if (picked)
        {
            if (Vector2.Distance(CageCenter.transform.position, this.transform.position) > 5)
            {
                picked = false;
                ShipController.currentCapacity--;
                gameObject.layer = 0;
            }
        }
    }

    private void FixedUpdate()
    {
        if (beingPicked)
        {
            transform.position = new Vector2(
                Mathf.SmoothDamp(transform.position.x, CageCenter.transform.position.x, ref xVelocity, 0.3f), 
                Mathf.SmoothDamp(transform.position.y, CageCenter.transform.position.y, ref yVelocity, 0.3f));
        }
    }

    void PickUP()
    {
        ShipController.currentCapacity++;
        beingPicked = true;
        col.enabled = false;
    }

}
