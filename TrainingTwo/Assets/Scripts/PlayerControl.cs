﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{

    public float velocity;
    private float moveSpeed;
    private float moveVelocity;
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

    private LevelLoader levelLoader;

    private TouchControls touchControlsToFlip;


    void Start()
    {

        // enemyHealth = FindObjectOfType<EnemyHealthOne>();
        // knockbackCount = 0f;

        fingerCount = 0;

        startSwipe = false;

        facingRight = true;

        anim = GetComponent<Animator>();

        platform = FindObjectOfType<MovingPlatform>();
        levelLoader = FindObjectOfType<LevelLoader>();
        touchControlsToFlip = FindObjectOfType<TouchControls>();


    }

    void FixedUpdate()
    {

        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

        // distancebetweenEnemyAndPlayer = Vector2.Distance(enemyHealth[0].transform.position, transform.position);

    }

    void Update()
    {



        if (grounded)
        {
            doubleJump = false;
        }


        //#if UNITY_STANDALONE
        if (knockbackCount <= 0)
        {
            //  Move(horizontal);
            GetComponent<Rigidbody2D>().velocity = new Vector2(moveVelocity, GetComponent<Rigidbody2D>().velocity.y);
        }
        else if (knockbackCount > 0)
        {
            if (knockbackFromRight)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(knockback, knockback / 2);
                knockbackCount -= Time.deltaTime * 3;
            }
            if (!knockbackFromRight)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(-knockback, knockback / 2);
                knockbackCount -= Time.deltaTime * 3;
            }
        }

        //#endif

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
#if UNITY_STANDALONE
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        if (Input.GetButtonDown("Jump") && grounded)
        {
            jump = true;
            HandleJump();
            //GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumperVelocity);
            Jump();
        }


        if(Input.GetButtonDown("Jump") && !doubleJump && !grounded)
        {
            jump = true;
            HandleJump();
           // GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumperVelocity);
            Jump();
            doubleJump = true;
        }



        //attack için olan if
        if (Input.GetButtonDown("Fire2"))
        {
            Sword();

            //attack = true;
            //HandleAttack();


            //GetComponent<Rigidbody2D>().velocity = Vector2.zero;// calışmadı tekrar bak...
        }

        if (Input.GetButtonDown("Fire1"))
        {
            //Instantiate(throwBall, throwBallPoint.transform.position, throwBallPoint.transform.rotation);
            FireBall();
        }

#endif

        //yüzün yönünü değiştirme
        Flip(touchControlsToFlip.moveInput);

        //horizontal olarak aldığımız değişkeni animationları değiştirirken kullanıyoruz.
        anim.SetFloat("speed", Mathf.Abs(touchControlsToFlip.moveInput));

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
                            //levelLoader.LoadLevel();
                            //GetComponent<Rigidbody2D>().velocity = new Vector2(velocity * horizontal, GetComponent<Rigidbody2D>().velocity.y);
                            Debug.Log("sağaaa");
                        }
                        else
                        {

                            Debug.Log("solaaa");
                            //GetComponent<Rigidbody2D>().velocity = new Vector2(-velocity * horizontal, GetComponent<Rigidbody2D>().velocity.y);
                            //Swipe Left
                           // levelLoader.LoadLevel();
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

    }

    public void Jump()
    {
        // GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumperVelocity);

        if (grounded)
        {
            jump = true;
            HandleJump();
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumperVelocity);
            // Jump();
        }


        if (!doubleJump && !grounded)
        {
            jump = true;
            HandleJump();
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumperVelocity);
            // Jump();
            doubleJump = true;
        }
    }

    // butona bastığımda move input değişmediği için yada velocity kıpırdamıyor.
    public void Move(float moveInput)
    {
        // GetComponent<Rigidbody2D>().velocity = new Vector2(moveInput * velocity, GetComponent<Rigidbody2D>().velocity.y);
        moveVelocity = moveInput * velocity;
    }

    public void FireBall()
    {
        Instantiate(throwBall, throwBallPoint.transform.position, throwBallPoint.transform.rotation);
    }

    public void Sword()
    {
        attack = true;
        HandleAttack();
    }


    void Flip(float moveInput)
    {
        if (facingRight && moveInput < 0)
        {
            facingRight = !facingRight;

            //Object in Scale i object'i döndürmek için kullanılır.  LOCALSCALE!!!
            Vector3 theScale = transform.localScale;

            theScale.x *= -1;

            transform.localScale = theScale;
        }
        if (!facingRight && moveInput > 0)
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
