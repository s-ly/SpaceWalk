using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// ГЛАВНЫЙ АРХИТЕКТОР
public class GameManager : MonoBehaviour {
    //[DllImport("__Internal")] private static extern void JS_MyWebLog(string my_log);

    // сумка игрока
    [SerializeField] GameObject BAG_IMG_Player_key;
    [SerializeField] GameObject BAG_IMG_Player_fuel;
    [SerializeField] GameObject BAG_IMG_Player_energy;
    bool BAG_Player_key;
    bool BAG_Player_fuel;
    bool BAG_Player_energy;

    [SerializeField] private GameObject Dialog; // диалог
    [SerializeField] private GameObject Dialog_Menu;
    [SerializeField] private TextMeshProUGUI TextDialog; // текст Диалога


    [SerializeField] private GameObject Player;

    // текущая цель, около радара.
    [SerializeField] private TextMeshProUGUI Text_Dialog_current_mission;
    [SerializeField] public GameObject Dialog_current_mission;

    //[SerializeField] GameManager StoreManager;
    [SerializeField] StoreManager script_StoreManager;
    [SerializeField] GameObject StoreButton;
    [SerializeField] GameObject SpacePodZone;
    [SerializeField] GameObject Store;

    // Триггер-терминалы (для их включения)
    [SerializeField] private GameObject Trigger_Terminal_key;
    [SerializeField] private GameObject Trigger_Terminal_fuel;
    [SerializeField] private GameObject Trigger_Terminal_energy;
    Trigger_Terminal script_rigger_Terminal_key;
    Trigger_Terminal script_rigger_Terminal_fuel;
    Trigger_Terminal script_rigger_Terminal_energy;

    private player scripc_player;
    [SerializeField] private Animator animatorPlayer;
    private float TimerDeath = 1.8f;
    private bool DeathTriger = false;
    public int GameState; // Состояние игры    

    // Доступ к менеджеру кристалов
    [SerializeField] private GameObject crystalManager;
    private crystalManager script_crystalManager;

    private string TextDialog_0 = "\tЖесткая посадка, нужно выяснить, что привело к крушению. "+
        "Но сейчас другая проблема, заканчивается кислород. "+
        "Нужно срочно добежать до базы, там кислород пополняется. "+
        "\n\tДля передвижения используй клавиши AWSD, для поворота и прыжка стрелки, или сенсорную клавиатуру.";
    private string TextDialog_1 = "\tЗапас кислорода слишком мал. На этой базе можно его увеличить. "+
        "Если собирать кристаллы и приносить их на базу, можно улучшать запас. "+
        "Нужно собрать 10 кристаллов, их много рядом, принести на базу и купить улучшение. "+"" +
        "\n\tКристаллы мало собрать, их нужно донести до базы. На месте собранного кристалла, со временем появится новый.";
    private string TextDialog_2 = "\tВокруг есть кристаллы, но их слишком мало для рейдов вглубь планеты. "+
        "Тут есть множество кратеров, в них должно быть гораздо больше кристаллов. "+
        "Найди «Северный» кратер. Он рядом, и отмечен на радаре. "+
        "\n\tНужно подойти к радару, тогда появится сферическая голограмма карты. "+
        "На ней есть  маркер твоего положения и ориентации, красная стрелка. "+
        "Ещё красным пунктирным кружком отмечено местоположение текущей цели. "+
        "Голограммой можно управлять с помощью оранжевых сенсорных кнопок.";
    private string TextDialog_3 = "\tПодозрительно много охраны. Нужно выяснить, что тут происходит. "+
        "Но в начале нужно улучшить оружие, иначе не справится. "+
        "Тут должна быть база вооружения, думаю, на ней можно улучшить оружие. "+
        "\n\tПопробуй двойной прыжок. Так, если подпрыгнуть, повторное нажатие на стрелку включит реактивный ранец, "+
        "и подбросит вверх. Или можно подскочить вперёд, если нажать во время прыжка стрелку вниз. На это расходуется топливо.";
    private string TextDialog_4 = "\tСтановится всё больше боевых машин. Нужно скорее свалить с этой планеты. "+
        "При аварийной посадке, я заметил космодром. Он был недалеко от радиолокационных тарелок. "+
        "Надеюсь, там найдётся какой-нибудь транспорт. "+
        "\n\tЭти турели не очень поворотливы, но после уничтожения, какая-то ремонтная система их снова восстанавливает. "+
        "Что тут происходит?";
    private string TextDialog_5 = "\tПохоже, так просто не улететь. Этими комплексами кто-то или что-то управляет. "+
        "Что бы выяснить, придётся разведать что дальше. Мне нужно улучшить топливные баки реактивного ранца. "+
        "Нужно найти станцию реактивных двигателей, там, я смогу улучшить снаряжение. "+
        "\n\tТех-контейнеры, остающиеся после взорванных турелей тоже нужно сперва отнести на главную базу, "+
        "что бы потом покупать улучшения.";
    private string TextDialog_6 = "\tПохоже, комплекс управляется выжившей из ума системой безопасности. "+
        "Нужно скорее улетать. Но для Шаттла нужно топливо. На топливной станции нужно получить ключ доступа. "+
        "Ключ доступа нужно донести до шаттла."+
        "\n\tИщи топливные баки, там можно заправить реактивный ранец.";
    private string TextDialog_7 = "\tЭто уже не смешно, слишком опасно. Нужно улучшить свою броню, "+
        "иначе эти турели и роботы меня сотрут в порошок. Мой скафандр оснащён защитным полем. "+
        "Если я найду кибернетическую лабораторию, я смогу улучшить его. "+
        "\n\tЗащитное поле тратится, когда в него попадают выстрелы, и так защищает меня. "+
        "Поле само медленно восстанавливается, но только если выйти из боя.";
    private string TextDialog_8 = "\tДля шаттла нужна энергия. Ключ доступа должен быть на электростанции. Нужно найти её."+
        "Ключ доступа нужно донести до шаттла. " +
        "\n\tНельзя просто так улететь, пострадает ещё кто-то. Нужно найти способ остановить безумную систему.";
    private string TextDialog_9 = "\tНа электростанции я выяснил, в южной части планеты расположен боевой комплекс, "+
        "он и сбил мой корабль. Его нужно отключить. Комплекс питают 3 реактора, нужно уничтожить их все." + 
        "\n\tНа карте отмечены три точки, где расположены реакторы. Там полно охраны!";
    private string TextDialog_10 = "\tВсе три реактора уничтожены, и защита комплекса ослабла. "+
        "Осталось найти ключ доступа охранной системы и добежать до шаттла." +
        "\n\tСтранно, что никто не управляет станциями, что привело их в ярость, и почему они нападают? "+
        "Нет времени думать, нужно хватать ключ и сматываться. ";
    private string TextDialog_11 = "\tПохоже, кто-то специально запрограммировал модули охраны на нападение. "+
        "Кто и зачем? Это уже меня не касается. С меня хватило. "+
        "\n\tПобеда!";

    [SerializeField] Yandex script_Yandex;

    // какие батареи уничтоженны
    bool DestroyBatteryNum_1 = false;
    bool DestroyBatteryNum_2 = false;
    bool DestroyBatteryNum_3 = false;
    public bool DestroyBatteryAll = false;

    // перекрытие UI
    public EngineBase SCRIPT_EngineBase; 
    public LabBase SRC_LabBase;
    public WeaponsBase STC_WeaponsBase;


    public GameObject EnergyBattery_1;
    public GameObject EnergyBattery_2;
    public GameObject EnergyBattery_3;
    EnergyBattery SRC_EnergyBattery_1;
    EnergyBattery SRC_EnergyBattery_2;
    EnergyBattery SRC_EnergyBattery_3;

    // Start is called before the first frame update
    void Start() {
        GameState = ProgressManager.Instance.YandexDataOBJ.GameState; // Загрузка Состояния игры
        //TouchKeyboardActive = ProgressManager.Instance.YandexDataOBJ.TouchKeyboardActive;

        // доступ к скриптам игрока и менеджера кристалов
        scripc_player = Player.GetComponent<player>();
        script_crystalManager = crystalManager.GetComponent<crystalManager>();

        // скрипты Триггер-терминалов
        script_rigger_Terminal_key = Trigger_Terminal_key.GetComponent<Trigger_Terminal>();
        script_rigger_Terminal_fuel = Trigger_Terminal_fuel.GetComponent<Trigger_Terminal>();
        script_rigger_Terminal_energy = Trigger_Terminal_energy.GetComponent<Trigger_Terminal>();

        SRC_EnergyBattery_1 = EnergyBattery_1.GetComponent<EnergyBattery>();
        SRC_EnergyBattery_2 = EnergyBattery_2.GetComponent<EnergyBattery>();
        SRC_EnergyBattery_3 = EnergyBattery_3.GetComponent<EnergyBattery>();



        Dialog_Menu.SetActive(false);
        OpenDialogMission();
        BAG_Player(false, false, false); // у игрока в сумке ничего нет
        StartCoroutine(Pause_TriggerActivation(1f)); // Запуск активации трегеров с задержкой.
        Dialog_current_mission.SetActive(false);// отключаем диалое текущей цели около радара.



        //dev_text = ProgressManager.Instance.YandexDataOBJ.DeviceInfo;
        //JS_MyWebLog(TouchKeyboardActive.ToString());
        //touch_keyboard_obj.SetActive(TouchKeyboardActive);
    }

    // Update is called once per frame
    void Update() {
        // после смерти держим экран немного
        if (TimerDeath > 0 && DeathTriger) {
            TimerDeath = TimerDeath - Time.deltaTime;
            if (TimerDeath <= 0) {
                SceneManager.LoadScene(2);  // Загрузка GAME OVER
                script_Yandex.ShowAdv();
            }
        }
    }

    // Запуск активации трегеров с задержкой.
    // Так-как в самих трегерах есть деактивация при старте, мы ждём паузу,
    // а затем активируем если нужно.
    IEnumerator Pause_TriggerActivation(float pauseSec) {
        yield return new WaitForSeconds(pauseSec);
        TriggerActivation();
    }

    // Проверка состояния игры
    public void Check_GameState(string GameEvent) {
        switch (GameEvent) {
            case "BayFuel": {
#if UNITY_WEBGL
                    script_Yandex.Button_Save();
#endif
                    break;
                }
            case "PlayerEnterSpacePod": {
                    // Состояние 0 - игрок в начале игры, его цель просто добежать до базы.
                    if (GameState == 0) {
                        GameState = 1; 
                        ProgressManager.Instance.YandexDataOBJ.GameState = GameState; // Сохранение данных между уровнями 
                        OpenDialogMission();
                    }
#if UNITY_WEBGL
                    script_Yandex.Button_Save();
#endif
                    break;
                }
            case "PlayerBayOxygen": {
                    // Состояние 1 - игрок должен собрать 10 кристаллов и купить увеличение кислорода.
                    if (GameState == 1) {
                        GameState = 2; 
                        ProgressManager.Instance.YandexDataOBJ.GameState = GameState; // Сохранение данных между уровнями
                        OpenDialogMission();
#if UNITY_WEBGL
                        script_Yandex.Button_Save();
#endif
                    }
                    break;
                }
            case "NorthCrater": {
                    // Состояние 2 - игрок должен найти Северный кратер.
                    if (GameState == 2) {
                        GameState = 3; 
                        ProgressManager.Instance.YandexDataOBJ.GameState = GameState; // Сохранение данных между уровнями
                        OpenDialogMission();
#if UNITY_WEBGL
                        script_Yandex.Button_Save();
#endif
                    }
                    break;
                }
            case "Weapons_Base": {
                    // Состояние 2 - игрок должен найти Северный кратер.
                    if (GameState == 3) {
                        GameState = 4; 
                        ProgressManager.Instance.YandexDataOBJ.GameState = GameState; // Сохранение данных между уровнями
                        OpenDialogMission();
#if UNITY_WEBGL
                        script_Yandex.Button_Save();
#endif
                    }
                    break;
                }
            case "Engine_base": {
                    // Состояние 5 - игрок должен найти двигатели.
                    if (GameState == 5) {
                        GameState = 6; 
                        ProgressManager.Instance.YandexDataOBJ.GameState = GameState; // Сохранение данных между уровнями
                        OpenDialogMission();
                        TriggerActivation();
#if UNITY_WEBGL
                        script_Yandex.Button_Save();
#endif
                    }
                    break;
                }
            case "Lab_base": {
                    // Состояние 7 - игрок должен найти Поле.
                    if (GameState == 7) {
                        GameState = 8; 
                        ProgressManager.Instance.YandexDataOBJ.GameState = GameState; // Сохранение данных между уровнями
                        OpenDialogMission();
                        TriggerActivation();
                        
#if UNITY_WEBGL
                        script_Yandex.Button_Save();
#endif
                    }
                    break;
                }
            case "Battery": {
                    // Состояние 9 - игрок должен уничтожить 3 батареи.
                    if (GameState == 9 && DestroyBatteryAll) {                        
                        GameState = 10; 
                        ProgressManager.Instance.YandexDataOBJ.GameState = GameState; // Сохранение данных между уровнями
                        OpenDialogMission();
                        TriggerActivation();
                        
#if UNITY_WEBGL
                        script_Yandex.Button_Save();
#endif
                    }
                    break;
                }
            case "PlayerEnter_Space_Shuttle": {
                    // Состояние 4 - игрок ищет шатл.
                    if (GameState == 4) {
                        GameState = 5;  
                        ProgressManager.Instance.YandexDataOBJ.GameState = GameState; // Сохранение данных между уровнями
                        OpenDialogMission();
                    }

                    // Состояние 6 - игрок должен принести на базу FUEL.
                    if (GameState == 6 && BAG_Player_fuel) {
                        GameState = 7;  
                        ProgressManager.Instance.YandexDataOBJ.GameState = GameState; // Сохранение данных между уровнями
                        BAG_Player(false, false, false); // у игрока в сумке ничего нет
                        OpenDialogMission();
                    }
                    // Состояние 8 - игрок должен принести на базу ENERGY.
                    if (GameState == 8 && BAG_Player_energy) {
                        GameState = 9; 
                        ProgressManager.Instance.YandexDataOBJ.GameState = GameState; // Сохранение данных между уровнями
                        BAG_Player(false, false, false); // у игрока в сумке ничего нет
                        SRC_EnergyBattery_1.ActivationBattery();
                        SRC_EnergyBattery_2.ActivationBattery();
                        SRC_EnergyBattery_3.ActivationBattery();
                        OpenDialogMission();
                    }
                    // Состояние 10 - игрок должен принести на базу KEY.
                    if (GameState == 10 && BAG_Player_key) {
                        GameState = 11; 
                        ProgressManager.Instance.YandexDataOBJ.GameState = GameState; // Сохранение данных между уровнями
                        BAG_Player(false, false, false); // у игрока в сумке ничего нет
                        YouWin(); // конец игры
                    }
                    //TriggerActivation();
#if UNITY_WEBGL
                    script_Yandex.Button_Save();
#endif
                    break;
                }
        }
    }

    // Показывает Диалог
    public void OpenDialogMission() {
        if (GameState == 0) {
            TextDialog.text = TextDialog_0;
            Text_Dialog_current_mission.text = "Текущая цель: " + TextDialog_0;
        }
        if (GameState == 1) {
            TextDialog.text = TextDialog_1;
            Text_Dialog_current_mission.text = "Текущая цель: " + TextDialog_1;
        }
        if (GameState == 2) {
            TextDialog.text = TextDialog_2;
            Text_Dialog_current_mission.text = "Текущая цель: " + TextDialog_2;
        }
        if (GameState == 3) {
            TextDialog.text = TextDialog_3;
            Text_Dialog_current_mission.text = "Текущая цель: " + TextDialog_3;
        }
        if (GameState == 4) {
            TextDialog.text = TextDialog_4;
            Text_Dialog_current_mission.text = "Текущая цель: " + TextDialog_4;
        }
        if (GameState == 5) {
            TextDialog.text = TextDialog_5;
            Text_Dialog_current_mission.text = "Текущая цель: " + TextDialog_5;
        }
        if (GameState == 6) {
            TextDialog.text = TextDialog_6;
            Text_Dialog_current_mission.text = "Текущая цель: " + TextDialog_6;
        }
        if (GameState == 7) {
            TextDialog.text = TextDialog_7;
            Text_Dialog_current_mission.text = "Текущая цель: " + TextDialog_7;
        }
        if (GameState == 8) {
            TextDialog.text = TextDialog_8;
            Text_Dialog_current_mission.text = "Текущая цель: " + TextDialog_8;
        }
        if (GameState == 9) {
            TextDialog.text = TextDialog_9;
            Text_Dialog_current_mission.text = "Текущая цель: " + TextDialog_9;
        }
        if (GameState == 10) {
            TextDialog.text = TextDialog_10;
            Text_Dialog_current_mission.text = "Текущая цель: " + TextDialog_10;
        }
        if (GameState == 11) {
            TextDialog.text = TextDialog_11;
            Text_Dialog_current_mission.text = "Текущая цель: " + TextDialog_11;
        }
        Dialog.SetActive(true);
        if (StoreButton.activeSelf) StoreButton.SetActive(false); // скрыть кнопку магазина
        if (Store.activeSelf) {
            Store.SetActive(false); // скрыть Магазин
            script_StoreManager.flagStoreUIOn = false; // значит сам магазин не открыт
        }

        Time.timeScale = 0; // Пауза
    }

    // Закрывает Диалог
    public void CloseDialog() {
        Dialog.SetActive(false);
        if (SpacePodZone.activeSelf) StoreButton.SetActive(true); // показать кнопку магазина
        Time.timeScale = 1; // Убираем паузу
    }

    public void LoadLevel() {
        SceneManager.LoadScene(1);  // Загрузка уровня
    }

    public void LoadMainMenu() {
        SceneManager.LoadScene(0);  // Загрузка главного меню
    }

    public void LoadHelp() {
        SceneManager.LoadScene(4);  // Загрузка помощи
    }

    public void ReloadGame() {
        SceneManager.LoadScene(0);  // Загрузка игры снова (после выигрыша)
    }

    // игрок умер
    public void GameOwer() {
        script_crystalManager.SaveDataCrystal(); // записываем данные
        animatorPlayer.SetTrigger("Death"); // тригер анимации смерти
        scripc_player.enabled = false; // отключаю скрипт игрока
        DeathTriger = true; // запуск таймера перезагрузки
    }

    // Игрок победил
    public void YouWin() {
        Debug.Log("You Win!");
        SceneManager.LoadScene(3);  // Загрузка You Win!
    }

    // Открывает игру во весь экран
    public void FullScreen() {
        Screen.fullScreen = !Screen.fullScreen;
    }

    public void OpenDialog_menu() {
        Dialog_Menu.SetActive(true);
        Dialog.SetActive(false);
        Store.SetActive(false);
        StoreButton.SetActive(false);
        script_StoreManager.flagStoreUIOn = false; // значит сам магазин не открыт
        Dialog_current_mission.SetActive(false);
        SCRIPT_EngineBase.SwitchActive(); // для предотвращение перекрытия ui
        SRC_LabBase.SwitchActive();
        STC_WeaponsBase.SwitchActive();
        Time.timeScale = 0; // Пауза
    }
    public void CloseDialog_menu() {
        Dialog_Menu.SetActive(false);
        if (SpacePodZone.activeSelf) StoreButton.SetActive(true); // показать кнопку магазина
        SCRIPT_EngineBase.SwitchActive(); // для предотвращение перекрытия ui
        SRC_LabBase.SwitchActive();
        STC_WeaponsBase.SwitchActive();
        Time.timeScale = 1; // Убираем паузу
    }

    // Качество графики
    public void QualitySet(int quality) {
        QualitySettings.SetQualityLevel(quality, true);
        Debug.Log("setQ " + quality.ToString());
    }

    // показывает на экране, что взял игрок
    public void BAG_Player(bool key, bool fuel, bool energy) {
        BAG_IMG_Player_key.SetActive(key);
        BAG_IMG_Player_fuel.SetActive(fuel);
        BAG_IMG_Player_energy.SetActive(energy);
        BAG_Player_key = key;
        BAG_Player_fuel = fuel;
        BAG_Player_energy = energy;
    }

    // Активирует тригеры.
    void TriggerActivation() {
        if (GameState == 6) script_rigger_Terminal_fuel.ActiveTermonal(true); // вкыл терминал FUEL
        if (GameState == 8) script_rigger_Terminal_energy.ActiveTermonal(true); // вкыл терминал ENERGY
        if (GameState == 10) script_rigger_Terminal_key.ActiveTermonal(true); // вкыл терминал KEY
    }

    // Проверяет, какая батарея (из 3-ох) уничтоженна
    public void CheckDestroyBatteryNum(int numBattery) {
        if (numBattery == 1) DestroyBatteryNum_1 = true;
        if (numBattery == 2) DestroyBatteryNum_2 = true;
        if (numBattery == 3) DestroyBatteryNum_3 = true;
        if (DestroyBatteryNum_1 && DestroyBatteryNum_2 && DestroyBatteryNum_3) DestroyBatteryAll = true;
    }

}