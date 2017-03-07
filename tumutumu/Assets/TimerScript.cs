using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimerScript : MonoBehaviour
{

    public Image UIobj;
    public Text Timer;
    public bool roop;
    public float c = 60;
    int count;
    public float countTime = 5.0f;
    public ballScript isPlaying;
    public ballScript Balls;

    public GameObject GameFinishPanel;


    //ballScript bs =GetComponent<ballScript>();

    // Use this for initialization
    void Start()
    {
        GameFinishPanel.SetActive(false);
        Timer.text = count.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        //yield return new WaitForSeconds(2.0f);
        if (isPlaying.isFalledBall)
        {
            UIobj.fillAmount -= 1.0f / countTime * Time.deltaTime;
            c -= Time.deltaTime;
            if (c <= 0)
            {
                c = 0;
                //GameFinishPanel.SetActive(true);
                //Balls.enabled = false;
                //Wait(3.0f);
                FinishGame();
            }
        }

        count = (int)c;
        Timer.text =  count.ToString();
    }

    public void FinishGame()
    {
        GameFinishPanel.SetActive(true);
        Balls.enabled = false;
        //Wait(10.0f);
        //LoadToResult();
    }

    //private IEnumerator Wait(float second)
    //{
    //    yield return new WaitForSeconds(second);
    //}

    //void LoadToResult()
    //{
    //    SceneManager.LoadScene("result");
    //}
}
