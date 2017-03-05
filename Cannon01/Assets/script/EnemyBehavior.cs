using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemyBehavior : MonoBehaviour
{

    private Transform player;
    //GameObject enemy_exist;
    public int enemyType;
    public float enemySpeed = 2f;
    public float rotationSmooth = 1f;
    //public Slider hpSlider;

    public float spawnTime;

    GameObject scoreGUI;
    private int point = 1;

    public int enemyForce;

    //ライフの設定（[]によって、privateな変数でもInspectorで設定できる）.
    [SerializeField]
    public int life = 3;           //Enemyのライフ（仮）.
    public GameObject particle;     //敵が撃破された時のエフェクト.

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        //hoSlider = 
        scoreGUI = GameObject.Find("Text_EnemyScore");
        //ScoreScript ss = scoreGUI.GetComponent<ScoreScript>();
    }

    // Update is called once per frame
    void Update()
    {
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
        }
    }

    //Enemyのライフが0になった時の処理.
    public void Dead()
    {
        scoreGUI = GameObject.Find("Stage");
        ScoreScript ss = scoreGUI.GetComponent<ScoreScript>();

        Destroy(gameObject);

        ss.AddScore(enemyType, point);     //敵が死ぬとText_EnemyScoreの値を1減らす

        if (particle != null)   //撃破エフェクトパーティクルを設定していなくても、エラーが起きないためのものです.
        {
            //撃破エフェクトの生成.
            Instantiate(particle, transform.position, Quaternion.identity);
        }
    }

    //敵の攻撃力を返す関数
    public int EnemyForce()
    {
        return enemyForce;
    }
}
