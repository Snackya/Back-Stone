using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {
    public GameObject pauseMenu;
	
    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            HandlePause();
        }
    }

	public void QuitGame()
    {
        Application.Quit();
    }

    public void HandlePause()
    {
        if (!pauseMenu.activeSelf)
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1;
        }
    }
}
