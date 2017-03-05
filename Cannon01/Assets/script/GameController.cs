using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public Gun gun;        //大砲
    public LifePanel lifePanel;     //自分HP
    //public ScoreScript enemy;       //敵の数
    //public EnemySpawn enemySpawn;   //敵の出現管理(ゲーム終了で出現停止)
    //public Text scoreLabel;         //クリア敵数
    //public EnemyBehavior deleteEnemy; //終了時に敵を消滅 

    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject enemy3;
    public GameObject enemy4;

    private float timer = 0.0f;
    private bool timerFlag;

    bool isCleared;         //クリアしたかどうか
    bool isDead;            //HPが0になったかどうか

    private List<int> existEnemy = new List<int>();
    private int enemyClearCount = 0; //指定数倒した敵種類数
    //int existEnemy;
    int gunHp;
    //int force;

    private List<GameObject> enemyList = new List<GameObject>();
    private List<GameObject> spawnEnemy = new List<GameObject>();
    private int spawnEnemyNum;

    [SerializeField]
    private Text CountDownText;
    [SerializeField]
    private Text StageNameText;
    
    public GameObject GameClearText;
    public GameObject nextStageButton;
    public GameObject GameOverText;

    public AudioClip gameClear;
    public AudioClip gameOver;

    private IEnumerator GameStart()
    {
        gun.enabled = false;
        //deleteEnemy.enabled = false;
        //enemySpawn.enabled = false;
        timerFlag = false;

        yield return new WaitForSeconds(1f);

        StageNameText.text = StageSelectManager.Instance.stage;
        for (int count = 3; count >= 0; count--)
        {
            if (count > 0)
            {
                CountDownText.text = count.ToString();
            }
            else
            {
                CountDownText.text = "Start";
                gun.enabled = true;
                //enemySpawn.enabled = true;
                //deleteEnemy.enabled = true;
                timerFlag = true;
            }

            yield return new WaitForSeconds(1f);
        }

        CountDownText.enabled = false;
        StageNameText.enabled = false;

        yield break;
    }

    // Use this for initialization
    void Start () {
        StartCoroutine(GameStart());

        GameClearText.GetComponent<Text>().text = GameOverText.GetComponent<Text>().text = StageSelectManager.Instance.stage;

        isCleared = false;
        isDead = false;
        foreach (int i in GetComponent<ScoreScript>().NumberOfEnemy())
        {
            existEnemy.Add(i);
        }
        gunHp = gun.Life();

        enemyList.Add(enemy1);
        enemyList.Add(enemy2);
        enemyList.Add(enemy3);
        enemyList.Add(enemy4);
        string stage = StageSelectManager.Instance.stage;
        Debug.Log(stage);
        TextAsset csv = Resources.Load("CSV/" + stage) as TextAsset;
        StringReader reader = new StringReader(csv.text);
        string line = reader.ReadLine();
        string[] n = line.Split(',');
        StageSelectManager.Instance.enemyNum.Clear();
        StageSelectManager.Instance.enemyNum.Add(int.Parse(n[5]));
        StageSelectManager.Instance.enemyNum.Add(int.Parse(n[7]));
        StageSelectManager.Instance.enemyNum.Add(int.Parse(n[9]));
        StageSelectManager.Instance.enemyNum.Add(int.Parse(n[11]));
        while (reader.Peek() > -1)
        {
            line = reader.ReadLine();
            string[] enemyValues = line.Split(',');
            int enemyType = int.Parse(enemyValues[0]);
            float spawnTime = float.Parse(enemyValues[1]);
            float spawnX = (float.Parse(enemyValues[2]) != 0) ? float.Parse(enemyValues[2]) : Random.Range(-130f, 185f);
            float spawnZ = (float.Parse(enemyValues[3]) != 0) ? float.Parse(enemyValues[3]) : Random.Range(200f, 264f);
            Vector3 pos = new Vector3(spawnX, 5, spawnZ);
            GameObject enemy = Instantiate(enemyList[enemyType], pos, Quaternion.identity);
            enemy.GetComponent<EnemyBehavior>().spawnTime = spawnTime;
            enemy.SetActive(false);
            spawnEnemy.Add(enemy);
            spawnEnemyNum++;
        }
    }
	
	// Update is called once per frame
	void Update () {
        gunHp = gun.Life();
        Debug.Log(gunHp);
        lifePanel.UpdateLife(gunHp);
        //Debug.Log("Hp更新するよ");
        existEnemy.Clear();
        foreach(int i in GetComponent<ScoreScript>().NumberOfEnemy())
        {
            existEnemy.Add(i);
        }
        Debug.Log(existEnemy.Count);
        //Debug.Log(existEnemy);

        if (timerFlag)timer += Time.deltaTime;

        spawnEnemyNum = spawnEnemy.Count;
        foreach(GameObject enemy in spawnEnemy)
        {
            if(enemy != null)
            {
                if (timer > enemy.GetComponent<EnemyBehavior>().spawnTime)
                {
                    enemy.SetActive(true);
                }
            }else
            {
                spawnEnemyNum--;
            }
        }

        enemyClearCount = 0;
        foreach(int i in existEnemy)
        {
            Debug.Log(i);
            if (i != 0) break;
            enemyClearCount++;
        }
        if(isDead==false && enemyClearCount == existEnemy.Count)
        {
            isCleared = true;
            enabled = false;

            DeleteAllEnemy();
            Invoke("StageClear", 1f);
        }

        if((gunHp <= 0 || spawnEnemyNum == 0) && isCleared ==false)
        {
            isDead = true;
            enabled = false;

            DeleteAllEnemy();
            Invoke("GameOver", 1f);
        }

	}

    void StageClear()
    {
        gun.enabled = false;
        AudioSource audio = GetComponent<AudioSource>();
        audio.clip = gameClear;
        audio.loop = false;
        audio.Play();
        GameClearText.transform.parent.gameObject.SetActive(true);
        string num = StageNameText.text.Substring(5);
        int n = int.Parse(num) + 1;
        if (StageSelectManager.Instance.stageNum < n)
        {
            nextStageButton.SetActive(false);
        }
    }

    void GameOver()
    {
        gun.enabled = false;
        AudioSource audio = GetComponent<AudioSource>();
        audio.clip = gameOver;
        audio.loop = false;
        audio.Play();
        GameOverText.transform.parent.gameObject.SetActive(true);
    }

    public void GoToNextStage()
    {
        string num = StageNameText.text.Substring(5);
        int n = int.Parse(num) + 1;
        StageSelectManager.Instance.stage = "Stage" + n;
        SceneManager.LoadScene("Cannon_test01");
    }

    public void ReturnToStageSelect()
    {
        SceneManager.LoadScene("Stage_Select");
    }

    public void ReplayStage()
    {
        SceneManager.LoadScene("Cannon_test01");
    }

    void DeleteAllEnemy()
    {
        //GameObject[] clones = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject clone in spawnEnemy)
        {
            if(clone != null)Destroy(clone);
        }
        spawnEnemy.Clear();
    }

}
