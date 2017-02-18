using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartPickup : MonoBehaviour {

    PlayerLifeSystem playerLife;

	// Use this for initialization
	void Start () {

        playerLife = FindObjectOfType<PlayerLifeSystem>();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            playerLife.LifeIncrease();
            Destroy(gameObject);
            //Debug.Log("kalp topladı..");
        }
    }
}
