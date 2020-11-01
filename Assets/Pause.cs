using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public static bool gamePaused;
    public GameObject pauseScreen;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            gamePaused = !gamePaused;

            if (gamePaused)
            {
                Time.timeScale = 0;
                AudioListener.pause = true;
                pauseScreen.SetActive(true);
                pauseScreen.transform.SetAsLastSibling();
            }
            else
            {
                AudioListener.pause = false;
                Time.timeScale = 1;
                pauseScreen.SetActive(false);
            }

        }
    }

}
