using Assets.CoinsScripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreSystem1 : MonoBehaviour
{
    public static ScoreSystem1 _instanceScoreS;
    public Text scorePoints;
    private int difficultyLevel = 1;
    public float _score = 0.0f;
    public float difficulty;

    void Awake()
    {
        if (_instanceScoreS == null)
            _instanceScoreS = this;
        else
            UnityEngine.Object.Destroy(this);
    }
    public static ScoreSystem1 InstanceScoreS
    {
        get { return _instanceScoreS; }
    }
    void Start()
    {
        this._score -= Time.deltaTime * difficultyLevel;
        scorePoints.text = ((int)_score).ToString();
        PlayerPrefs.SetInt("Score", (int)_score);
    }
    void Update()
    {
        difficulty = (Time.time + 300) / 30;
        // Debug.Log(difficulty);
        _score += Time.deltaTime * difficultyLevel;

        scorePoints.text = ((int)_score).ToString();
    }
}



