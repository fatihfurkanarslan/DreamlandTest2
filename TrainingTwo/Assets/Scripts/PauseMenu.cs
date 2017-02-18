using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {


    public bool isPaused;

    public GameObject pauseMenuCanvas;

    public string selectLevel;
    public string mainMenu;



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (isPaused)
        {
            pauseMenuCanvas.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            pauseMenuCanvas.SetActive(false);
            Time.timeScale = 1f;
        }
	}


    public void Resume()
    {
        isPaused = false;
    }

    public void SelectLevel()
    {
        Application.LoadLevel(selectLevel);
        isPaused = false;
    }

    public void QuitGame()
    {
        Application.LoadLevel(mainMenu);
    }
}
