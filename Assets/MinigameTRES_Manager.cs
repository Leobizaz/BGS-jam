using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class MinigameTRES_Manager : MonoBehaviour
{
    public bool verdeEntregue;
    public bool vermelhoEntregue;
    public bool rosaEntregue;

    public static bool level3Done;

    public GameObject winScreen;

    public GameObject verdeCheck;
    public GameObject vermelhoCheck;
    public GameObject rosaCheck;

    public GameObject star1;
    public GameObject star2;
    public GameObject star3;

    public Text timerDisplay;
    public Text finalTimeDisplay;
    public GameObject fadeOut;
    AudioSource audioS;
    bool once;
    

    float startTime;
    TimeSpan timeSpan;

    private void Start()
    {
        audioS = GetComponent<AudioSource>();
        level3Done = false;
        startTime = Time.timeSinceLevelLoad;
    }

    private void Update()
    {
        if (!level3Done)
        {

            timeSpan = TimeSpan.FromSeconds(Time.timeSinceLevelLoad - startTime);
            timerDisplay.text = timeSpan.Minutes.ToString("00") + ":" + timeSpan.Seconds.ToString("00") + ":" + timeSpan.Milliseconds.ToString("00");

            if (verdeEntregue) verdeCheck.SetActive(true);
            if (vermelhoEntregue) vermelhoCheck.SetActive(true);
            if (rosaEntregue) rosaCheck.SetActive(true);


            if (verdeEntregue && vermelhoEntregue && rosaEntregue)
            {
                level3Done = true;
                winScreen.SetActive(true);
            }
        }
        else
        {
            audioS.DOFade(0, 3);


            if (timeSpan.Minutes < 1)
            {
                star1.SetActive(true);
                star2.SetActive(true);
                star3.SetActive(true);
                FinalScore.estrelasFase3 = 3;
            }
            else if (timeSpan.Seconds <= 120)
            {
                star1.SetActive(true);
                star2.SetActive(true);
                star3.SetActive(false);
                FinalScore.estrelasFase3 = 2;
            }
            else
            {
                star1.SetActive(true);
                star2.SetActive(false);
                star3.SetActive(false);
                FinalScore.estrelasFase3 = 1;
            }
            finalTimeDisplay.text = string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds);
            FinalScore.tempoFase3 = finalTimeDisplay.text;

        }
    }

    public void ButtonNext()
    {
        fadeOut.SetActive(true);
        fadeOut.transform.SetAsLastSibling();
        Invoke("GoToNext", 2);
    }

    public void ButtonMenu()
    {
        fadeOut.SetActive(true);
        fadeOut.transform.SetAsLastSibling();
        Invoke("GoToMenu", 2);
    }

    public void GoToNext()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("Menu");
    }


}
