using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart2 : MonoBehaviour
{
    public GameObject Manager;

    private void Start()
    {
        GameEvents.current.onGameStart += Startgame;
    }

    void Startgame()
    {
        Manager.SetActive(true);
    }

}
