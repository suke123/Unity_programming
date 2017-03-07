using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleScript : MonoBehaviour {
    public Text HighScore;

	// Use this for initialization
	void Start () {
        int high = ScoreScript.getHighScore();
        HighScore.text = "HIGH SCORE: " + high;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
