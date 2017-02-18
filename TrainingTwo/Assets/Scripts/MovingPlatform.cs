using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {

    public GameObject movingPlatform;

    public Transform[] points;

    private Transform currentPoint;

    public int speedofPlatform;

    public int pointSelection;


    private PlayerControl player;


	// Use this for initialization
	void Start () {

        currentPoint = points[pointSelection];

        player = FindObjectOfType<PlayerControl>();
		
	}
	
	// Update is called once per frame
	void Update () {

        movingPlatform.transform.position = Vector3.MoveTowards(movingPlatform.transform.position, currentPoint.position, Time.deltaTime * speedofPlatform);
		
        if(movingPlatform.transform.position == currentPoint.position)
        {
            pointSelection++;

            if(pointSelection == points.Length)
            {
                pointSelection = 0;
            }

            currentPoint = points[pointSelection];
        }
	}

   


}
