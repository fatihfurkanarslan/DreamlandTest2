using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour {

    private PauseMenu pausing;
    public GameObject pauseMenuCanvas;
    // Use this for initialization
    void Start () {

        pausing = FindObjectOfType<PauseMenu>();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Puasing()
    {
        pausing.isPaused = true;
        Debug.Log("gsfjlkskfs");
        
    }
}
