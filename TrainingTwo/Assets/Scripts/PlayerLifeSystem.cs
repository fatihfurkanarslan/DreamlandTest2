using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLifeSystem : MonoBehaviour {

    public int playerLife;
   // public int startingLife;

    public float waitForReborn;

    private PlayerControl player;

    public GameObject gameoverText;

    Text text;


	// Use this for initialization
	void Start () {

        text = GetComponent<Text>();
        player = FindObjectOfType<PlayerControl>();
    
        playerLife = PlayerPrefs.GetInt("CurrentPlayerLife");

    }
	
	// Update is called once per frame
	void Update () {

       if(!player.gameObject.activeSelf)
        {
            waitForReborn -= Time.deltaTime;
        }


       //kritik bug yeniden başlaması gereken yerde respawn yaptı. setactive false olduğu için player object görünmedi ama effect çalıştı.
       if(playerLife <= 0)
        {
            //Application.LoadLevel(Application.loadedLevel);
            gameoverText.SetActive(true);
            player.gameObject.SetActive(false);
        }

        if (gameoverText.activeSelf)
        {
            waitForReborn -= Time.deltaTime;
            Debug.Log("süre  " + waitForReborn);
        }

        if(waitForReborn < 0)
        {
            Application.LoadLevel(Application.loadedLevel);
            Debug.Log("tekrar yükleme  ");

        }


        text.text = "" + playerLife;


	}

    public void LifeIncrease()
    {
        playerLife++;
        PlayerPrefs.SetInt("CurrentPlayerLife", playerLife);
    }

    public void LifeDecrease()
    {
        playerLife--;
        PlayerPrefs.SetInt("CurrentPlayerLife", playerLife);

    }
}
