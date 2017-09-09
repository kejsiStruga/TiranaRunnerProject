using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectorA : MonoBehaviour {
    //fade & change between scenes !
    public SceneFader fader;
    public GameObject panel;

    //arr of buttons of levels
    public Button[] levelButtons;
    private bool showPopUp;
    private bool Ongui;
    public Text loadingText;
    public string emriSkenes;
    private bool loadScene;
    public bool kejsi;


    #region Score Labels

    public Text highScore;
    public Text highScoreValue;
    public Text score;
    public Text scoreValue;
    public Text coins;
    public Text coinsValue;
    private int selectedLevel;

    #endregion

    public AnimationCurve curve;
    void Start()
    {
        /// nr of highest level we have reached; name of ; string value
        /// associated with that number
        /// default is one 
        /// 
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
        if (loadScene == true && LoadingScene._instanceA.loadingText.color != null)
        {
            LoadingScene._instanceA.loadingText.color =
                new Color(LoadingScene._instanceA.loadingText.color.r, LoadingScene._instanceA.loadingText.color.g,
                    LoadingScene._instanceA.loadingText.color.b, Mathf.PingPong(Time.time, 1));
        }
    }

	public void Select (string levelName) 
    {
        loadScene = true;
        LoadingScene._instanceA.loadingText.text = "Loading ...";
        fader.FadeTo(Levels.LevelsNames[selectedLevel-1]); 
	}

    public void FadeToPanel(int level)
    {
        selectedLevel = level;
        SetActive(false);
        StartCoroutine(showPanel(panel, level));
    }

    public void FadeToLevelSelection(GameObject panel)
    {
        StartCoroutine(hidePanel(panel));
    }

    IEnumerator showPanel(GameObject panel, int level)
    {
        var hasHighScoreValue = PlayerPrefs.GetInt("HighScore");
        if (hasHighScoreValue > 0)
        {
            highScore.gameObject.SetActive(true);
            highScoreValue.text = hasHighScoreValue.ToString();
            highScoreValue.gameObject.SetActive(true);
        }
        var hasScoreValue = PlayerPrefs.GetInt("Score" + level);
        if (hasScoreValue > 0)
        {
            score.gameObject.SetActive(true);
            scoreValue.text = hasScoreValue.ToString();
            scoreValue.gameObject.SetActive(true);
        }
        var hasCoinsValue = PlayerPrefs.GetInt("Coins" + level);
        if (hasCoinsValue > 0)
        {
            coins.gameObject.SetActive(true);
            coinsValue.text = hasCoinsValue.ToString();
            coinsValue.gameObject.SetActive(true);
        }

        // Color color = img.color;
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime;
            float a = curve.Evaluate(t); 
           // levelButtons[1].image.color = new Color(0f, 0f, 0f, a);
           // levelButtons[2].image.color = new Color(0f, 0f, 0f, a);
           // levelButtons[0].GetComponentInChildren<Text>().color = new Color(0f, 0f, 0f, a);
           // levelButtons[1].GetComponentInChildren<Text>().color = new Color(0f, 0f, 0f, a);
           // levelButtons[2].GetComponentInChildren<Text>().color = new Color(0f, 0f, 0f, a);  
           // img.color = new Color(0f, 0f, 0f, a);
           //// skip to the next frame
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

    private void SetActive(bool value)
    {
        highScore.gameObject.SetActive(value);
        highScoreValue.gameObject.SetActive(value);
        score.gameObject.SetActive(value);
        scoreValue.gameObject.SetActive(value);
        coins.gameObject.SetActive(value);
        coinsValue.gameObject.SetActive(value);
    }
}

internal class Levels
{
    public static List<string> LevelsNames = new List<string>()
    {
        "TiranaRunner45",
        "SecondLevelTiranaRunner"
    };
}
