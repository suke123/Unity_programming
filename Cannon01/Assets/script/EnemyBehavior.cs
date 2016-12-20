using UnityEngine;
using System.Collections;

public class EnemyBehavior : MonoBehaviour {

    private Transform player;

    public float enemySpeed = 5f;
    public float rotationSmooth = 1f;

	// Use this for initialization
	void Start () {
        player = GameObject.FindWithTag("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
        // プレイヤーの方向を向く
        Quaternion targetRotation = Quaternion.LookRotation(player.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSmooth);

        // 前方に進む
        transform.Translate(Vector3.forward * enemySpeed * Time.deltaTime);
    }
}
