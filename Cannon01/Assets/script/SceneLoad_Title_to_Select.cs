using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoad_Title_to_Select : MonoBehaviour {

   

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SceneLoad()
    {
        //gameObject.GetComponent<AudioSource>().Play();
        SceneManager.LoadScene("Stage_Select");
    }

    public void GameEnd()
    {
        //gameObject.GetComponent<AudioSource>().Play();
        Debug.Log("Game End");
        Application.Quit();
    }
}
