using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        FinalScore.tempoFase1 = "00:00:000";
        FinalScore.estrelasFase1 = 0;
        FinalScore.tempoFase2 = "00:00:000";
        FinalScore.estrelasFase2 = 0;
        FinalScore.tempoFase3 = "00:00:000";
        FinalScore.estrelasFase3 = 0;
    }

    public void Quitgame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
