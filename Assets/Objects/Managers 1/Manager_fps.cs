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
    string text_fps = "fps: ";

    void Start(){
      text_fps = TextManager.Inst_TextData.textsData.fps;
    }
    
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
        text.text = (text_fps + fpsTEMP.ToString());        
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
