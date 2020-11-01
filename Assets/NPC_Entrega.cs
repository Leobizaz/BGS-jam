using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Entrega : MonoBehaviour
{
    public string tag;

    public bool verde;
    public bool rosa;
    public bool vermelho;

    bool once;
    public GameObject activate;
    public GameObject deactivate;
    public MinigameTRES_Manager manager;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == tag)
        {
            if (!once)
            {
                once = true;
                Debug.Log("Dale");
                Destroy(collision.gameObject);
                deactivate.SetActive(false);
                activate.SetActive(true);

                if (verde) manager.verdeEntregue = true;
                if (rosa) manager.rosaEntregue = true;
                if (vermelho) manager.vermelhoEntregue = true;

            }
        }
        else
        {
            GameEvents.current.destroyCaixa(collision.gameObject.tag);
            Destroy(collision.gameObject);
        }
    }
}
