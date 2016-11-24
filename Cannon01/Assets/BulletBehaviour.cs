using UnityEngine;
using System.Collections;

public class BulletBehaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    GameObject ExplosionPrefab;
    void OnCollisionEnter(Collision WHO){
        Instantiate(
            ExplosionPrefab,
            transform.position,
            transform.rotation
            );

        Destroy(gameObject);
    }
}
