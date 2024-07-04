using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOwer : MonoBehaviour {
    [DllImport("__Internal")] private static extern void JS_ShowAdv();
    float Timer_Start_Adv = 1.8f;
    float Timer_Show_Restart_Button = 5.0f;
    public GameObject ButtonRestart;
    public GameObject TextLoad;

    // Start is called before the first frame update
    void Start() {
        TextLoad.SetActive(false);
        ButtonRestart.SetActive(false);
    }

    // Update is called once per frame
    void Update() {
        TimerAdvStart();
        TimerShowRestartButton();
    }
    void ShowAdv() {
#if UNITY_WEBGL
        JS_ShowAdv();
#endif
    }
    void TimerAdvStart() {
        // после смерти держим экран немного
        if (Timer_Start_Adv > 0) {
            Timer_Start_Adv = Timer_Start_Adv - Time.deltaTime;
            if (Timer_Start_Adv <= 0) {
                Debug.Log("=============>ShowAdv");
                ShowAdv();
            }
        }
    }
    void TimerShowRestartButton() {
        // после смерти держим экран немного
        if (Timer_Show_Restart_Button > 0) {
            Timer_Show_Restart_Button = Timer_Show_Restart_Button - Time.deltaTime;
            if (Timer_Show_Restart_Button <= 0) {
                ShowButRestart();
            }
        }
    }
    public void ShowButRestart() {
        ButtonRestart.SetActive(true);
    }

    public void LoadLevel() {
        StartCoroutine(ENUM_LoadLevel());
    }

    IEnumerator ENUM_LoadLevel() {
        ButtonRestart.SetActive(false);
        yield return new WaitUntil(() => !ButtonRestart.gameObject.activeSelf);
        TextLoad.SetActive(true);
        yield return new WaitUntil(() => TextLoad.gameObject.activeSelf);

        SceneManager.LoadScene(1);
    }
}
