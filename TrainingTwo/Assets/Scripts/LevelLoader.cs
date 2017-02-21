using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour {

    public bool inZone;
    public string levelToLoad;
    public MainMenu mainMenu;

	// Use this for initialization
	void Start () {

        inZone = false;
        mainMenu = FindObjectOfType<MainMenu>();
		
	}
	
	// Update is called once per frame
	void Update () {

        if (inZone && Input.GetButtonDown("Vertical"))
        {
            Application.LoadLevel(levelToLoad);

            Debug.Log("fkşsl" + inZone);
            
            
        }
		
	}

    public void LoadLevel()
    {
        Application.LoadLevel(levelToLoad);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            mainMenu.Save();
            inZone = true;
            Debug.Log("inzone oldu : " + inZone);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            inZone = false;
            Debug.Log("inzonedan cıktı : " + inZone);
        }
    }
}
