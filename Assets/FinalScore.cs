using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class FinalScore : MonoBehaviour
{
    public static string tempoFase1;
    public static string tempoFase2;
    public static string tempoFase3;

    public static int estrelasFase1;
    public static int estrelasFase2;
    public static int estrelasFase3;

    public GameObject[] estrelas;

    public Text tempo1;
    public Text tempo2;
    public Text tempo3;

    private void Start()
    {
        tempo1.text = tempoFase1;
        tempo2.text = tempoFase2;
        tempo3.text = tempoFase3;

        if(estrelasFase1 == 1)
        {
            estrelas[0].SetActive(true);
        }
        if(estrelasFase1 == 2)
        {
            estrelas[0].SetActive(true);
            estrelas[1].SetActive(true);
        }
        if(estrelasFase1 == 3)
        {
            estrelas[0].SetActive(true);
            estrelas[1].SetActive(true);
            estrelas[2].SetActive(true);
        }
        if (estrelasFase2 == 1)
        {
            estrelas[3].SetActive(true);
        }
        if (estrelasFase2 == 2)
        {
            estrelas[3].SetActive(true);
            estrelas[4].SetActive(true);
        }
        if (estrelasFase2 == 3)
        {
            estrelas[3].SetActive(true);
            estrelas[4].SetActive(true);
            estrelas[5].SetActive(true);
        }
        if (estrelasFase3 == 1)
        {
            estrelas[6].SetActive(true);
        }
        if (estrelasFase3 == 2)
        {
            estrelas[6].SetActive(true);
            estrelas[7].SetActive(true);
        }
        if (estrelasFase3 == 3)
        {
            estrelas[6].SetActive(true);
            estrelas[7].SetActive(true);
            estrelas[8].SetActive(true);
        }



    }

    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }

}
