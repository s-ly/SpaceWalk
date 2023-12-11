using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

// Скрывает сенсорную клавиатуру если запуск на компе
public class Manager_DeviceInfo : MonoBehaviour
{
    // Сенсорная клавиатура
    [SerializeField] GameObject touch_keyboard_obj_1;
    [SerializeField] GameObject touch_keyboard_obj_2;
    [SerializeField] GameObject touch_keyboard_obj_3;
    [SerializeField] GameObject touch_keyboard_obj_4;
    [SerializeField] GameObject touch_keyboard_obj_5;
    [SerializeField] GameObject touch_keyboard_obj_6;
    [SerializeField] GameObject touch_keyboard_obj_7;
    [SerializeField] GameObject touch_keyboard_obj_8;

    // Функция java-script
    [DllImport("__Internal")] private static extern void JS_DeviceInfo();

    //string Device;

    // Start is called before the first frame update
    void Start()
    {
#if UNITY_WEBGL
        JS_DeviceInfo();
#endif
    }

    // отключает сенсорную клавиатуру, если платформа: компьютер
    public void Touch_Keyboard_SetActive(string Device)
    {
        if (Device == "desktop")
        {
            touch_keyboard_obj_1.SetActive(false);
            touch_keyboard_obj_2.SetActive(false);
            touch_keyboard_obj_3.SetActive(false);
            touch_keyboard_obj_4.SetActive(false);
            touch_keyboard_obj_5.SetActive(false);
            touch_keyboard_obj_6.SetActive(false);
            touch_keyboard_obj_7.SetActive(false);
            touch_keyboard_obj_8.SetActive(false);
        }
    }
}
