using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class CanvasManager : MonoBehaviour
{
    public static bool levelDone;

    public GameObject fadeOut;

    TimeSpan timeSpan;

    private void Awake()
    {
        levelDone = false;
    }

    public Text objetivoDisplay;
    public Text timerDisplay;
    public Text finalTimeDisplay;

    public GameObject star1;
    public GameObject star2;
    public GameObject star3;

    public GameObject failScreen;

    public float startTime;

    private void Start()
    {
        startTime = Time.timeSinceLevelLoad;
    }

    private void Update()
    {
        if (!levelDone)
        {
            timeSpan = TimeSpan.FromSeconds(Time.timeSinceLevelLoad - startTime);
            timerDisplay.text = string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds);

            if(timeSpan.Minutes >= 1)
            {
                levelDone = true;
                failScreen.SetActive(true);
            }

        }
        else
        {
            if(timeSpan.Seconds <= 26)
            {
                star1.SetActive(true);
                star2.SetActive(true);
                star3.SetActive(true);
            }
            else if(timeSpan.Seconds <= 34)
            {
                star1.SetActive(true);
                star2.SetActive(true);
                star3.SetActive(false);
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

    public void ChangeObjetivo(string text)
    {
        objetivoDisplay.text = text;
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
