using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnMuteClick : MonoBehaviour {

    private static Button myButton;
    private GameObject soundButton;

    public AudioSource sound;
    public Sprite soundOff;
    public Sprite soundOn;

    // Use this for initialization
    void Start () {
        soundButton = GameObject.FindGameObjectWithTag("Mute");
        myButton = soundButton.GetComponent<Button>();
        myButton.onClick.AddListener(SoundButtonClicked);
    }
	
	
	public void SoundButtonClicked() {

        if (!sound.mute)
        {
            Debug.Log("Eshte shtypur Mute button!");
            sound.mute = true;
            myButton.image.overrideSprite = soundOff;
        }
        else
        {
            Debug.Log("Eshte shtypur Sound button!");
            sound.mute = false;
            myButton.image.overrideSprite = soundOn;
        }
	}
}
