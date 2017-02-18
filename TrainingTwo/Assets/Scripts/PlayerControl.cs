using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

    public float velocity;
    public float jumperVelocity;

    private bool attack;
    private bool jump;

    //ground check ile yerle teması algılama Pyhsic2D.overlapcircle ile..
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask whatIsGround;

    //object in sağa ve sola dönme olayını transform.positionla olmadığı için tuşlara bastığında sayı atamasıyla yaptım.
    private int facingRightNumber;
    
    private bool grounded;

    private bool doubleJump;

    //facingRight değişkeniyle objeyi Flip ediyoruz.
    private bool facingRight;

    //animasyonlar için anim objecti 
    Animator anim;

    // TOUCH CONTROL DENEMELERİ

    private bool startSwipe;

    private int fingerCount;

    //private float startPositionOfX;
    //private float startPositionOfY;

    Vector2 firstPressPos;
    Vector2 secondPressPos;
    Vector2 currentSwipe;

    //fırlatma objesi
    public Transform throwBall;
    public GameObject throwBallPoint;

    //knockback için, yani enemy e değdiği zaman geri savurma için 
    public float knockback;
    public bool knockbackFromRight;
    public float knockbackCount;
    public float knockbackLenght;

   // public float distancebetweenEnemyAndPlayer;
   // private EnemyHealthOne enemyHealth;
    public int takeDamage;

    private MovingPlatform platform;
    

    void Start () {

       // enemyHealth = FindObjectOfType<EnemyHealthOne>();
       // knockbackCount = 0f;

        fingerCount = 0;

        startSwipe = false;

        facingRight = true;

        anim = GetComponent<Animator>();

        platform = FindObjectOfType<MovingPlatform>();


    }

    void FixedUpdate() {

        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
        
       // distancebetweenEnemyAndPlayer = Vector2.Distance(enemyHealth[0].transform.position, transform.position);
        
    }
	
	void Update () {

        

        if (grounded)
        {
            doubleJump = false;
        }

        float horizontal = Input.GetAxisRaw("Horizontal");

        float vertical = Input.GetAxisRaw("Vertical");

        if(knockbackCount <= 0)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(horizontal * velocity, GetComponent<Rigidbody2D>().velocity.y);
        }
        else if(knockbackCount > 0)
        {
            if (knockbackFromRight)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(knockback, knockback/2);
                knockbackCount -= Time.deltaTime*3;
            }
            if(!knockbackFromRight)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(-knockback, knockback/2);
                knockbackCount -= Time.deltaTime*3;
            }
        }


        //touch controls kısmı burda
        #region touch controls
        if (Input.touches.Length > 0)
        {
            Touch t = Input.GetTouch(0);
            if (t.phase == TouchPhase.Began)
            {
                //save began touch 2d point
                firstPressPos = new Vector2(t.position.x, t.position.y);
            }
            if (t.phase == TouchPhase.Ended)
            {
                //save ended touch 2d point
                secondPressPos = new Vector2(t.position.x, t.position.y);

                //create vector from the two points
                currentSwipe = new Vector3(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

                //normalize the 2d vector
                currentSwipe.Normalize();

                //swipe upwards
                if (!(secondPressPos == firstPressPos))
                {
                    if (Mathf.Abs(currentSwipe.x) > Mathf.Abs(currentSwipe.y))
                    {
                        if (currentSwipe.x < 0)
                        {
                            GetComponent<Rigidbody2D>().velocity = new Vector2(velocity * horizontal, GetComponent<Rigidbody2D>().velocity.y);
                            Debug.Log("sağaaa");
                        }
                        else
                        {
                            Debug.Log("solaaa");
                            GetComponent<Rigidbody2D>().velocity = new Vector2(-velocity * horizontal, GetComponent<Rigidbody2D>().velocity.y);
                            //Swipe Left
                        }
                    }
                    else
                    {
                        if (currentSwipe.y < 0)
                        {
                            Debug.Log("asağıı");
                            // Swipe Down
                        }
                        else
                        {
                            Debug.Log("yukarııı");
                            //Swipe Up
                        }
                    }
                }
            }

        }
        #endregion

        //yüzün yönünü değiştirme
        Flip(horizontal);

        //horizontal olarak aldığımız değişkeni animationları değiştirirken kullanıyoruz.
        anim.SetFloat("speed", Mathf.Abs(horizontal));

       
        
        //if (Input.GetKey(KeyCode.D))
        //{
        //    facingRightNumber = 5; // artı değer verip flip in içinde scale.x i değiştirmek için kullandık ama gerek kalmadı input.getaxis ile 
        //    GetComponent<Rigidbody2D>().velocity = new Vector2(velocity, GetComponent<Rigidbody2D>().velocity.y);
        //    Flip();

        //}

        //if (Input.GetKey(KeyCode.A))
        //{
        //    facingRightNumber = -5;
        //    GetComponent<Rigidbody2D>().velocity = new Vector2(-velocity, GetComponent<Rigidbody2D>().velocity.y);
        //    Flip();
        //}

        if (Input.GetButtonDown("Jump") && grounded)
        {
            jump = true;
            HandleJump();
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumperVelocity);
        }


        if(Input.GetButtonDown("Jump") && !doubleJump && !grounded)
        {
            jump = true;
            HandleJump();
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumperVelocity);
            doubleJump = true;
        }

        //attack için olan if
        if (Input.GetButtonDown("Fire2"))
        {
            attack = true;
            HandleAttack();
            
            
            //GetComponent<Rigidbody2D>().velocity = Vector2.zero;// calışmadı tekrar bak...
        }

        if (Input.GetButtonDown("Fire1"))
        {
            Instantiate(throwBall, throwBallPoint.transform.position, throwBallPoint.transform.rotation);
        }


    }

    void Flip(float horizontal)
    {
        if (facingRight && horizontal < 0)
        {
            facingRight = !facingRight;

            //Object in Scale i object'i döndürmek için kullanılır.  LOCALSCALE!!!
            Vector3 theScale = transform.localScale;

            theScale.x *= -1;

            transform.localScale = theScale;
        }
        if (!facingRight && horizontal > 0)
        {
            facingRight = !facingRight;
            Vector3 theScale = transform.localScale;

            theScale.x *= -1;

            transform.localScale = theScale;
        }
    }

    void HandleAttack()
    {
        if (attack)
        {
            anim.SetTrigger("attack");
            
            attack = false;
        }
    }

    void HandleJump()
    {
        if (jump)
        {
            anim.SetTrigger("jump");

            jump = false;
        }
        

    }

    // transform.parent neden ??
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "MovingPlatform")
        {
            //bunuda sormayı unutma neden transform.parent??
            transform.parent = other.transform;
            Debug.Log("ustüne çıktı");

        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "MovingPlatform")
        {
            //bunuda sormayı unutma neden transform.parent??
            transform.parent = null;
            Debug.Log("üstünden indi");
        }
    }

}
