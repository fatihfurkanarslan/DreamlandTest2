using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchControls : MonoBehaviour {

    private PlayerControl player;
    public float moveInput;

	// Use this for initialization
	void Start () {
        player = FindObjectOfType<PlayerControl>();
	}

  
	
    public void LeftArrow()
    {
        player.Move(-1);
        moveInput = -1;
    }

    public void RightArrow()
    {
        player.Move(1);
        moveInput = 1;
    }

    public void UnpressedArrow()
    {
        player.Move(0);
        moveInput = 0;
    }

    public void Sword()
    {
        player.Sword();
    }


    public void FireBallMethod()
    {
        player.FireBall();
    }

    public void Jump()
    {
        player.Jump();
    }

}
