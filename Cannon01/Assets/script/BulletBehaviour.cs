using UnityEngine;
using System.Collections;

public class BulletBehaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public GameObject ExplosionPrefab;
    void OnCollisionEnter(Collider WHO){
        Instantiate(
            ExplosionPrefab,
            transform.position,
            transform.rotation
            );

        Destroy(gameObject);

        //Enemyタグを持つオブジェクトに当たったら
        if(WHO.tag == "Enemy")
        {
            //Damage関数を実行する
            WHO.SendMessage("Damage");
        }
    }
}
