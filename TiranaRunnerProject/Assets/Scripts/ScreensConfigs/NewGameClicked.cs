using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NewGameClicked : MonoBehaviour {
    
    public Button newgameButton;
	// Use this for initialization
	void Start () {
        newgameButton.onClick.AddListener(RestartGame);
	}
	
	// Update is called once per frame
	void RestartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
}
