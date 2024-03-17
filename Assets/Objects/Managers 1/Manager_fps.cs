using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// ������� � ������� � TextMeshPro FPS
public class Manager_fps : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private float TimePeriod = 3f;
    [SerializeField] bool tes_d = false;
    private float fps;    
    private float TimeData = 0f;

    private void Update()
    {
        fps = 1.0f / Time.deltaTime; // ������� fps
        Timer(fps);
    }

    // ������� FPS 
    private void ShowFPS(float fps)
    {        
        float fpsTEMP = fps;
        fpsTEMP = Mathf.Round(fpsTEMP); // ���������
        text.text = ("������ � �������: " + fpsTEMP.ToString());        
    }

    // ������
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
