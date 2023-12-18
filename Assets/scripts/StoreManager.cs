// �������

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StoreManager : MonoBehaviour {
    [SerializeField] private GameObject GameManager; // ������� ����������
    [SerializeField] private GameObject crystalManager;
    [SerializeField] private GameObject oxygenManager;
    [SerializeField] private GameObject TechnicalContainerManager;
    [SerializeField] private GameObject BayO2;
    [SerializeField] private GameObject Player_Rifle;
    [SerializeField] private GameObject StoreUI; // ���� ��������
    [SerializeField] public float stepUpgradeTimeFire; // ��� ��������� ����������� ��������
    [SerializeField] private TextMeshProUGUI GUI_TEXT_nowTimeShotPause;

    private GameManager script_GameManager; // ������ �������� �����������
    private TechnicalContainerManager SCRIPT_TechnicalContainerManager;
    private Player_Rifle SCRIPT_Player_Rifle;
    private crystalManager script_crystalManager;
    private oxygen script_oxygen;
    public bool flagStoreUIOn = true;

    [SerializeField] GameObject Button_Bay_TimeShotPause; // ������ ������� �������� ��������� (����� ����� ����������)

    // ����� �������� (����� ����� ���������� �������� ������).
    // ������� �� DATA ��������� 
    private float time_shot_pause;

    float minimal_time_shot_pause = 0.1f;



    public fuel_manager SCRIPT_fuel_manager;
    public BatteryManager SCRIPT_BatteryManager;
    // Start is called before the first frame update
    void Start() {
        // � �������� ������� ������� ����� �����������
        time_shot_pause = ProgressManager.Instance.YandexDataOBJ.DATA_time_shot_pause;
        GUI_TEXT_nowTimeShotPause.text = "����� �����������: " + time_shot_pause.ToString() + " ���";

        // ������ �� �������:
        script_crystalManager = crystalManager.GetComponent<crystalManager>();
        script_oxygen = oxygenManager.GetComponent<oxygen>();
        script_GameManager = GameManager.GetComponent<GameManager>();
        SCRIPT_TechnicalContainerManager = TechnicalContainerManager.GetComponent<TechnicalContainerManager>();
        SCRIPT_Player_Rifle = Player_Rifle.GetComponent<Player_Rifle>();

        // ��� ������ ��������� �������
        StoreUI.SetActive(false);
        flagStoreUIOn = false;

        Off_Button_Bay_TimeShotPause();
    }

    // Update is called once per frame
    void Update() {

    }

    // ���� ������ ����������� ���������, �� ��������� ������ �������� ������. 
    void Off_Button_Bay_TimeShotPause() {
        if (ProgressManager.Instance.YandexDataOBJ.DATA_time_shot_pause == minimal_time_shot_pause) {
            Button_Bay_TimeShotPause.GetComponent<Button>().interactable = false;
        }
    }

    // ������� ���������
    public void BayOxygen() {
        if (script_crystalManager.CrystalStore >= 10) {
            script_crystalManager.CrystalStore -= 10;
            script_oxygen.oxygenTime += 5f;
            script_oxygen.oxygenTimeTemp += 5f;
            ProgressManager.Instance.YandexDataOBJ.Oxygen = script_oxygen.oxygenTime; // ���������� ������ ����� ��������
            script_crystalManager.SaveDataCrystal();
            script_crystalManager.UpdateUICrystal();
            script_oxygen.UpdateUIOxygen();
            script_GameManager.Check_GameState("PlayerBayOxygen"); // �������� ��������� ����
        }

    }

    // ������� ��������� ����� ����������� �������� ������
    public void BuyUpgradeTimeFire() {
        int upgrade_price = 5;
        //float minimal_time_shot_pause = 0.1f;
        float TEMP_DATA_time_shot_pause = ProgressManager.Instance.YandexDataOBJ.DATA_time_shot_pause;

        if (SCRIPT_TechnicalContainerManager.TechnicalContainerStore >= upgrade_price) {
            if (TEMP_DATA_time_shot_pause != minimal_time_shot_pause) {
                TEMP_DATA_time_shot_pause -= stepUpgradeTimeFire;
                if (TEMP_DATA_time_shot_pause < minimal_time_shot_pause) {
                    TEMP_DATA_time_shot_pause = minimal_time_shot_pause;
                }
                ProgressManager.Instance.YandexDataOBJ.DATA_time_shot_pause = TEMP_DATA_time_shot_pause;
                SCRIPT_TechnicalContainerManager.TechnicalContainerStore -= upgrade_price;
                SCRIPT_TechnicalContainerManager.SaveDataTechnicalContainerManager();
                SCRIPT_TechnicalContainerManager.UpdateUITechnicalContainer();
                SCRIPT_Player_Rifle.UpdateTimeFireTemp();
                GUI_TEXT_nowTimeShotPause.text = "����� �����������: " + TEMP_DATA_time_shot_pause.ToString() + " ���";
                Off_Button_Bay_TimeShotPause();
            }
        }
    }

    public void BuyFuelSize() {
        int upgrFuelPrice_crystal = 10;
        int upgrFuelPrice_techCon = 5;
        int upgrade_fuel = 10; // ������������
        int tmp_crystal = script_crystalManager.CrystalStore;
        int tmp_techCon = SCRIPT_TechnicalContainerManager.TechnicalContainerStore;

        if (tmp_techCon >= upgrFuelPrice_techCon && tmp_crystal >= upgrFuelPrice_crystal) {
            tmp_crystal -= upgrFuelPrice_crystal;
            tmp_techCon -= upgrFuelPrice_techCon;
            SCRIPT_fuel_manager.fuel_max += upgrade_fuel; // �������

            script_crystalManager.CrystalStore = tmp_crystal;
            SCRIPT_TechnicalContainerManager.TechnicalContainerStore = tmp_techCon;

            SCRIPT_fuel_manager.SaveFuel();
            SCRIPT_fuel_manager.UpdateUIFuel();
            SCRIPT_TechnicalContainerManager.SaveDataTechnicalContainerManager();
            SCRIPT_TechnicalContainerManager.UpdateUITechnicalContainer();
            script_crystalManager.SaveDataCrystal();
            script_crystalManager.UpdateUICrystal();

            script_GameManager.Check_GameState("BayFuel"); // ��� �������� �� ������� �������

        }
    }

    public void BayEnergyField() {
        int upgrPrice_crystal = 10;
        int upgrPrice_techCon = 5;
        int max_level = 4;  // ������������ ������� �������� (0-4) 
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

            script_GameManager.Check_GameState("BayFuel"); // ��� �������� �� ������� �������


        }
    }


    // ����/���� UI ��������
    // ������������� �������� "������� ������", ��� �� �� ������������ � ����� ��������.
    public void StoreUIOnOff() {
        if (flagStoreUIOn) {
            StoreUI.SetActive(false);
            flagStoreUIOn = false;
        }
        else {
            StoreUI.SetActive(true);
            flagStoreUIOn = true;
            script_GameManager.Dialog_current_mission.SetActive(false);
        }

    }
    // ���� UI ��������
    public void StoreUIOff() {
        StoreUI.SetActive(false);
    }
}
