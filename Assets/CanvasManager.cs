using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CanvasManager : MonoBehaviour
{
    public Text objetivoDisplay;
    public Text timerDisplay;

    private void Update()
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(Time.time);
        timerDisplay.text = string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds);
    }

    public void ChangeObjetivo(string text)
    {
        objetivoDisplay.text = text;
    }


}
