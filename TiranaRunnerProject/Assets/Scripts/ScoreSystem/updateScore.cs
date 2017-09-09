using System.Collections;
using Assets.CoinsScripts;
using UnityEngine;
using UnityEngine.UI;

public class updateScore : MonoBehaviour
{
    public Text scoreText;
    public Text coinsText;
    void OnEnable()
    {
        StartCoroutine(AnimateText());
    }

    IEnumerator AnimateText()
    {
        scoreText.text = "0";
        coinsText.text = "0";
        string round = ((int)ScoreSystem._instanceScore._score).ToString();
        string coinsCollected = CoinsSystem.InstanceCoins.coins.text;

        scoreText.text = round;
        coinsText.text = coinsCollected;

        yield return new WaitForSeconds(.000005f);
    }
}


//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
//public class updateScore : MonoBehaviour
//{
//    public Text scoreText;
//    public Text coinsText;
//    void OnEnable()
//    {
//        StartCoroutine(AnimateText());
//    }

//    IEnumerator AnimateText()
//    {
//        string str = "0";
//        string round = Convert.ToInt32(ScoreSystem1._instanceScoreS._score).ToString();

//        if (Spin.coin != null)
//        {
//            str = (Spin.coin.coins.text.ToString() == "") ? "0" : Spin.coin.coins.text.ToString();

//            scoreText.text = round;
//            coinsText.text = str;

//            int coins = Convert.ToInt32(str);
//        }


//        yield return new WaitForSeconds(.000005f);
//    }

//}
