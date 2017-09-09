using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GoToLevelSelection : MonoBehaviour {

    public Button backToMenuButton;
    public Text loadingText;
    public string emriSkenes = "LevelSelectorOldWorkingA";
    private bool loadScene;
    //public SceneFader sceneFader;

    void Start () {
        loadScene = false;
        backToMenuButton.onClick.AddListener(BackToLevelSelection);
	}
    void Update()
    {
        if (loadScene == true)
        {
            //LoadingScene._instanceA.loadingText.color = new Color(loadingText.color.r, loadingText.color.g, loadingText.color.b, Mathf.PingPong(Time.time, 1));
            loadingText.color = new Color(loadingText.color.r, loadingText.color.g, loadingText.color.b, Mathf.PingPong(Time.time, 1));
        }
    }

	void BackToLevelSelection() {
        loadScene = true;
        loadingText.text = "Duke u ngarkuar ...";
        //sceneFader.FadeTo(emriSkenes);
        StartCoroutine(LoadingScene.LoadNewScene("LevelSelectorOldWorkingA"));
    }
}
