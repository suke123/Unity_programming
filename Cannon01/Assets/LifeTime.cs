using UnityEngine;
using System.Collections;

public class LifeTime : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Invoke("Death", 10f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void Death () {
        Destroy(gameObject);
    }
}
