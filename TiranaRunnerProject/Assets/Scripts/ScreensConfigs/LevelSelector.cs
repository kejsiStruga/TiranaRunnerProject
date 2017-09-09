using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour {
    //fade & change between scenes !
    public SceneFader fader;
    //arr of buttons of levels
    public Button[] levelButtons;
    private bool showPopUp;
    private bool Ongui;
    public Text loadingText;
    public string emriSkenes;
    private bool loadScene;
    public bool kejsi;
    public AnimationCurve curve;
    void Start()
    {
        /// nr of highest level we have reached; name of ; string value
        /// associated with that number
        /// default is one 


        int levelReached = PlayerPrefs.GetInt("levelReached", 1);

        for (int i = 0; i < levelButtons.Length; i++)
        {
            if (i + 1 > levelReached)
            {
                Debug.Log("Non interractable !!");
             //   levelButtons[i].image.color = new Color(0f,0f,0f);
                levelButtons[i].GetComponentInChildren<Text>().text = "?";
              //  Resources.GetBuiltinResource<Font>("TitilliumWeb-Black.ttf");

                //levelButtons[i].interactable = false;
                levelButtons[i].enabled = false;
            }
        }
    }

    void Update()
    {
        if (loadScene == true)
        {
            loadingText.color = new Color(loadingText.color.r, loadingText.color.g, loadingText.color.b, Mathf.PingPong(Time.time, 1));
        }
    }

	public void Select (string levelName) 
    {
        fader.FadeTo(levelName); 
	}

    public void FadeToPanel(GameObject panel)
    {
        StartCoroutine(showPanel(panel));
    }

    public void FadeToLevelSelection(GameObject panel)
    {
        //StartCoroutine(hidePanel(panel));
        loadScene = true;
        loadingText.text = "Loading ...";
        levelButtons[0].GetComponentInChildren<Text>().color = new Color(0f, 0f, 0f, 255);
        levelButtons[1].GetComponentInChildren<Text>().color = new Color(0f, 0f, 0f, 255);
        levelButtons[2].GetComponentInChildren<Text>().color = new Color(0f, 0f, 0f, 255);
        StartCoroutine(LoadingScene.LoadNewScene("TiranaRunner"));
    }

    public void FadeToLevelSelectionAnullo(GameObject panel)
    {
        StartCoroutine(hidePanel(panel));
    }

    IEnumerator showPanel(GameObject panel)
    {
       // Color color = img.color;

        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime;
            float a = curve.Evaluate(t);
            yield return 0;
        }
        panel.gameObject.SetActive(true);
    }
    IEnumerator hidePanel(GameObject panel)
    {
     //   Color color = img.color;
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime;
            float a = curve.Evaluate(t);
            yield return 0;
        }
        panel.gameObject.SetActive(false);
    }
    public void FadeMe()
    {
        StartCoroutine("FadeCanvas");
    }

}
