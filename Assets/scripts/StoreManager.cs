// Магазин

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class StoreManager : MonoBehaviour {
  [SerializeField] private GameObject GameManager; // ГЛАВНЫЙ АРХИТЕКТОР
  [SerializeField] private GameObject crystalManager;
  [SerializeField] private GameObject oxygenManager;
  [SerializeField] private GameObject TechnicalContainerManager;
  [SerializeField] private GameObject BayO2;
  [SerializeField] private GameObject Player_Rifle;

  [SerializeField] private GameObject StoreUI; // Окно магазина 

  [SerializeField] public float stepUpgradeTimeFire; // шаг улучшения перезарядки винтовки
  [SerializeField] private TextMeshProUGUI GUI_TEXT_nowTimeShotPause;
  [SerializeField] private TextMeshProUGUI GUI_TEXT_nowPole;

  private GameManager script_GameManager; // скрипт ГЛАВНОГО АРХИТЕКТОРА
  private TechnicalContainerManager SCRIPT_TechnicalContainerManager;
  private Player_Rifle SCRIPT_Player_Rifle;
  private crystalManager script_crystalManager;
  private oxygen script_oxygen;
  public bool flagStoreUIOn = true;

  [SerializeField] GameObject Button_Bay_TimeShotPause; // кнопка покупки скорости выстрелов (время между выстрелами)

  // Пауза выстрела (время между выстрелами винтовки игрока).
  // Получим из DATA хранилища 
  private float time_shot_pause;

  int poleLevel;
  public GameObject Button_Bay_Pole;

  float minimal_time_shot_pause = 0.1f;


  public fuel_manager SCRIPT_fuel_manager;
  public BatteryManager SCRIPT_BatteryManager;
  // Start is called before the first frame update

  //LOCAL
  string Store_RechargeTime;
  string Store_Sec;
  string Store_CurrentFieldLevel;
  string Store_Maximum4;
  // string Store_WeaponUpgrade;
  // string Store_FieldUpgrade;
  // string Store_BUTT_Buy;

  public
  // END LOCAL

  void Start() {
    //LOCAL LOAD
    Store_RechargeTime = TextManager.Inst_TextData.textsData.Store_RechargeTime;
    Store_Sec = TextManager.Inst_TextData.textsData.Store_Sec;
    Store_CurrentFieldLevel = TextManager.Inst_TextData.textsData.Store_CurrentFieldLevel;
    Store_Maximum4 = TextManager.Inst_TextData.textsData.Store_Maximum4;
    // Store_WeaponUpgrade = TextManager.Inst_TextData.textsData.Store_WeaponUpgrade;
    // Store_FieldUpgrade = TextManager.Inst_TextData.textsData.Store_FieldUpgrade;
    // Store_BUTT_Buy = TextManager.Inst_TextData.textsData.Store_BUTT_Buy;

    // в магазине выводим текущее время перезарядки
    time_shot_pause = ProgressManager.Instance.YandexDataOBJ.DATA_time_shot_pause;
    poleLevel = ProgressManager.Instance.YandexDataOBJ.DATA_battary_level;
    GUI_TEXT_nowTimeShotPause.text = Store_RechargeTime + time_shot_pause.ToString() + Store_Sec;
    GUI_TEXT_nowPole.text = Store_CurrentFieldLevel + poleLevel.ToString() + Store_Maximum4;

    // ссылки на скрипты:
    script_crystalManager = crystalManager.GetComponent<crystalManager>();
    script_oxygen = oxygenManager.GetComponent<oxygen>();
    script_GameManager = GameManager.GetComponent<GameManager>();
    SCRIPT_TechnicalContainerManager = TechnicalContainerManager.GetComponent<TechnicalContainerManager>();
    SCRIPT_Player_Rifle = Player_Rifle.GetComponent<Player_Rifle>();

    // при старте отключаем магазин
    StoreUI.SetActive(false);
    flagStoreUIOn = false;

    Off_Button_Bay_TimeShotPause();
    Off_Button_Bay_Pole();
  }

  // Update is called once per frame
  void Update() {
  }

  // Если оружее максимально прокачено, то отключаем кнопку прокачки орижия. 
  // Сложная проверка из-за проблем с точностью у чисел с плавующей точностью
  void Off_Button_Bay_TimeShotPause() {
    float TEMP_DATA_time_shot_pause = ProgressManager.Instance.YandexDataOBJ.DATA_time_shot_pause;
    if (Math.Abs(TEMP_DATA_time_shot_pause - minimal_time_shot_pause) < 0.00001f) {
      Button_Bay_TimeShotPause.GetComponent<Button>().interactable = false;
    }
  }

  void Off_Button_Bay_Pole() {
    if (ProgressManager.Instance.YandexDataOBJ.DATA_battary_level == 4) {
      Button_Bay_Pole.GetComponent<Button>().interactable = false;
    }
  }

  // Покупка кислорода
  public void BayOxygen() {
    if (script_crystalManager.CrystalStore >= 10) {
      script_crystalManager.CrystalStore -= 10;
      script_oxygen.oxygenTime += 5f;
      script_oxygen.oxygenTimeTemp += 5f;
      ProgressManager.Instance.YandexDataOBJ.Oxygen = script_oxygen.oxygenTime; // Сохранение данных между уровнями
      script_crystalManager.SaveDataCrystal();
      script_crystalManager.UpdateUICrystal();
      script_oxygen.UpdateUIOxygen();
      script_GameManager.Check_GameState("PlayerBayOxygen"); // Проверка состояния игры
    }
  }

  // Покупка улучшение время перезарядки винтовки игрока
  public void BuyUpgradeTimeFire() {
    int upgrade_price_techCon = 5;
    int upgrade_price_crystal = 15;
    float TEMP_DATA_time_shot_pause = ProgressManager.Instance.YandexDataOBJ.DATA_time_shot_pause;

    if (SCRIPT_TechnicalContainerManager.TechnicalContainerStore >= upgrade_price_techCon &&
        script_crystalManager.CrystalStore >= upgrade_price_crystal &&
        TEMP_DATA_time_shot_pause > minimal_time_shot_pause) {

      TEMP_DATA_time_shot_pause -= stepUpgradeTimeFire;
      ProgressManager.Instance.YandexDataOBJ.DATA_time_shot_pause = TEMP_DATA_time_shot_pause;

      SCRIPT_TechnicalContainerManager.TechnicalContainerStore -= upgrade_price_techCon;
      script_crystalManager.CrystalStore -= upgrade_price_crystal;

      SCRIPT_TechnicalContainerManager.SaveDataTechnicalContainerManager();
      SCRIPT_TechnicalContainerManager.UpdateUITechnicalContainer();
      script_crystalManager.SaveDataCrystal();
      script_crystalManager.UpdateUICrystal();

      SCRIPT_Player_Rifle.UpdateTimeFireTemp();
      GUI_TEXT_nowTimeShotPause.text = Store_RechargeTime + TEMP_DATA_time_shot_pause.ToString() + Store_Sec;

      // скрываю кнопку покупки
      if (Math.Abs(TEMP_DATA_time_shot_pause - minimal_time_shot_pause) < 0.00001f) {
        Off_Button_Bay_TimeShotPause();
      }
      script_GameManager.Check_GameState("BayFuel"); // это сохранит на сервере Яндекса
    }
  }

  public void BuyFuelSize() {
    int upgrFuelPrice_crystal = 10;
    int upgrFuelPrice_techCon = 5;
    int upgrade_fuel = 10; // преобретение
    int tmp_crystal = script_crystalManager.CrystalStore;
    int tmp_techCon = SCRIPT_TechnicalContainerManager.TechnicalContainerStore;

    if (tmp_techCon >= upgrFuelPrice_techCon && tmp_crystal >= upgrFuelPrice_crystal) {
      tmp_crystal -= upgrFuelPrice_crystal;
      tmp_techCon -= upgrFuelPrice_techCon;
      SCRIPT_fuel_manager.fuel_max += upgrade_fuel; // покупка

      script_crystalManager.CrystalStore = tmp_crystal;
      SCRIPT_TechnicalContainerManager.TechnicalContainerStore = tmp_techCon;

      SCRIPT_fuel_manager.SaveFuel();
      SCRIPT_fuel_manager.UpdateUIFuel();
      SCRIPT_TechnicalContainerManager.SaveDataTechnicalContainerManager();
      SCRIPT_TechnicalContainerManager.UpdateUITechnicalContainer();
      script_crystalManager.SaveDataCrystal();
      script_crystalManager.UpdateUICrystal();

      script_GameManager.Check_GameState("BayFuel"); // это сохранит на сервере Яндекса
    }
  }

  public void BayEnergyField() {
    int upgrPrice_crystal = 10;
    int upgrPrice_techCon = 5;
    int max_level = 4;  // максимальный уровень прокачки (0-4) 
    int tmp_BatteryManager = SCRIPT_BatteryManager.battery_level;
    int tmp_crystal = script_crystalManager.CrystalStore;
    int tmp_techCon = SCRIPT_TechnicalContainerManager.TechnicalContainerStore;

    if (tmp_techCon >= upgrPrice_techCon && tmp_crystal >= upgrPrice_crystal && tmp_BatteryManager < max_level) {
      tmp_crystal -= upgrPrice_crystal;
      tmp_techCon -= upgrPrice_techCon;


      SCRIPT_BatteryManager.battery_level++;

      script_crystalManager.CrystalStore = tmp_crystal;
      SCRIPT_TechnicalContainerManager.TechnicalContainerStore = tmp_techCon;

      SCRIPT_BatteryManager.SaveDataLevelBattery();
      SCRIPT_BatteryManager.BatteryInIt();
      SCRIPT_BatteryManager.BtteryChargeMath();
      SCRIPT_BatteryManager.UpdateDevUIBattery();

      SCRIPT_TechnicalContainerManager.SaveDataTechnicalContainerManager();
      SCRIPT_TechnicalContainerManager.UpdateUITechnicalContainer();
      script_crystalManager.SaveDataCrystal();
      script_crystalManager.UpdateUICrystal();

      poleLevel = ProgressManager.Instance.YandexDataOBJ.DATA_battary_level;
      GUI_TEXT_nowPole.text = Store_CurrentFieldLevel + poleLevel.ToString() + Store_Maximum4;
      Off_Button_Bay_Pole();

      script_GameManager.Check_GameState("BayFuel"); // это сохранит на сервере Яндекса
    }
  }

  // вкыл/выкл UI магазина
  // Дополнительно отключем "текущая миссия", что бы не пересекалось с окном магазина.
  public void StoreUIOnOff() {
    if (flagStoreUIOn) {
      StoreUI.SetActive(false);
      flagStoreUIOn = false;
    }
    else {
      StoreUI.SetActive(true);
      flagStoreUIOn = true;
      script_GameManager.Dialog_current_mission.SetActive(false);    }

  }
  
  // выкл UI магазина
  public void StoreUIOff() {
    StoreUI.SetActive(false);
  }
}
