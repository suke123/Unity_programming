using UnityEngine;
using System.Collections;

public class BulletBehaviour : MonoBehaviour
{
    public int damage = 1;      //鉄球のダメージ.
    public float BulletTime = 7f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public GameObject ExplosionPrefab;
    void OnCollisionEnter(Collision WHO)
    {
        //Instantiate(
        //    ExplosionPrefab,
        //    transform.position,
        //    transform.rotation
        //);

        //Destroy(gameObject);

        //Groundタグを持つオブジェクトに当たったら
        if(WHO.gameObject.tag == "Ground")
        {
            Invoke("DeleteBullet", BulletTime);
        }

        //Enemyタグを持つオブジェクトに当たったら.
        if (WHO.gameObject.tag == "Enemy")
        {
            Instantiate(
            ExplosionPrefab,
            transform.position,
            transform.rotation
        );

            Destroy(gameObject);

            //Damage関数を実行する。引数として弾のダメージを渡す.
            WHO.gameObject.SendMessage("Damage", damage);
            OperationCanvas os = GetComponent<OperationCanvas>();
            if (os != null)
            {
                os.Damage(damage);
            }
            else
            {
                Debug.LogWarning("ダメージがHPゲージに対応してないよ");
            }
        }
    }

    //転がる弾を消す
    void DeleteBullet()
    {
        Destroy(gameObject);
    }
}
