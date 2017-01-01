using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour {

    public GameObject BulletPrefab;
    public GameObject SparkPrefab;
    public float gun_speed = 50;

    // Use this for initialization
    void Start () {
        //transform.parent = GameObject.Find("Base").transform;
        //Transform tar = GameObject.Find("SparkLoc").transform;
    }
	
	// Update is called once per frame
	void Update () {
        Transform Base = transform.parent;
        if(Base != null){
            Base.Rotate(
                new Vector3(0, Input.GetAxis("Horizontal") * 60.0f * Time.deltaTime, 0),
                Space.World
            );
        }
        else
        {
            Debug.LogWarning("Set Base!");
        }
      
        transform.Rotate(
            new Vector3(Input.GetAxis("Vertical") * 60.0f * Time.deltaTime, 0, 0),
            Space.Self
        );

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Fire();
        }

    }

    public void Fire() {
        //Transform tar = GameObject.Find("SparkLoc").transform;
        Transform tar = transform.Find("SparkLoc");
        if (tar != null)
        {
            Instantiate(
                SparkPrefab,
                tar.position,
                transform.rotation
            );
        }
        else
        {
            Debug.LogWarning("Set tar!");
        }

        GameObject bullet = (GameObject)Instantiate(
            BulletPrefab, 
            transform.position, 
            transform.rotation
        );
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.AddForce(transform.up * gun_speed, ForceMode.VelocityChange);
    }
}
