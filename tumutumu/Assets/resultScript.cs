using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class resultScript : MonoBehaviour {
    public Text Score;
    public Text HighScore;
    public GameObject CongText;

    public bool isUpdatedHighScore;

	// Use this for initialization
	void Start () {
        CongText.SetActive(false);

        isUpdatedHighScore = false;
        int score = ScoreScript.getScore();
        int highScore = ScoreScript.getHighScore();

        isUpdatedHighScore = ScoreScript.isUpdatedScore;

        if (isUpdatedHighScore)
        {
            UpdateHighScore();
        }
        Score.text = "SCORE: "+score.ToString();
        HighScore.text = "HIGH SCORE: " + highScore.ToString();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void UpdateHighScore()
    {
        CongText.SetActive(true);
    }
}
