using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour {

    public static int score;
    public static int highScore;

    public static bool isUpdatedScore;

	// Use this for initialization
	void Start () {
        score = 0;

        //初期スコア(0点)を表示
        GetComponent<Text>().text = score.ToString();
        //キーを使って値を取得
        //キーがない場合は第2引数をの値を取得
        //highScore = PlayerPrefs.GetInt("highScoreKey", 0);

        isUpdatedScore = false;
	}

    //ballScriptからsendMessageで呼び出されるスコア加算用メソッド
    public void AddPoint(int point)
    {
        score += point;
        GetComponent<Text>().text = score.ToString();
    }
	
	// Update is called once per frame
	void Update () {
        //ハイスコア更新
        if (highScore < score)
        {
            highScore = score;
            isUpdatedScore = true;
        }
	}

    public void Save()
    {
        //メソッドが呼ばれた時のキーと値をセットする。
        PlayerPrefs.SetInt("highScoreKey", highScore);
        //キーと値を保存
        PlayerPrefs.Save();
    }

    public static int getScore()
    {
        return score;
    }
    public static int getHighScore()
    {
        return highScore;
    }

}
