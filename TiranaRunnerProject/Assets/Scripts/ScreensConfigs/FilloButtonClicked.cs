using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FilloButtonClicked : MonoBehaviour {

    public Text InfoText;
    public string emriISkenes;
    public Button filloButton;
    public Button MbyllButton;
    private bool loadScene;
	// Use this for initialization
	void Start () {
        filloButton.onClick.AddListener(ShkoNeLevelSelection);
        MbyllButton.onClick.AddListener(DilNgaAplikacioni);
	}
	
	// Update is called once per frame
	void Update () {
        if (loadScene == true)
        {
            InfoText.color = new Color(InfoText.color.r, InfoText.color.g, InfoText.color.b, Mathf.PingPong(Time.time, 1));
        }
	}

    void ShkoNeLevelSelection()
    {
        loadScene = true;
        InfoText.text = "Po ngarkohet...";
        StartCoroutine(LoadingScene.LoadNewScene(emriISkenes));
    }

    void DilNgaAplikacioni()
    {
        loadScene = true;
        InfoText.text = "Shihemi perseri ;)";
        Application.Quit();
    }
}
