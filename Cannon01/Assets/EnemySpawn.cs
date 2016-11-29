using UnityEngine;
using System.Collections;

public class EnemySpawn : MonoBehaviour {
    public GameObject enemy;   //敵オブジェクト
    //public Transform ground;   //地面オブジェクト
    public float count = 5;    //一度に何体のオブジェクトをスポーンさせるか
    public float interval = 5; //何秒おきに敵を発生させるか
    private float timer;

	// Use this for initialization
	void Start () {
        Spawn();  //初期のスポーン
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;  //経過時間の加算
        if (timer >= interval)
        {
            Spawn();
            timer = 0;
        }
	}

    void Spawn()
    {
        for (int i = 0; i < count; i++)
        {
            float x = Random.Range(-130f,185f);
            float z = Random.Range(200f,264f);
            Vector3 pos = new Vector3(x, 5, z);
            GameObject.Instantiate(enemy, pos, Quaternion.identity);
        }
    }
}
