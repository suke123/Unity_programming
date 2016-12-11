using UnityEngine;
using System.Collections;

public class EnemyGeneratorCtrl : MonoBehaviour
{
    //生まれてくる敵プレハブ
    public GameObject enemyPrefab;
    //敵を格納
    GameObject[] existEnemys;
    //アクティブ最大数
    public int maxEnemy = 2;

    // Use this for initialization
    void Start()
    {
        //配列確保
        existEnemys = new GameObject[maxEnemy];
        //周期的に実行したい場合はコルーチン
        StartCoroutine(Exec());

    }

    //敵を作成する
    IEnumerator Exec()
    {
        while (true)
        {
            Generate();
            yield return new WaitForSeconds(3.0f);
        }
    }

    void Generate()
    {
        for (int enemyCount = 0; enemyCount < existEnemys.Length; ++enemyCount)
        {
            if (existEnemys[enemyCount] == null)
            {
                //敵を作成する
                existEnemys[enemyCount] = Instantiate(enemyPrefab, transform.position, transform.rotation) as GameObject;
                return;
            }
        }
    }
}