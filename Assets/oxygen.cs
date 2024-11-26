using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class oxygen : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] public float oxygenTime;
    public float oxygenTimeTemp;
    private bool trigerOxygen = false;

    // менеджер игры
    [SerializeField] private GameObject GameManager; 
    private GameManager script_GameManager;

    [SerializeField] AudioSource sound_oxy;

    // Start is called before the first frame update
    void Start()
    {
        // Загрузка данных о кислороде из межуровнего хранилища 
        oxygenTime = ProgressManager.Instance.YandexDataOBJ.Oxygen;

        oxygenTimeTemp = oxygenTime;        
        UpdateUIOxygen();
        script_GameManager = GameManager.GetComponent<GameManager>(); // менеджер игры
    }

    // Update is called once per frame
    void Update()
    {
        if (trigerOxygen)
        {
            oxygenTime = oxygenTime - Time.deltaTime;
            if (oxygenTime < 0) oxygenTime = 0; // что-бы не уходить в минус
            UpdateUIOxygen();

            if (oxygenTime == 0)
            {
                //ProgressManager.Instance.Oxygen = oxygenTimeTemp; // Сохранение данных между уровнями
                script_GameManager.GameOwer(); // Загрузка GAME OVER                
            }
        }
    }

    public void oxygenTimerRestart()
    {
        sound_oxy.Play();
        oxygenTime = oxygenTimeTemp; 
        trigerOxygen = false;
        //text.text = (Mathf.Round(oxygenTime)).ToString();
        UpdateUIOxygen();
        Debug.Log("oxygenTimerRestart");
    }
    public void oxygenTimerExit()
    {        
        trigerOxygen = true;
        Debug.Log("oxygenTimerExit");
    }

    public void UpdateUIOxygen()
    {
        text.text = (Mathf.Round(oxygenTime)).ToString();
    }
    public void oxygenAdd() {
        sound_oxy.Play(); // sound
        int oxy_balon_size = 10; // емкость балона
        for (int i = 0; i < oxy_balon_size; i++) {
            if (oxygenTime < oxygenTimeTemp) {
                oxygenTime++;
            }
        }
        UpdateUIOxygen();
    }
}