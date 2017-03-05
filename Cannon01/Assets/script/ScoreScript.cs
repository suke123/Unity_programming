using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour {

    public GameObject enemyNumText;
    public GameObject parent;

    //private List<GameObject> textList = new List<GameObject>();
    //テキストと（エネミーの種類、倒した数）をペアで格納
    //private Dictionary<GameObject, List<int>> textList = new Dictionary<GameObject, List<int>>();
    private List<GameObject> textList = new List<GameObject>();
    private List<int> enemyTypeList = new List<int>(); 
    private List<int> existEnemyList = new List<int>();
    private List<int> clearEnemyList = new List<int>();

    private int clearEnemy;
    private int existEnemy;

    // Use this for initialization
    void Start () {

        for(int i = 0; i < StageSelectManager.Instance.enemyNum.Count;i++)
        {
            if (StageSelectManager.Instance.enemyNum[i] != 0)
            {
                clearEnemy = existEnemy = StageSelectManager.Instance.enemyNum[i];
                //GameObject text = Instantiate(enemyNumText, new Vector3(-105, -25 - i * 30, 0), transform.rotation);
                GameObject text = Instantiate(enemyNumText);
                text.transform.SetParent(parent.transform, false);
                Vector3 tmp = text.transform.position;
                text.transform.position = new Vector3(0,i * -30,0) + tmp;
                text.GetComponent<Text>().text = "Enemy" + (i+1) + ": " + existEnemy.ToString() + " / " + clearEnemy.ToString();
                textList.Add(text);
                enemyTypeList.Add(i);
                existEnemyList.Add(existEnemy);
                clearEnemyList.Add(clearEnemy);
                //Debug.Log(i);
                //Debug.Log(existEnemy);
            }

        }
        //clearEnemy = existEnemy = StageSelectManager.Instance.enemy1;

        //GetComponent<Text>().text = "Enemy: " + existEnemy.ToString() + " / " + clearEnemy.ToString();
	}

    void Update()
    {

    }

    public void AddScore(int enemyType, int point)
    {
        for(int i = 0;i < enemyTypeList.Count;i++)
        {
            if(enemyTypeList[i] == enemyType)
            {
                if(existEnemyList[i] <= 0)
                {
                    existEnemyList[i] = 0;
                }
                else
                {
                    existEnemyList[i] -= point;
                }
                textList[i].GetComponent<Text>().text = "Enemy: " + existEnemyList[i].ToString() + " / " + clearEnemyList[i].ToString();
            }
        }
    }

    public List<int> NumberOfEnemy()
    {
        /*List<int> existEnemyList = new List<int>();
        foreach(KeyValuePair<GameObject, List<int>> i in textList)
        {
            existEnemyList.Add(i.Value[0]);
            Debug.Log(i.Value[0]);
        }*/
        //Debug.Log(existEnemyList.Count);
        return existEnemyList;
    }
}
