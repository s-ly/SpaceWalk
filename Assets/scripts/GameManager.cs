using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

// ГЛАВНЫЙ АРХИТЕКТОР
public class GameManager : MonoBehaviour
{
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

    private string TextDialog_0 = "Доберитесь до базы.";
    private string TextDialog_1 = "Собери 10 Кристаллов, увеличь объём кислорода.";
    private string TextDialog_2 = "Найди Шаттл.";
    private string TextDialog_3 = "Найти топливную станцию и принести к Шаттлу.";
    private string TextDialog_4 = "Найти энергетическую станцию и принести к Шаттлу.";
    private string TextDialog_5 = "Найти станцию охраны и принести к Шаттлу.";
    private string TextDialog_6 = "Вы уже выиграли.";

    [SerializeField] Yandex script_Yandex;

    

    // Start is called before the first frame update
    void Start()
    {
        
        GameState = ProgressManager.Instance.YandexDataOBJ.GameState; // Загрузка Состояния игры        

        // доступ к скриптам игрока и менеджера кристалов
        scripc_player = Player.GetComponent<player>(); 
        script_crystalManager = crystalManager.GetComponent<crystalManager>();

        // скрипты Триггер-терминалов
        script_rigger_Terminal_key = Trigger_Terminal_key.GetComponent<Trigger_Terminal>();
        script_rigger_Terminal_fuel = Trigger_Terminal_fuel.GetComponent<Trigger_Terminal>();
        script_rigger_Terminal_energy = Trigger_Terminal_energy.GetComponent<Trigger_Terminal>();

        Dialog_Menu.SetActive(false);
        OpenDialogMission();
        BAG_Player(false, false, false); // у игрока в сумке ничего нет
        StartCoroutine(Pause_TriggerActivation(1f)); // Запуск активации трегеров с задержкой.
        Dialog_current_mission.SetActive(false);// отключаем диалое текущей цели около радара.

        
    }

    // Update is called once per frame
    void Update()
    {        
        // после смерти держим экран немного
        if (TimerDeath > 0 && DeathTriger)
        {
            TimerDeath = TimerDeath - Time.deltaTime;
            if (TimerDeath <= 0)
            {
                SceneManager.LoadScene(2);  // Загрузка GAME OVER
            }
        }
    }

    // Запуск активации трегеров с задержкой.
    // Так-как в самих трегерах есть деактивация при старте, мы ждём паузу,
    // а затем активируем если нужно.
    IEnumerator Pause_TriggerActivation(float pauseSec)
    {
        yield return new WaitForSeconds(pauseSec);
        TriggerActivation();
    }

    // Проверка состояния игры
    public void Check_GameState(string GameEvent){
        switch (GameEvent){
            case "PlayerEnterSpacePod":
            {
                // Состояние 0 - игрок в начале игры, его цель просто добещать до базы.
                if (GameState == 0)
                {
                    GameState = 1; // переводим в состояние 1
                    ProgressManager.Instance.YandexDataOBJ.GameState = GameState; // Сохранение данных между уровнями 
                    OpenDialogMission();
                }
#if UNITY_WEBGL
                script_Yandex.Button_Save();
#endif
                break;
            }
            case "PlayerBayOxygen":
            {
                // Состояние 1 - игрок должен собрать 10 кристаллов и купить увеличение кислорода.
                if (GameState == 1)
                {
                    GameState = 2; // переводим в состояние 2
                    ProgressManager.Instance.YandexDataOBJ.GameState = GameState; // Сохранение данных между уровнями
                    OpenDialogMission();
#if UNITY_WEBGL
                    script_Yandex.Button_Save();
#endif
                    }
                    break;
            }            
            case "PlayerEnter_Space_Shuttle":
            {
                // Состояние 2 - игрок ищет шатл.
                if (GameState == 2)
                {
                    GameState = 3; // переводим в состояние 
                    ProgressManager.Instance.YandexDataOBJ.GameState = GameState; // Сохранение данных между уровнями
                    OpenDialogMission();
                }                
                // Состояние 3 - игрок должен принести на базу FUEL.
                if (GameState == 3 && BAG_Player_fuel)
                {
                    GameState = 4; // переводим в состояние 
                    ProgressManager.Instance.YandexDataOBJ.GameState = GameState; // Сохранение данных между уровнями
                    BAG_Player(false, false, false); // у игрока в сумке ничего нет
                    OpenDialogMission();
                }
                // Состояние 4 - игрок должен принести на базу ENERGY.
                if (GameState == 4 && BAG_Player_energy)
                {
                    GameState = 5; // переводим в состояние 
                    ProgressManager.Instance.YandexDataOBJ.GameState = GameState; // Сохранение данных между уровнями
                    BAG_Player(false, false, false); // у игрока в сумке ничего нет
                    OpenDialogMission();
                }
                // Состояние 5 - игрок должен принести на базу KEY.
                if (GameState == 5 && BAG_Player_key)
                {
                    GameState = 6; // переводим в состояние 
                    ProgressManager.Instance.YandexDataOBJ.GameState = GameState; // Сохранение данных между уровнями
                    BAG_Player(false, false, false); // у игрока в сумке ничего нет
                    YouWin(); // конец игры
                }
                TriggerActivation();
#if UNITY_WEBGL
                script_Yandex.Button_Save();
#endif
                break;
            }
        }
    }

    // Показывает Диалог
    public void OpenDialogMission()
    {
        if (GameState == 0) 
        {
            TextDialog.text = TextDialog_0;
            Text_Dialog_current_mission.text = "Текущая цель: " + TextDialog_0;
        }
        if (GameState == 1)
        {
            TextDialog.text = TextDialog_1;
            Text_Dialog_current_mission.text = "Текущая цель: " + TextDialog_1;
        }
        if (GameState == 2)
        {
            TextDialog.text = TextDialog_2;
            Text_Dialog_current_mission.text = "Текущая цель: " + TextDialog_2;
        }
        if (GameState == 3)
        {
            TextDialog.text = TextDialog_3;
            Text_Dialog_current_mission.text = "Текущая цель: " + TextDialog_3;
        }
        if (GameState == 4)
        {
            TextDialog.text = TextDialog_4;
            Text_Dialog_current_mission.text = "Текущая цель: " + TextDialog_4;
        }
        if (GameState == 5)
        {
            TextDialog.text = TextDialog_5;
            Text_Dialog_current_mission.text = "Текущая цель: " + TextDialog_5;
        }
        if (GameState == 6)
        {
            TextDialog.text = TextDialog_6;
            Text_Dialog_current_mission.text = "Текущая цель: " + TextDialog_6;
        }
        Dialog.SetActive(true);
        Time.timeScale = 0; // Пауза
    }
    
    // Закрывает Диалог
    public void CloseDialog(){
        Dialog.SetActive(false);
        Time.timeScale = 1; // Убираем паузу
    }

    public void LoadLevel(){
        SceneManager.LoadScene(1);  // Загрузка уровня
    }

    public void LoadMainMenu(){
        SceneManager.LoadScene(0);  // Загрузка главного меню
    }

    public void LoadHelp(){
        SceneManager.LoadScene(4);  // Загрузка помощи
    }

    public void ReloadGame(){
        SceneManager.LoadScene(0);  // Загрузка игры снова (после выигрыша)
    }

    // игрок умер
    public void GameOwer(){
        script_crystalManager.SaveDataCrystal(); // записываем данные
        animatorPlayer.SetTrigger("Death"); // тригер анимации смерти
        scripc_player.enabled = false; // отключаю скрипт игрока
        DeathTriger = true; // запуск таймера перезагрузки
    }

    // Игрок победил
    public void YouWin(){
        Debug.Log("You Win!");
        SceneManager.LoadScene(3);  // Загрузка You Win!
    }

    // Открывает игру во весь экран
    public void FullScreen(){
        Screen.fullScreen = !Screen.fullScreen;
    }

    public void OpenDialog_menu()
    {
        Dialog_Menu.SetActive(true);
        Time.timeScale = 0; // Пауза
    }
    public void CloseDialog_menu()
    {
        Dialog_Menu.SetActive(false);
        Time.timeScale = 1; // Убираем паузу
    }
    
    // Качество графики
    public void QualitySet(int quality)
    {
        QualitySettings.SetQualityLevel(quality, true);
        Debug.Log("setQ " + quality.ToString());
    }

    // показывает на экране, что взял игрок
    public void BAG_Player(bool key, bool fuel, bool energy)
    {
        BAG_IMG_Player_key.SetActive(key);
        BAG_IMG_Player_fuel.SetActive(fuel);
        BAG_IMG_Player_energy.SetActive(energy);
        BAG_Player_key = key;
        BAG_Player_fuel = fuel;
        BAG_Player_energy = energy;
    }

    // Активирует тригеры.
    void TriggerActivation()
    {
        if (GameState == 3) script_rigger_Terminal_fuel.ActiveTermonal(true); // вкыл терминал FUEL
        if (GameState == 4) script_rigger_Terminal_energy.ActiveTermonal(true); // вкыл терминал ENERGY
        if (GameState == 5) script_rigger_Terminal_key.ActiveTermonal(true); // вкыл терминал KEY
    }
}