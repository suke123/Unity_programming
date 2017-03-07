using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class buttonScript : MonoBehaviour {

    //public Button StartButton; 

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}

    public void StartButtonClicked()
    {
        SceneManager.LoadScene("Main");
    }

    public void LoadToTitle()
    {
        SceneManager.LoadScene("Title");
    }

    public void LoadToResult()
    {
        SceneManager.LoadScene("Result");
    }

    public void QuitGame()
    {
        //UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
}
