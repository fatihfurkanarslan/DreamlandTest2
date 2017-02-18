using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthManager : MonoBehaviour {

    public int playerHealth;
    public int fullPlayerHealth;

    LevelManager levelManager;
    PlayerLifeSystem playerLife;

    Text text;

    //nedenini anlamadım ama bunu yapmazsam ufak bir bug oluyor oyunda bir sn civarı respawn ederken takılıyor
    public bool isDead;

	// Use this for initialization
	void Start () {

        text = GetComponent<Text>();

        playerHealth = fullPlayerHealth;

        levelManager = FindObjectOfType<LevelManager>();
        playerLife = FindObjectOfType<PlayerLifeSystem>();

        isDead = false;
		
	}
	
	// Update is called once per frame
	void Update () {
        if(playerHealth <= 0 && !isDead)
        {
            levelManager.RespawnPlayer();
            playerLife.LifeDecrease();
        }

        if(playerHealth <= 0)
        {
            isDead = true;
        }

        text.text = "" + playerHealth;
		

	}

    public void TakeDamage(int takeDamage)
    {
        playerHealth -= takeDamage;
    }

    public void DoFullHealth()
    {
        playerHealth = fullPlayerHealth;
    }
}
