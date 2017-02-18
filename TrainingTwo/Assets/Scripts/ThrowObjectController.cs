using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowObjectController : MonoBehaviour {

    public PlayerControl player;

    public int speed;

    public ParticleSystem enemyDeathEffect;

    public ParticleSystem throwBallEffect;

    public int points;

    //angularVelocity methodunu kullanarak rotation(objenin dönmesini) sağlarız.
    public float rotationSpeed;

    //EnemyHealthOne ile kurulan bağlantı ve giveDamageForEnemy ile enemy health one da enemyHealth 0 olduğunda enemy destroy edilir.
    private EnemyHealthOne enemyHealth;
    public int giveDamageForEnemy;

    // Use this for initialization
    void Start () {

        player = FindObjectOfType<PlayerControl>();

        enemyHealth = FindObjectOfType<EnemyHealthOne>();

        //her instantiate methodu calıştırıldığında script bir kez daha calıştığı için startta olması gerekiyor bu kısmın. 
        //yoksa update de olduğu zaman düzgün çalışmaz ve çalışmadı.
        if(player.transform.localScale.x > 0)
        {
            speed = -speed;
            rotationSpeed = -rotationSpeed;
        }
		
	}
	
	// Update is called once per frame
	void Update () {

        GetComponent<Rigidbody2D>().velocity = new Vector2(-speed, GetComponent<Rigidbody2D>().velocity.y);

        GetComponent<Rigidbody2D>().angularVelocity = rotationSpeed;
        
	}
    //ontriggerenter neden çalışmıyor anlamadım. ??
    //void OnTriggerEnter2D(Collider2D other)
    //{
    //    //if(other.tag == "Enemy")
    //    //{
    //    //    Destroy(other.gameObject);
    //    //}
    //    Debug.Log("carptı.");
    //    Destroy(gameObject);
    //}

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            //Instantiate(enemyDeathEffect, transform.position, transform.rotation);
            //Destroy(other.gameObject);
            //   ScoreManager.AddToPoints(points);
            enemyHealth.TakeDamageEnemy(giveDamageForEnemy);
            //Debug.Log("fjaklf");
        }
        Instantiate(throwBallEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
