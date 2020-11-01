using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SegundoMinigameManager : MonoBehaviour
{
    public int trecosColetados;
    public static bool level2Done;

    public GameObject winScreen;
    public GameObject loseScreen;

    public int wrongs;

    private void Awake()
    {
        
    }

    private void Start()
    {
        GameEvents.current.onCollectTreco += TrecoColetado;
        GameEvents.current.onCollectWrong += WrongTreco;
    }

    private void Update()
    {
        if (!level2Done)
        {
            if(wrongs >= 3)
            {
                level2Done = true;
                loseScreen.SetActive(true);
            }

            if(trecosColetados >= 20)
            {
                level2Done = true;
                winScreen.SetActive(true);
            }
        }
    }

    public void WrongTreco(int index)
    {
        wrongs++;
    }

    public void InstantiateTrecos()
    {

    }

    public void TrecoColetado(string tipo)
    {
        trecosColetados++;
    }


}
