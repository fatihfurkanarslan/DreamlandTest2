using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

    public Collider2D colliderAttack;

    private float attackTimer;
    public float attackCooldown;

    private bool attacking;

    EnemyHealthOne enemyy;
    public int giveDamageToEnemy;
 


	// Use this for initialization
	void Start () {

        enemyy = FindObjectOfType<EnemyHealthOne>();
        colliderAttack.enabled = false;
        attacking = false;
	}
	
	// Update is called once per frame
	void Update () {

        if (!attacking && Input.GetButtonDown("Fire2"))
        {
            attacking = true;
            attackTimer = attackCooldown;
            colliderAttack.enabled = true;
           

        }

        if (attacking)
        {
            if(attackTimer > 0)
            {
                attackTimer -= Time.deltaTime;
            }
            else
            {
                attacking = false;
                colliderAttack.enabled = false;
            }
        }
		
        
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            enemyy.TakeDamageEnemy(giveDamageToEnemy);
         //   Debug.Log("sayı " + giveDamageToEnemy);
            Debug.Log("temas olduuu");
        }
    }


    //void OnCollisionEnter2D(Collision2D coll)
    //{
    //    if (coll.gameObject.tag == "Enemy")
    //    {
    //        Debug.Log("temas varrr........");
    //        enemy.TakeDamageEnemy(giveDamageToEnemy);
    //        //player.atAttack = false;
    //    }

    //}

}
