using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class timer : MonoBehaviour
{
    public Text text;
    public float startTime;
    float currentTime;
    public int decimals;

    public bool isTimeLimit;
    public float timeLimit;

    string timeLimitString;
    public playerHP hP;

    public bool timerPaused;

    private void Start()
    {
        timeLimitString = "";
        if(isTimeLimit)
        {
            timeLimitString = " out of " + timeLimit.ToString();
        }
        startTime = Time.time;
        timerPaused = false;
    }

    private void Update()
    {
        if(!timerPaused)
        currentTime = Time.time - startTime;
        text.text = currentTime.ToString($"F{decimals}") + timeLimitString;
        if(isTimeLimit && currentTime >= timeLimit)
        {
            hP.OnDeath();
        }
    }
    public void pauseTimer() 
    {
        timerPaused = !timerPaused;
    }
}
