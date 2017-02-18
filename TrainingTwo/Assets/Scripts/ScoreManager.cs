using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    public static int score;

    //text objesini update te text.text ile screne de ki objeye bağlıyoruz.
    Text text;
	// Use this for initialization
	void Start () {


        text = GetComponent<Text>();

        score = PlayerPrefs.GetInt("CurrentPlayerGold");
	}
	
	// Update is called once per frame
	void Update () {
		if(score < 0)
        {
            score = 0;
        }

        text.text = "" + score;
	}

    public static void AddToPoints(int addToPoints)
    {
        score += addToPoints;
        PlayerPrefs.SetInt("CurrentPlayerGold", score);
    }

    public static void ResetScore()
    {
        score = 0;
    }
}
