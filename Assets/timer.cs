using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class timer : MonoBehaviour
{
    public Text text;
    public float startTime;
    List<float> times = new();
    int currentTime = 0;
    public int decimals;

    public bool isTimeLimit;
    public float timeLimit;

    string timeLimitString;
    public playerHP hP;

    public bool timerPaused;

    private void Start()
    {
        resetTimer();
    }

    private void Update()
    {
        if(!timerPaused)
        times[currentTime] = Time.time - startTime;
        string timerText = "";
        int level = 1;
        foreach(float time in times)
        {
            timerText += level + ": "  + time.ToString($"F{decimals}") + " ";
            level++;
        }
        text.text = timerText + timeLimitString;
        if(isTimeLimit && times[currentTime] >= timeLimit)
        {
            hP.OnDeath();
        }
    }
    public void toggleTimer() 
    {
        timerPaused = !timerPaused;
    }
    public void nextLevelTime()
    {
        startTime = Time.time;
        times.Add(1);
        currentTime++;
        Debug.Log(times.Count);
    }
    public void resetTimer()
    {
        timeLimitString = "";
        if(isTimeLimit)
        {
            timeLimitString = " out of " + timeLimit.ToString();
        }
        startTime = Time.time;
        timerPaused = false;
        times = new();
        times.Add(0);
        currentTime = 0;
    }
}
