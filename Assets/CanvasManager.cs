using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class CanvasManager : MonoBehaviour
{
    public static bool levelDone;

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

    private void Update()
    {
        if (!levelDone)
        {
            timeSpan = TimeSpan.FromSeconds(Time.time);
            timerDisplay.text = string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds);
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

    public void GoToNext()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("Menu");
    }


}
