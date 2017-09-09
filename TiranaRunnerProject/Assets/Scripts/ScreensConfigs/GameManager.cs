using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Assets.CoinsScripts;

public class GameManager : MonoBehaviour
{
    public bool GameIsOver;
    public GameObject gameOverUI;
    public GameObject completeLevelUI;
    public AudioSource backgroundMusic;

    public AudioClip gameOverSFX;

    public AudioClip beatLevelSFX;

    void Start()
    {
        GameIsOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameIsOver)
            return;
    }

    public void EndGame()
    {
        GameIsOver = true;
        gameOverUI.SetActive(true);
        //backgroundMusic.volume = 0.00f;
        //backgroundMusic.clip = gameOverSFX;
        //backgroundMusic.Play();
        //if (backgroundMusic.volume <= 0.0f)
        //{
        //    AudioSource.PlayClipAtPoint(gameOverSFX, gameObject.transform.position);
        //}
    }

    public void WinLevel()
    {
        GameIsOver = true;
        completeLevelUI.SetActive(true);
        Unit1._instance.lost = true;
    }

}
