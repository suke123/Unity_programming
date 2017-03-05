using UnityEngine;
using System.Collections;

public class EnemySpawn : MonoBehaviour
{
    public GameObject enemy;        //敵オブジェクト.
    public Transform ground;        //地面オブジェクト.
    public float count = 1;         //一度に何体のオブジェクトをスポーンさせるか.
    public float interval = 10000;  //何秒おきに敵を発生させるか.
    private float timer = 0;
    public int enemy_exist = 0;     //フィールドに存在している敵の数.
    public int max_exist = 8;       //フィールドに同時に存在できる敵の数.
    public int clear_enemy;     //クリア条件のenemy数

    int i;

    // Use this for initialization
    void Start()
    {
        Spawn();  //初期のスポーン.
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;  //経過時間の加算.
        if (timer >= interval)
        {
            if (enemy_exist < max_exist)
            {
                Spawn();
            }
            timer = 0;
        }
    }

    //敵が減った時に減った分の敵をフィールドに生成する関数.
    /*public void ReSpawn()
    {
        this.enemy_exist -= 1;
        //Spawn();
    }*/

    void Spawn()
    {
        if (enemy_exist < max_exist)
        {
            for (i = 0; i < count; i++)
            {
                float x = Random.Range(-130f, 185f);
                float z = Random.Range(200f, 264f);
                Vector3 pos = new Vector3(x, 5, z);// + ground.position;
                Instantiate(enemy, pos, Quaternion.identity);
                this.enemy_exist += 1;
            }
        }
    }

    public void StopSpawn()
    {
        count = -1;
    }
}
