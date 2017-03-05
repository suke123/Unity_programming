using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeffenceLine : MonoBehaviour {

    public GameObject player;
    public GameObject gun;
    public float interval = 1.5f;
    public Gun gunLife;             // 大砲のHP

    private float timer = 0.0f;
    private bool damageFlag;
    private int count = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (damageFlag)
        {
            timer += Time.deltaTime;
        }
        if(timer > interval)
        {
            timer = 0.0f;
            count = 0;
            damageFlag = false;
            player.GetComponent<Blinker>().reDoMaterial();
            player.GetComponent<Blinker>().enabled = false;
            gun.GetComponent<Blinker>().reDoMaterial();
            gun.GetComponent<Blinker>().enabled = false;
        }
	}

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            Destroy(col.gameObject);
            Debug.Log("Base Damaged!");

            /*baseダメージ処理*/
            if(count == 0)gunLife.isDamaged(col.gameObject.GetComponent<EnemyBehavior>().EnemyForce());
            damageFlag = true;
            player.GetComponent<Blinker>().enabled = true;
            gun.GetComponent<Blinker>().enabled = true;
        }
    }
}
