using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    public GameObject currentCheckPoint;
    
    //Partice effects
    public ParticleSystem deathParticle;
    public ParticleSystem respawnParticle;
    private float delayTime;

    private PlayerControl player;
    private PlayerHealthManager playerHealthManager;

	// Use this for initialization
	void Start () {

        playerHealthManager = FindObjectOfType<PlayerHealthManager>();

        delayTime = 2f;
        player = FindObjectOfType<PlayerControl>();

       // player = FindObjectOfType<PlayerControl>();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


   public void RespawnPlayer()
    {
        StartCoroutine("RespawnPlayerCo");
    }

    public IEnumerator RespawnPlayerCo()
    {
        Instantiate(deathParticle, player.transform.position, player.transform.rotation);
        player.enabled = false;
        player.GetComponent<Renderer>().enabled = false;
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        Debug.Log("respawn player..");
        player.transform.position = currentCheckPoint.transform.position;

        //IEnumeratorla çalışıyor 
        yield return new WaitForSeconds(delayTime);

        playerHealthManager.DoFullHealth();
        playerHealthManager.isDead = false;
        player.enabled = true;
        player.GetComponent<Renderer>().enabled = true;
        player.knockbackCount = -0.1f;
        Instantiate(respawnParticle, player.transform.position, player.transform.rotation);
    }
}
