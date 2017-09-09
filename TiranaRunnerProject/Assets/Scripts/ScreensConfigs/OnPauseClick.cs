using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OnPauseClick : MonoBehaviour {

    private bool paused;
    public Button pauseButton;
    public Button resumeButton;
    public Button SoundButton;
    public GameObject resumePanel;
    public AudioSource sound;
    public Sprite soundOff;
    public Sprite soundOn;

    // Use this for initialization
    public void Start () {

        paused = false;
        Debug.Log("Hello fhjfhskjfhj");
        pauseButton.onClick.AddListener(PauseButtonClicked);
        resumeButton.onClick.AddListener(PauseButtonClicked);
        SoundButton.onClick.AddListener(SoundButtonClicked);
    }

    public void PauseButtonClicked() 
    {
        paused = !paused;
        if (paused)
        {
            Time.timeScale = 0;
            resumePanel.SetActive(true);
        }
        else // mund te kthehesh ne loje edhe duke shtypur perseri butonin Pause (pervecse Resume)
        {
            Time.timeScale = 1;
            resumePanel.SetActive(false);
        }
        SoundButtonClicked();
	}

    public void SoundButtonClicked()
    {
        if (!sound.mute)
        {
            Debug.Log("Eshte shtypur Mute button!");
            sound.mute = true;
            SoundButton.image.overrideSprite = soundOff;
        }
        else
        {
            Debug.Log("Eshte shtypur Sound button!");
            sound.mute = false;
            SoundButton.image.overrideSprite = soundOn;
        }
    }
}
