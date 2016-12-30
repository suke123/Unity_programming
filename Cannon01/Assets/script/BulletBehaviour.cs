using UnityEngine;
using System.Collections;

public class BulletBehaviour : MonoBehaviour {
    public int damage = 1;      //鉄球のダメージ

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public GameObject ExplosionPrefab;
    void OnCollisionEnter(Collision WHO){
        Instantiate(
            ExplosionPrefab,
            transform.position,
            transform.rotation
            );

        Destroy(gameObject);

        //Enemyタグを持つオブジェクトに当たったら
        if(WHO.gameObject.tag == "Enemy")
        {
            //Damage関数を実行する。引数として弾のダメージを渡す
            WHO.gameObject.SendMessage("Damage", damage);
        }
    }
}
