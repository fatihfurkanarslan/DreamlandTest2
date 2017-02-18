using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayer : MonoBehaviour {

    LevelManager levelManager;

	// Use this for initialization
	void Start () {

        levelManager = FindObjectOfType<LevelManager>();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //ontriggerenter2d calışmadı yerine collision kullandım.
    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("temas var..");
           levelManager.RespawnPlayer();
        }
    }
}
