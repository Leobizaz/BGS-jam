using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public PlayerController player;
    public GameObject indicator;
    bool onArea;
    bool once;


    void Start()
    {
        indicator.SetActive(false);
    }
    void Update()
    {
        if (onArea && Input.GetKeyDown(KeyCode.E) && !once)
        {
            Execute();

        }
    }

    public void Execute()
    {
        if (!player.handsBusy)
        {
            Debug.Log("Pegou");
            indicator.SetActive(false);
            player.HoldObject(this.gameObject);
        }
        else
        {
            player.DropObject();
            indicator.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            onArea = true;
            indicator.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            indicator.SetActive(false);
            onArea = false;
        }
    }
}
