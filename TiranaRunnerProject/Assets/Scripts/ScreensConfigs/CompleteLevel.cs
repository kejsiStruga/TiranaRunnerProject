using Assets.CoinsScripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompleteLevel : MonoBehaviour
{
    public int levelToUnlock;
    public int levelCompleted;

    public string menuSceneName = "LevelSelectorOldWorkingA";

    public string levelSelectionScreen = "LevelSelectorOldWorkingA";

    public SceneFader sceneFader;

    public void Continue()
    {
        var score = ScoreSystem._instanceScore._score;
        string coinsCollected = CoinsSystem.InstanceCoins.coins.text;
        PlayerPrefs.SetInt("levelReached", 2);


        var hasHighScoreValue = PlayerPrefs.GetInt("HighScore");
        if (hasHighScoreValue <Convert.ToInt32(score))
        {
            PlayerPrefs.SetInt("HighScore", Convert.ToInt32(score));
        }

        var hasScoreValue = PlayerPrefs.GetInt("Score" + levelCompleted);
        if (hasScoreValue < Convert.ToInt32(score))
        {
            PlayerPrefs.SetInt("Score" + levelCompleted, Convert.ToInt32(score));
        }

        var hasCoinsValue = PlayerPrefs.GetInt("Coins" + levelCompleted);
        if (hasCoinsValue < Convert.ToInt32(coinsCollected))
        {
            PlayerPrefs.SetInt("Coins" + levelCompleted, Convert.ToInt32(coinsCollected));
        }

        sceneFader.FadeTo(levelSelectionScreen);
    }

    public void Menu()
    {
        sceneFader.FadeTo(menuSceneName);
    }

}
