using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {
    [SerializeField] TextMeshProUGUI UserID_text_MainMenu;
    [SerializeField] TextMeshProUGUI PlayerDataText;
    [SerializeField] Yandex script_Yandex;
    [SerializeField] GameObject ButtonLoadGame;
    //[SerializeField] TextMeshProUGUI Text_DeviceInfo;

    void Start() {
        Set_UserID_text_MainMenu("none");
        PlayerDataShowInMainMenu();
        //Set_Text_DeviceInfo();

#if UNITY_WEBGL
        // Debug.Log("Unity Editor");
        script_Yandex.Button_LogUserID(); // показывает id пользователя
        script_Yandex.Button_Load();
        //script_Yandex.Yandex_JS_DeviceInfo(); // узнать платформу
#endif
    }

    public void Set_UserID_text_MainMenu(string id_text) {
        UserID_text_MainMenu.text = "Ваш id: " + id_text;
    }

    // Скрывает кнопку "Загрузить сохранённую игру"
    public void Button_LoadGame_hide() {
        ButtonLoadGame.GetComponent<Button>().interactable = false;
    }

    // Показывает данные игрока в главном меню.
    public void PlayerDataShowInMainMenu() {
        string Crystal = ProgressManager.Instance.YandexDataOBJ.Crystal.ToString();
        string Oxygen = ProgressManager.Instance.YandexDataOBJ.Oxygen.ToString();
        string TechnicalContainer = ProgressManager.Instance.YandexDataOBJ.TechnicalContainer.ToString();
        string Level = ProgressManager.Instance.YandexDataOBJ.GameState.ToString();
        string Rifle_shot_pause = ProgressManager.Instance.YandexDataOBJ.DATA_time_shot_pause.ToString();
        string player_speed = ProgressManager.Instance.YandexDataOBJ.DATA_player_speed.ToString();
        string player_health = ProgressManager.Instance.YandexDataOBJ.DATA_player_health.ToString();
        string fuel = ProgressManager.Instance.YandexDataOBJ.DATA_fuel.ToString();
        string battary = ProgressManager.Instance.YandexDataOBJ.DATA_battary_level.ToString();

        string PlayerDataString = (
            "Игровой прогресс: " + "\n" +
            "Кристаллы: " + Crystal + "\n" +
            "Максимальный запас кислорода: " + Oxygen + "\n" +
            "Тех-контейнеры: " + TechnicalContainer + "\n" +
            "Текущее задание: " + Level + "\n" +
            "Время перезарядки оружия: " + Rifle_shot_pause + "\n" +
            "Скорость игрока: " + player_speed + "\n" +
            "Здоровье игрока: " + player_health + "\n" +
            "Максимальный запас топлива: " + fuel + "\n" +
            "Версия защитного поля: " + battary);

        PlayerDataText.text = PlayerDataString;
    }

    // Увеличивает скорость игрока
    public void DEV_BUTTON_speed_player() {
        ProgressManager.Instance.YandexDataOBJ.DATA_player_speed = 3.0f;
        PlayerDataShowInMainMenu();
    }

    // Увеличивает кислород игрока
    public void DEV_BUTTON_oxygen_player() {
        ProgressManager.Instance.YandexDataOBJ.Oxygen = 20000.0f;
        PlayerDataShowInMainMenu();
    }

    // Увеличивает здоровье игрока
    public void DEV_BUTTON_health_player() {
        ProgressManager.Instance.YandexDataOBJ.DATA_player_health = 100000;
        PlayerDataShowInMainMenu();
    }

    // Меняет строку с типом девайса в mainMenu
    //public void Set_Text_DeviceInfo()
    //{
    //    string str_DeviceInfo = ProgressManager.Instance.YandexDataOBJ.DeviceInfo;
    //    string str_DeviceInfo_ru = "другое";
    //    ProgressManager.Instance.YandexDataOBJ.TouchKeyboardActive = true;

    //    switch (str_DeviceInfo)
    //    {
    //        case "desktop":
    //            {
    //                str_DeviceInfo_ru = "компьютер";
    //                ProgressManager.Instance.YandexDataOBJ.TouchKeyboardActive = false;
    //                break;
    //            }
    //        case "mobile":
    //            {
    //                str_DeviceInfo_ru = "мобильное устройство";
    //                break;
    //            }
    //        case "tablet":
    //            {
    //                str_DeviceInfo_ru = "планшет";
    //                break;
    //            }
    //        case "tv":
    //            {
    //                str_DeviceInfo_ru = "телевизор";
    //                break;
    //            }
    //    }

    //    Text_DeviceInfo.text = "Платформа: " + str_DeviceInfo_ru;
    //}
}
