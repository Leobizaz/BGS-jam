using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaixaSpawner : MonoBehaviour
{
    public string tag;
    public GameObject caixa;
    float cooldown;
    private void Start()
    {
        GameEvents.current.onDestroyCaixa += TrySpawn;
        Spawn();
    }

    private void Update()
    {
        if (cooldown > 0) cooldown -= Time.deltaTime;
    }

    void TrySpawn(string tagevent)
    {
        if(tagevent == tag)
        {
            if (cooldown <= 0)
            {
                Spawn();
                cooldown = 0.5f;
            }
        }
        else
        {
            //nothing
        }
    }

    void Spawn()
    {
        Instantiate(caixa, transform.position, Quaternion.identity);
    }

}
