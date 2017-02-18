using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour {

    public int addToPoint;

   // effect kullanarak ses ekleyebilirz. diğer türlü getcomponentla oluşturduğumz audiosouce, gameobject destroy olduğu için başlamadan siliniyor..
	// Use this for initialization
	void Start () {

        

	}
	
	// Update is called once per frame
	void Update () {
		
	}

   public void OnTriggerEnter2D(Collider2D other)
    {   
        if (other.tag == "Player")
        {
            ScoreManager.AddToPoints(addToPoint);
          //Debug.Log("temaaaaaaaaaas");
            Destroy(gameObject);
        }       
    }
}
