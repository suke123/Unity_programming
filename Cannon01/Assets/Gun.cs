﻿using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour {

    public GameObject BulletPrefab;
    public GameObject SparkPrefab;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKeyDown(KeyCode.Space)){
            Fire();
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
        rb.AddForce(transform.up * 30, ForceMode.VelocityChange);
    }
}
