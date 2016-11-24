using UnityEngine;
using System.Collections;

public class BulletBehaviour : MonoBehaviour {
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
