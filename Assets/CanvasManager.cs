using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    public Text objetivoDisplay;
    public Text timerDisplay;

    private void Update()
    {
        timerDisplay.text = Time.time.ToString();
    }

    public void ChangeObjetivo(string text)
    {
        objetivoDisplay.text = text;
    }


}
