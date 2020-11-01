using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Safe : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Circulo" || collision.gameObject.tag == "Quadrado" || collision.gameObject.tag == "Triangulo")
        {
            GameEvents.current.destroyCaixa(collision.gameObject.tag);
            Destroy(collision.gameObject);
        }
    }
}
