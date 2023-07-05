using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// Считает и выводит в TextMeshPro FPS
public class Manager_fps : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private float TimePeriod = 3f;
    private float fps;    
    private float TimeData = 0f;

    private void Update()
    {
        fps = 1.0f / Time.deltaTime; // считаем fps
        Timer(fps);
    }

    // выводит FPS 
    private void ShowFPS(float fps)
    {        
        float fpsTEMP = fps;
        fpsTEMP = Mathf.Round(fpsTEMP); // округлили
        text.text = ("Кадров в секунду: " + fpsTEMP.ToString());        
    }

    // таймер
    private void Timer(float fps)
    {
        TimeData += Time.deltaTime;
        if (TimeData >= TimePeriod)
        {            
            TimeData = 0f;
            ShowFPS(fps);
        }
    }
}
