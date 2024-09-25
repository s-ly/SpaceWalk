using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOwer : MonoBehaviour {
  [SerializeField] Yandex script_Yandex;
  [DllImport("__Internal")] private static extern void JS_ShowAdv();
  float Timer_Start_Adv = 1.8f;
  float Timer_Show_Restart_Button = 0.5f;
  public GameObject ButtonRestart;
  public GameObject TextLoad;

  // Start is called before the first frame update
  void Start() {
    TextLoad.SetActive(false);
    ButtonRestart.SetActive(false);
#if UNITY_WEBGL
    script_Yandex.GameStop(); // отчёт Яндексу
#endif
  }

  // Update is called once per frame
  void Update() {
    // TimerAdvStart();
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

  // загрузка уровня по нажатию "Рестарт"
  public void LoadLevel() {
    ShowAdv();
    // StartCoroutine(ENUM_LoadLevel());
  }

  public void StartReload() {
    StartCoroutine(ENUM_LoadLevel());
  }

  // Перед тем, как загрузить уровень, корутина ждё пока точно
  // скроется кнопка "Рестарт" и появится текст "Загрузка",
  // только потом загружается уровень.
  IEnumerator ENUM_LoadLevel() {
    ButtonRestart.SetActive(false);
    yield return new WaitUntil(() => !ButtonRestart.gameObject.activeSelf);
    TextLoad.SetActive(true);
    yield return new WaitUntil(() => TextLoad.gameObject.activeSelf);

    SceneManager.LoadScene(1);
  }
}
