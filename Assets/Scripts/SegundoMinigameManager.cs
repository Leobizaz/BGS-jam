using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SegundoMinigameManager : MonoBehaviour
{
    public int trecosColetados;
    public static bool level2Done;

    public GameObject winScreen;
    public GameObject loseScreen;

    public int wrongs;
    public Canvas canvas;

    public GameObject metais;
    public GameObject minerios;
    public GameObject organicos;

    public GameObject clamp1;
    public GameObject clamp2;

    public GameObject star1;
    public GameObject star2;
    public GameObject star3;

    public GameObject wrong1;
    public GameObject wrong2;
    public GameObject wrong3;

    public GameObject bacia1;
    public GameObject bacia2;
    public GameObject bacia3;

    public GameObject fadeOutScreen;

    public Text timerDisplay;
    public Text finalTimeDisplay;
    TimeSpan timeSpan;
    float startTime;

    private void Awake()
    {
        
    }

    private void Start()
    {
        startTime = Time.timeSinceLevelLoad;
        GameEvents.current.onCollectTreco += TrecoColetado;
        GameEvents.current.onCollectWrong += WrongTreco;

        wrong1.SetActive(false);
        wrong2.SetActive(false);
        wrong3.SetActive(false);
        InstantiateTrecos();

    }

    private void Update()
    {
        if (!level2Done)
        {
            timeSpan = TimeSpan.FromSeconds(Time.timeSinceLevelLoad - startTime);
            timerDisplay.text = timeSpan.Minutes.ToString("00") + ":" + timeSpan.Seconds.ToString("00") + ":" + timeSpan.Milliseconds.ToString("00");

            if (wrongs >= 3)
            {
                level2Done = true;
                loseScreen.SetActive(true);
                loseScreen.transform.SetAsLastSibling();
                return;
            }

            if(trecosColetados >= 20)
            {
                level2Done = true;
                winScreen.SetActive(true);
                winScreen.transform.SetAsLastSibling();
            }
        }
        else
        {
            star1.SetActive(false);
            star2.SetActive(false);
            star3.SetActive(false);


            if (timeSpan.Seconds <= 16)
            {
                star1.SetActive(true);
                star2.SetActive(true);
                star3.SetActive(true);

                if(wrongs > 0)
                {
                    star3.SetActive(false);
                }

            }
            else if (timeSpan.Seconds <= 24)
            {
                star1.SetActive(true);
                star2.SetActive(true);
                star3.SetActive(false);

                if(wrongs > 0)
                {
                    star2.SetActive(false);
                }
            }
            else
            {
                star1.SetActive(true);
                star2.SetActive(false);
                star3.SetActive(false);
            }
            finalTimeDisplay.text = string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds);
        }
    }

    public void WrongTreco(int index)
    {
        wrongs++;

        switch (index)
        {
            case 1:
                bacia1.GetComponent<Animator>().Play("baciashake");
                break;
            case 2:
                bacia2.GetComponent<Animator>().Play("baciashake");
                break;
            case 3:
                bacia3.GetComponent<Animator>().Play("baciashake");
                break;

        }

        if(wrongs == 1)
        {
            wrong1.SetActive(true);
        }

        if(wrongs == 2)
        {
            wrong2.SetActive(true);
        }

        if (wrongs == 3)
        {
            wrong3.SetActive(true);
        }

    }

    public void BotaoVoltarMenu()
    {
        fadeOutScreen.SetActive(true);
        fadeOutScreen.transform.SetAsLastSibling();
        Invoke("VoltarMenu", 2);
    }

    public void BotaoNext()
    {
        fadeOutScreen.SetActive(true);
        fadeOutScreen.transform.SetAsLastSibling();
        Invoke("LoadNext", 2);
    }

    public void VoltarMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    void LoadNext()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }


    public void InstantiateTrecos()
    {
        for (int i = 0; i < 20; i++)
        {
            Vector3 position = new Vector3(UnityEngine.Random.Range(clamp1.transform.position.x, clamp2.transform.position.x), UnityEngine.Random.Range(clamp2.transform.position.y, clamp1.transform.position.y), 0);
            int r = UnityEngine.Random.Range(0, 3);
            switch (r)
            {
                case 0:
                    Instantiate(metais, position, Quaternion.identity, canvas.transform);
                    break;

                case 1:
                    Instantiate(organicos, position, Quaternion.identity, canvas.transform);
                    break;

                case 2:
                    Instantiate(minerios, position, Quaternion.identity, canvas.transform);
                    break;

                default:
                    Instantiate(metais, position, Quaternion.identity, canvas.transform);
                    break;
            }

        }
    }

    public void TrecoColetado(string tipo)
    {
        trecosColetados++;
    }


}
