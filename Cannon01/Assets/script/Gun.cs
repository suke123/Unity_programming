using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour {

    public GameObject BulletPrefab;
    public GameObject SparkPrefab;
    public float gun_speed = 50;                            //弾のスピード
    public float cannon_x = Input.GetAxis("Vertical");      //大砲の立野動きの制限
    public float cannon_y = Input.GetAxis("Horizontal");    //大砲の横の動きの制限

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKeyDown(KeyCode.Space)){
            Fire();
        }

        if(cannon_x >= 34 && cannon_x <= 92)
        {
            transform.Rotate(
            new Vector3(Input.GetAxis("Vertical") * 60.0f * Time.deltaTime, 0, 0),
            Space.Self
            );
        }

        if (cannon_y >= -43 && cannon_y <= 43)
        {
            Transform Base = transform.parent;
            Base.Rotate(
                new Vector3(0, Input.GetAxis("Horizontal") * 60.0f * Time.deltaTime, 0),
                Space.World
                );
        }

    }

    void Fire() {
        Transform tar = transform.Find("SparkLoc");
        Instantiate(
             SparkPrefab,
             tar.position,
             transform.rotation
         );

        GameObject bullet = (GameObject)Instantiate(
            BulletPrefab, 
            transform.position, 
            transform.rotation
        );
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.AddForce(transform.up * gun_speed, ForceMode.VelocityChange);
    }
}
