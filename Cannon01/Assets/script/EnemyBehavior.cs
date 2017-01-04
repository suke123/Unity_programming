using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemyBehavior : MonoBehaviour {

    private Transform player;
    //GameObject enemy_exist;
    GameObject spawner;
    public float enemySpeed = 2f;
    public float rotationSmooth = 1f;
    //public Slider hpSlider;

    //ライフの設定（[]によって、privateな変数でもInspectorで設定できる）.
    [SerializeField]
    private int life = 3;           //Enemyのライフ（仮）.
    public GameObject particle;     //敵が撃破された時のエフェクト.

	// Use this for initialization
	void Start () {
        player = GameObject.FindWithTag("Player").transform;
        spawner = GameObject.Find("Spawner");
	}
	
	// Update is called once per frame
	void Update () {
        // プレイヤーの方向を向く.
        Quaternion targetRotation = Quaternion.LookRotation(player.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSmooth);

        // 前方に進む.
        transform.Translate(Vector3.forward * enemySpeed * Time.deltaTime);
    }

    //Damage関数、弾が当たった時の処理.
    public void Damage(int damage)
    {
        life -= damage;
        //hpSlider.value -= damage;
        if (life <= 0)
        {
            Dead();
            /*enemy_exist = GameObject.Find("Stage"); //Stageオブジェクトに付属しているEnemySpawnを参照する.
            EnemySpawn es = enemy_exist.GetComponent<EnemySpawn>();
            es.enemy_exist--;   //EnemySpawnのenemy_existを-1する。*/
            //AddEnemy();
            EnemySpawn es = spawner.GetComponent<EnemySpawn>();
            if (es != null)
            {
                es.ReSpawn();
            }
            else
            {
                Debug.LogWarning("Set es!");
            }
        }
    }

    //Enemyのライフが0になった時の処理.
    void Dead()
    {
        Destroy(gameObject);
        if (particle != null)   //撃破エフェクトパーティクルを設定していなくても、エラーが起きないためのものです.
        {
            //撃破エフェクトの生成.
            Instantiate(particle, transform.position, Quaternion.identity);
        }
    }
}
