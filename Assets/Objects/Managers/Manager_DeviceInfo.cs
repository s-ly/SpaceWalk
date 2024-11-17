using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

// Скрывает сенсорную клавиатуру если запуск на компе
public class Manager_DeviceInfo : MonoBehaviour {
  public bool deviseInfoDesktop = true;  

  // Сенсорная клавиатура
  [SerializeField] GameObject touch_keyboard_up;
  [SerializeField] GameObject touch_keyboard_down;

  [SerializeField] PlayerControl scriptPlayerControl;
  [SerializeField] PlayerTouchMove scriptPlayerTouchMove;
  [SerializeField] PlayerTouchRotation scriptPlayerTouchRotation;

  // Функция java-script
  [DllImport("__Internal")] private static extern void JS_DeviceInfo();

  //string Device;

  // Start is called before the first frame update
  void Start() {
#if UNITY_WEBGL
    JS_DeviceInfo();
#endif
  }

  // отключает сенсорную клавиатуру, если платформа: компьютер
  // иначе отключает управление мышкой
  public void Touch_Keyboard_SetActive(string Device) {
    if (Device == "desktop") {
      deviseInfoDesktop = true;
      touch_keyboard_up.SetActive(false);
      touch_keyboard_down.SetActive(false);

      scriptPlayerControl.enabled = true;
      scriptPlayerTouchMove.enabled = false;
      scriptPlayerTouchRotation.enabled = false;
    }
    else {
      deviseInfoDesktop = false;
      scriptPlayerControl.enabled = false;
      scriptPlayerTouchMove.enabled = true;
      scriptPlayerTouchRotation.enabled = true;
    }
  }
}


/*
В поле type возвращается строка "desktop" (компьютер), "mobile" (мобильное устройство), "tablet" (планшет) или "tv" (телевизор), 
а также все методы с одним из значений.

*/
