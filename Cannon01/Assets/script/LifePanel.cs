using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifePanel : MonoBehaviour {
    public GameObject[] LifeIcons;
    int i = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void UpdateLife(int life)
    {
        for(i = 0; i < LifeIcons.Length; i++)
        {
            if (i < life)
            {
                LifeIcons[i].SetActive(true);
                //Debug.Log("Hp表示するよ");
            }
            else
            {
                LifeIcons[i].SetActive(false);
                Debug.Log("Hp非表示するよ");
            }
        }

    }
}
