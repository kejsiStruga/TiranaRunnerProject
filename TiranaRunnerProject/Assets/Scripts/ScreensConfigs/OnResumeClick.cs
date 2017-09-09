using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OnResumeClick : MonoBehaviour {

    public GameObject resumePanel;

    // Use this for initialization
    void Start () {
   
    }
	
	// Update is called once per frame
	public void ResumeButtonClicked() {
        Time.timeScale = 1;
        resumePanel.SetActive(false);
    }
}
