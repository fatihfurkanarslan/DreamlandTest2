using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    private bool isFallowing;

    private PlayerControl player;

    public int xOffset;
    public int yOffset;

	// Use this for initialization
	void Start () {

        player = FindObjectOfType<PlayerControl>();
		
	}
	
	// Update is called once per frame
	void Update () {

        transform.position = new Vector3(player.transform.position.x + xOffset , player.transform.position.y + yOffset, transform.position.z);
		
	}
}
