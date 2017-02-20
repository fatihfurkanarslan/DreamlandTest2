using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthManager : MonoBehaviour {

    public float playerHealth;
    public float fullPlayerHealth;

    LevelManager levelManager;
    PlayerLifeSystem playerLife;

    private float healthMines = 0.2f;

    // Text text;
    Slider slider;

    //nedenini anlamadım ama bunu yapmazsam ufak bir bug oluyor oyunda bir sn civarı respawn ederken takılıyor
    public bool isDead;

	// Use this for initialization
	void Start () {

        // text = GetComponent<Text>();
        slider = GetComponent<Slider>();

        playerHealth = fullPlayerHealth;

        levelManager = FindObjectOfType<LevelManager>();
        playerLife = FindObjectOfType<PlayerLifeSystem>();

        isDead = false;
		
	}
	
	// Update is called once per frame
	void Update () {

        playerHealth -= Time.deltaTime * healthMines;

        if(playerHealth <= 0 && !isDead)
        {
            levelManager.RespawnPlayer();
            playerLife.LifeDecrease();
        }

        if(playerHealth <= 0)
        {
            isDead = true;
        }

        if(playerHealth >= fullPlayerHealth)
        {
            playerHealth = fullPlayerHealth;
        }

        //  text.text = "" + playerHealth;
        slider.value = playerHealth;
		

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
