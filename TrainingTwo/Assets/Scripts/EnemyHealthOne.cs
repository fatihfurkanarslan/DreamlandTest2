using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthOne : MonoBehaviour {

    public int enemyHealth;

    public ParticleSystem deathEffect;

    public int enemyKillPoint;


	// Use this for initialization
	void Start () {

        //enemyHealth = 2;
	}
	
	// Update is called once per frame
	void Update () {

        if(enemyHealth <= 0)
        {
            
            Instantiate(deathEffect, transform.position, transform.rotation);
            Destroy(gameObject);
            ScoreManager.AddToPoints(enemyKillPoint);
        }
		
	}

    public void TakeDamageEnemy(int takeDamageForEnemy)
    {
        enemyHealth -= takeDamageForEnemy;

    }
}
