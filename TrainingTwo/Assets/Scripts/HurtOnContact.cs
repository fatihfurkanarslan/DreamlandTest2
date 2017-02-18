using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtOnContact : MonoBehaviour {

    PlayerHealthManager playerHealth;

    public int giveDamage;

   // public AudioSource takeDamageAuido;

    private PlayerControl player;

	// Use this for initialization
	void Start () {

        playerHealth = FindObjectOfType<PlayerHealthManager>();
     //   takeDamageAuido = GetComponent<AudioSource>();
        player = FindObjectOfType<PlayerControl>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter2D(Collision2D other)
    {
        if ((other.gameObject.tag == "Player"))
        {
            playerHealth.TakeDamage(giveDamage);
            
        //    takeDamageAuido.Play();
            player.knockbackCount = 1;
            Debug.Log("değdi..");
            
        }

        // player.knockbackCount = player.knockbackLenght;
        if (player.transform.position.x > transform.position.x)
        {
            player.knockbackFromRight = true;
           // Debug.Log("sağdan çarptı.");
        }
        else
        {
            player.knockbackFromRight = false;
          //  Debug.Log("soldan çarptı.");
        }


    }
}
