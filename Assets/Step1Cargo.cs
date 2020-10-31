using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Step1Cargo : MonoBehaviour
{
    public GameObject CageCenter;
    public bool picked;
    public bool picked2;
    public Collider2D col;
    public bool beingPicked;
    float xVelocity;
    float yVelocity;
    Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Cursor" && !picked)
        {
            picked = true;
            PickUP();
        }

        if (collision.gameObject.tag == "CageCenter" && !picked2)
        {
            //beingPicked = false;
            picked2 = true;
            //col.enabled = true;
            gameObject.layer = 14;
            beingPicked = true;

            col.enabled = false;
            ShipController.currentCapacity++;
            GameEvents.current.collectCargo(rb.mass);
        }
    }

    private void Update()
    {
        if (picked2)
        {
            if (Vector2.Distance(CageCenter.transform.position, this.transform.position) > 8)
            {
                picked2 = false;
                picked = false;
                ShipController.currentCapacity--;
                gameObject.layer = 0;
                GameEvents.current.loseCargo(rb.mass);
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
        beingPicked = true;
    }

}