using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class timer : MonoBehaviour
{
    public Text text;
    float startTime;
    float currentTime;

    private void Start()
    {
        startTime = Time.time;
    }

    private void Update()
    {
        currentTime = Time.time - startTime;
        text.text = currentTime + "";
    }
}
