using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class timer : MonoBehaviour {


    public GameObject character;
    public GameObject timerText;
    public GameObject healthBar;

    private Text showText;
    float countdown = 300f;
    float TimerCountdown;
    int minutes, seconds;

    private CharacterScript characterScript;

	// Use this for initialization
	void Start ()
    {
        characterScript = character.GetComponent<CharacterScript>();
        showText = timerText.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update ()
    {
        UpdateTime();
	}

    void UpdateTime()
    {
        TimerCountdown += Time.deltaTime;
        int remainingTime = (int) countdown - (int) TimerCountdown;

        if (remainingTime >= 1)
        {
            minutes = remainingTime / (int)60f;
            seconds = remainingTime % 60;
            showText.text = "Time: " + minutes + ":" + seconds.ToString("00");
        }
        else
        {
            showText.text = "Time: 0:00";
            characterScript.characterDeath();
        }
    }
}
