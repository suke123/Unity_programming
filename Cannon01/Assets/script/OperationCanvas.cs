using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OperarionCanvas : MonoBehaviour {

    public Camera rotateCamera;
    public Slider hpSlider;

	// Use this for initialization
	void Start () {
        rotateCamera = Camera.main;

	}
	
	// Update is called once per frame
	void Update () {
        transform.rotation = rotateCamera.transform.rotation;
	}

    public void Disable()
    {
        this.gameObject.SetActive(false);
    }

    public void Damage(int damage)
    {
        hpSlider.value -= damage;
    }
}
