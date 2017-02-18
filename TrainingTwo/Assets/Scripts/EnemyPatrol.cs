using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour {

    public int moveVelocity;

    public bool moveRight;

    //bu ikili enemy nin duvara ya da boşluğa gelip gelmedğini kontrol eder..
    public bool hitWall;
    public bool notAtGround;

    //ustteki ikili için overlapcircle da gerekli olan parametreler

    public Transform wallCheck;
    public float wallCheckRadius;
    public LayerMask whatIsGround;

    
    public Transform spaceCheck;
   
    
    // Use this for initialization
    void Start () {

        notAtGround = false;
        hitWall = false;
		
	}
	
	// Update is called once per frame
	void Update () {

        hitWall = Physics2D.OverlapCircle(wallCheck.position, wallCheckRadius, whatIsGround);

        notAtGround = Physics2D.OverlapCircle(spaceCheck.position, wallCheckRadius, whatIsGround);

        if (hitWall || !notAtGround)
        {
            moveRight = !moveRight;
            Vector3 theScale = transform.localScale;

            theScale.x *= -1;

            transform.localScale = theScale;
        }
            

        if (moveRight)
        {
           GetComponent<Rigidbody2D>().velocity = new Vector2(moveVelocity, GetComponent<Rigidbody2D>().velocity.y);
            
        }
        else
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(-moveVelocity, GetComponent<Rigidbody2D>().velocity.y);
            
        }

    }


    void Flip()
    {
        if (moveRight)
        {
            moveRight = !moveRight;

            //Object in Scale i object'i döndürmek için kullanılır.  LOCALSCALE!!!
            Vector3 theScale = transform.localScale;

            theScale.x *= -1;

            transform.localScale = theScale;
        }
        if (!moveRight)
        {
            moveRight = !moveRight;
            Vector3 theScale = transform.localScale;

            theScale.x *= -1;

            transform.localScale = theScale;
        }
    }
}
