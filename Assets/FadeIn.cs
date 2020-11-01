using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeIn : MonoBehaviour
{
    public void StartGame()
    {
        GameEvents.current.startGame();
    }
}
