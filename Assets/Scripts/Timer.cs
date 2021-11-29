using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Timer : MonoBehaviour
{
    private TextMeshProUGUI TimerText;
    public float startTime;
    private bool started = false;
    private bool finnished = false;
    public static float t; // bad coding style but ok for 2 variables

    // Start is called before the first frame update
    void Start()
    {
        TimerText = GetComponent<TextMeshProUGUI>();
        StartTime();
    }

    // Update is called once per frame
    void Update()
    {
        if (started)
        {
            if (finnished){
                return;
            }
                

            t =  startTime - Time.time;

            string minutes = ((int)t / 60).ToString();
            string seconds = (t % 60).ToString("f0");

            TimerText.text = minutes + ":" + seconds;

             if (t <= 0)
            {
                finnished = true;
            }
        }
    }

    public void StartTime() 
    {
        started = true;
    }

    public void Finnish()
    {
        finnished = true;
        TimerText.fontSize = 60;
        TimerText.color = Color.green;
    }
}