using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageSelect : MonoBehaviour {

    //public int enemy1;
    //public int enemy2;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void LoadScene()
    {
        StageSelectManager.Instance.stage = GetComponent<Text>().text;
        /*StageSelectManager.Instance.enemyNum.Clear();
        StageSelectManager.Instance.enemyNum.Add(enemy1);
        StageSelectManager.Instance.enemyNum.Add(enemy2);*/
        SceneManager.LoadScene("Cannon_test01");
    }
}
