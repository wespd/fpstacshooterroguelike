using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class timer : MonoBehaviour
{
    public Text text;
    float startTime;
    float currentTime;
    public int decimals;

    public bool isTimeLimit;
    public float timeLimit;

    string timeLimitString;
    public playerHP hP;

    private void Start()
    {
        timeLimitString = "";
        if(isTimeLimit)
        {
            timeLimitString = " out of " + timeLimit.ToString();
        }
        startTime = Time.time;
    }

    private void Update()
    {
        currentTime = Time.time - startTime;
        text.text = currentTime.ToString($"F{decimals}") + timeLimitString;
        if(isTimeLimit && currentTime >= timeLimit)
        {
            hP.OnDeath();
        }
    }
}
