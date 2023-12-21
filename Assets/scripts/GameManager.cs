using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// ������� ����������
public class GameManager : MonoBehaviour {
    //[DllImport("__Internal")] private static extern void JS_MyWebLog(string my_log);

    // ����� ������
    [SerializeField] GameObject BAG_IMG_Player_key;
    [SerializeField] GameObject BAG_IMG_Player_fuel;
    [SerializeField] GameObject BAG_IMG_Player_energy;
    bool BAG_Player_key;
    bool BAG_Player_fuel;
    bool BAG_Player_energy;

    [SerializeField] private GameObject Dialog; // ������
    [SerializeField] private GameObject Dialog_Menu;
    [SerializeField] private TextMeshProUGUI TextDialog; // ����� �������


    [SerializeField] private GameObject Player;

    // ������� ����, ����� ������.
    [SerializeField] private TextMeshProUGUI Text_Dialog_current_mission;
    [SerializeField] public GameObject Dialog_current_mission;

    //[SerializeField] GameManager StoreManager;
    [SerializeField] StoreManager script_StoreManager;
    [SerializeField] GameObject StoreButton;
    [SerializeField] GameObject SpacePodZone;
    [SerializeField] GameObject Store;

    // �������-��������� (��� �� ���������)
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
    public int GameState; // ��������� ����    

    // ������ � ��������� ���������
    [SerializeField] private GameObject crystalManager;
    private crystalManager script_crystalManager;

    private string TextDialog_0 = "���������� �� ����. " +
        "���������� �� �������.";
    private string TextDialog_1 = "������ 10 ����������, ������� ����� ���������.";
    private string TextDialog_2 = "������ � ����������� (�������� ������).";
    private string TextDialog_3 = "����� ������� ������.";
    private string TextDialog_4 = "����� �����.";
    private string TextDialog_5 = "����� ������� ����������.";
    private string TextDialog_6 = "����� ��������� ������� � �������� � ������.";
    private string TextDialog_7 = "����� ������� ����.";
    private string TextDialog_8 = "����� �������������� ������� � �������� � ������.";
    private string TextDialog_9 = "���������� 3 ��������.";
    private string TextDialog_10 = "����� ������� ������ � �������� � ������.";
    private string TextDialog_11 = "�� ��� ��������.";

    [SerializeField] Yandex script_Yandex;

    // ����� ������� �����������
    bool DestroyBatteryNum_1 = false;
    bool DestroyBatteryNum_2 = false;
    bool DestroyBatteryNum_3 = false;
    public bool DestroyBatteryAll = false; 

    public EngineBase SCRIPT_EngineBase; // ���������� UI

    public GameObject EnergyBattery_1;
    public GameObject EnergyBattery_2;
    public GameObject EnergyBattery_3;
    EnergyBattery SRC_EnergyBattery_1;
    EnergyBattery SRC_EnergyBattery_2;
    EnergyBattery SRC_EnergyBattery_3;

    // Start is called before the first frame update
    void Start() {
        GameState = ProgressManager.Instance.YandexDataOBJ.GameState; // �������� ��������� ����
        //TouchKeyboardActive = ProgressManager.Instance.YandexDataOBJ.TouchKeyboardActive;

        // ������ � �������� ������ � ��������� ���������
        scripc_player = Player.GetComponent<player>();
        script_crystalManager = crystalManager.GetComponent<crystalManager>();

        // ������� �������-����������
        script_rigger_Terminal_key = Trigger_Terminal_key.GetComponent<Trigger_Terminal>();
        script_rigger_Terminal_fuel = Trigger_Terminal_fuel.GetComponent<Trigger_Terminal>();
        script_rigger_Terminal_energy = Trigger_Terminal_energy.GetComponent<Trigger_Terminal>();

        SRC_EnergyBattery_1 = EnergyBattery_1.GetComponent<EnergyBattery>();
        SRC_EnergyBattery_2 = EnergyBattery_2.GetComponent<EnergyBattery>();
        SRC_EnergyBattery_3 = EnergyBattery_3.GetComponent<EnergyBattery>();



        Dialog_Menu.SetActive(false);
        OpenDialogMission();
        BAG_Player(false, false, false); // � ������ � ����� ������ ���
        StartCoroutine(Pause_TriggerActivation(1f)); // ������ ��������� �������� � ���������.
        Dialog_current_mission.SetActive(false);// ��������� ������ ������� ���� ����� ������.



        //dev_text = ProgressManager.Instance.YandexDataOBJ.DeviceInfo;
        //JS_MyWebLog(TouchKeyboardActive.ToString());
        //touch_keyboard_obj.SetActive(TouchKeyboardActive);
    }

    // Update is called once per frame
    void Update() {
        // ����� ������ ������ ����� �������
        if (TimerDeath > 0 && DeathTriger) {
            TimerDeath = TimerDeath - Time.deltaTime;
            if (TimerDeath <= 0) {
                SceneManager.LoadScene(2);  // �������� GAME OVER
                script_Yandex.ShowAdv();
            }
        }
    }

    // ������ ��������� �������� � ���������.
    // ���-��� � ����� �������� ���� ����������� ��� ������, �� ��� �����,
    // � ����� ���������� ���� �����.
    IEnumerator Pause_TriggerActivation(float pauseSec) {
        yield return new WaitForSeconds(pauseSec);
        TriggerActivation();
    }

    // �������� ��������� ����
    public void Check_GameState(string GameEvent) {
        switch (GameEvent) {
            case "BayFuel": {
#if UNITY_WEBGL
                    script_Yandex.Button_Save();
#endif
                    break;
                }
            case "PlayerEnterSpacePod": {
                    // ��������� 0 - ����� � ������ ����, ��� ���� ������ �������� �� ����.
                    if (GameState == 0) {
                        GameState = 1; 
                        ProgressManager.Instance.YandexDataOBJ.GameState = GameState; // ���������� ������ ����� �������� 
                        OpenDialogMission();
                    }
#if UNITY_WEBGL
                    script_Yandex.Button_Save();
#endif
                    break;
                }
            case "PlayerBayOxygen": {
                    // ��������� 1 - ����� ������ ������� 10 ���������� � ������ ���������� ���������.
                    if (GameState == 1) {
                        GameState = 2; 
                        ProgressManager.Instance.YandexDataOBJ.GameState = GameState; // ���������� ������ ����� ��������
                        OpenDialogMission();
#if UNITY_WEBGL
                        script_Yandex.Button_Save();
#endif
                    }
                    break;
                }
            case "NorthCrater": {
                    // ��������� 2 - ����� ������ ����� �������� ������.
                    if (GameState == 2) {
                        GameState = 3; 
                        ProgressManager.Instance.YandexDataOBJ.GameState = GameState; // ���������� ������ ����� ��������
                        OpenDialogMission();
#if UNITY_WEBGL
                        script_Yandex.Button_Save();
#endif
                    }
                    break;
                }
            case "Weapons_Base": {
                    // ��������� 2 - ����� ������ ����� �������� ������.
                    if (GameState == 3) {
                        GameState = 4; 
                        ProgressManager.Instance.YandexDataOBJ.GameState = GameState; // ���������� ������ ����� ��������
                        OpenDialogMission();
#if UNITY_WEBGL
                        script_Yandex.Button_Save();
#endif
                    }
                    break;
                }
            case "Engine_base": {
                    // ��������� 5 - ����� ������ ����� ���������.
                    if (GameState == 5) {
                        GameState = 6; 
                        ProgressManager.Instance.YandexDataOBJ.GameState = GameState; // ���������� ������ ����� ��������
                        OpenDialogMission();
                        TriggerActivation();
#if UNITY_WEBGL
                        script_Yandex.Button_Save();
#endif
                    }
                    break;
                }
            case "Lab_base": {
                    // ��������� 7 - ����� ������ ����� ����.
                    if (GameState == 7) {
                        GameState = 8; 
                        ProgressManager.Instance.YandexDataOBJ.GameState = GameState; // ���������� ������ ����� ��������
                        OpenDialogMission();
                        TriggerActivation();
                        
#if UNITY_WEBGL
                        script_Yandex.Button_Save();
#endif
                    }
                    break;
                }
            case "Battery": {
                    // ��������� 9 - ����� ������ ���������� 3 �������.
                    if (GameState == 9 && DestroyBatteryAll) {                        
                        GameState = 10; 
                        ProgressManager.Instance.YandexDataOBJ.GameState = GameState; // ���������� ������ ����� ��������
                        OpenDialogMission();
                        TriggerActivation();
                        
#if UNITY_WEBGL
                        script_Yandex.Button_Save();
#endif
                    }
                    break;
                }
            case "PlayerEnter_Space_Shuttle": {
                    // ��������� 4 - ����� ���� ����.
                    if (GameState == 4) {
                        GameState = 5;  
                        ProgressManager.Instance.YandexDataOBJ.GameState = GameState; // ���������� ������ ����� ��������
                        OpenDialogMission();
                    }

                    // ��������� 6 - ����� ������ �������� �� ���� FUEL.
                    if (GameState == 6 && BAG_Player_fuel) {
                        GameState = 7;  
                        ProgressManager.Instance.YandexDataOBJ.GameState = GameState; // ���������� ������ ����� ��������
                        BAG_Player(false, false, false); // � ������ � ����� ������ ���
                        OpenDialogMission();
                    }
                    // ��������� 8 - ����� ������ �������� �� ���� ENERGY.
                    if (GameState == 8 && BAG_Player_energy) {
                        GameState = 9; 
                        ProgressManager.Instance.YandexDataOBJ.GameState = GameState; // ���������� ������ ����� ��������
                        BAG_Player(false, false, false); // � ������ � ����� ������ ���
                        SRC_EnergyBattery_1.ActivationBattery();
                        SRC_EnergyBattery_2.ActivationBattery();
                        SRC_EnergyBattery_3.ActivationBattery();
                        OpenDialogMission();
                    }
                    // ��������� 10 - ����� ������ �������� �� ���� KEY.
                    if (GameState == 10 && BAG_Player_key) {
                        GameState = 11; 
                        ProgressManager.Instance.YandexDataOBJ.GameState = GameState; // ���������� ������ ����� ��������
                        BAG_Player(false, false, false); // � ������ � ����� ������ ���
                        YouWin(); // ����� ����
                    }
                    //TriggerActivation();
#if UNITY_WEBGL
                    script_Yandex.Button_Save();
#endif
                    break;
                }
        }
    }

    // ���������� ������
    public void OpenDialogMission() {
        if (GameState == 0) {
            TextDialog.text = TextDialog_0;
            Text_Dialog_current_mission.text = "������� ����: " + TextDialog_0;
        }
        if (GameState == 1) {
            TextDialog.text = TextDialog_1;
            Text_Dialog_current_mission.text = "������� ����: " + TextDialog_1;
        }
        if (GameState == 2) {
            TextDialog.text = TextDialog_2;
            Text_Dialog_current_mission.text = "������� ����: " + TextDialog_2;
        }
        if (GameState == 3) {
            TextDialog.text = TextDialog_3;
            Text_Dialog_current_mission.text = "������� ����: " + TextDialog_3;
        }
        if (GameState == 4) {
            TextDialog.text = TextDialog_4;
            Text_Dialog_current_mission.text = "������� ����: " + TextDialog_4;
        }
        if (GameState == 5) {
            TextDialog.text = TextDialog_5;
            Text_Dialog_current_mission.text = "������� ����: " + TextDialog_5;
        }
        if (GameState == 6) {
            TextDialog.text = TextDialog_6;
            Text_Dialog_current_mission.text = "������� ����: " + TextDialog_6;
        }
        if (GameState == 7) {
            TextDialog.text = TextDialog_7;
            Text_Dialog_current_mission.text = "������� ����: " + TextDialog_7;
        }
        if (GameState == 8) {
            TextDialog.text = TextDialog_8;
            Text_Dialog_current_mission.text = "������� ����: " + TextDialog_8;
        }
        if (GameState == 9) {
            TextDialog.text = TextDialog_9;
            Text_Dialog_current_mission.text = "������� ����: " + TextDialog_9;
        }
        if (GameState == 10) {
            TextDialog.text = TextDialog_10;
            Text_Dialog_current_mission.text = "������� ����: " + TextDialog_10;
        }
        if (GameState == 11) {
            TextDialog.text = TextDialog_11;
            Text_Dialog_current_mission.text = "������� ����: " + TextDialog_11;
        }
        Dialog.SetActive(true);
        if (StoreButton.activeSelf) StoreButton.SetActive(false); // ������ ������ ��������
        if (Store.activeSelf) {
            Store.SetActive(false); // ������ �������
            script_StoreManager.flagStoreUIOn = false; // ������ ��� ������� �� ������
        }

        Time.timeScale = 0; // �����
    }

    // ��������� ������
    public void CloseDialog() {
        Dialog.SetActive(false);
        if (SpacePodZone.activeSelf) StoreButton.SetActive(true); // �������� ������ ��������
        Time.timeScale = 1; // ������� �����
    }

    public void LoadLevel() {
        SceneManager.LoadScene(1);  // �������� ������
    }

    public void LoadMainMenu() {
        SceneManager.LoadScene(0);  // �������� �������� ����
    }

    public void LoadHelp() {
        SceneManager.LoadScene(4);  // �������� ������
    }

    public void ReloadGame() {
        SceneManager.LoadScene(0);  // �������� ���� ����� (����� ��������)
    }

    // ����� ����
    public void GameOwer() {
        script_crystalManager.SaveDataCrystal(); // ���������� ������
        animatorPlayer.SetTrigger("Death"); // ������ �������� ������
        scripc_player.enabled = false; // �������� ������ ������
        DeathTriger = true; // ������ ������� ������������
    }

    // ����� �������
    public void YouWin() {
        Debug.Log("You Win!");
        SceneManager.LoadScene(3);  // �������� You Win!
    }

    // ��������� ���� �� ���� �����
    public void FullScreen() {
        Screen.fullScreen = !Screen.fullScreen;
    }

    public void OpenDialog_menu() {
        Dialog_Menu.SetActive(true);
        Dialog.SetActive(false);
        Store.SetActive(false);
        StoreButton.SetActive(false);
        script_StoreManager.flagStoreUIOn = false; // ������ ��� ������� �� ������
        Dialog_current_mission.SetActive(false);
        SCRIPT_EngineBase.SwitchActive(); // ��� �������������� ���������� ui
        Time.timeScale = 0; // �����
    }
    public void CloseDialog_menu() {
        Dialog_Menu.SetActive(false);
        if (SpacePodZone.activeSelf) StoreButton.SetActive(true); // �������� ������ ��������
        SCRIPT_EngineBase.SwitchActive(); // ��� �������������� ���������� ui
        Time.timeScale = 1; // ������� �����
    }

    // �������� �������
    public void QualitySet(int quality) {
        QualitySettings.SetQualityLevel(quality, true);
        Debug.Log("setQ " + quality.ToString());
    }

    // ���������� �� ������, ��� ���� �����
    public void BAG_Player(bool key, bool fuel, bool energy) {
        BAG_IMG_Player_key.SetActive(key);
        BAG_IMG_Player_fuel.SetActive(fuel);
        BAG_IMG_Player_energy.SetActive(energy);
        BAG_Player_key = key;
        BAG_Player_fuel = fuel;
        BAG_Player_energy = energy;
    }

    // ���������� �������.
    void TriggerActivation() {
        if (GameState == 6) script_rigger_Terminal_fuel.ActiveTermonal(true); // ���� �������� FUEL
        if (GameState == 8) script_rigger_Terminal_energy.ActiveTermonal(true); // ���� �������� ENERGY
        if (GameState == 10) script_rigger_Terminal_key.ActiveTermonal(true); // ���� �������� KEY
    }

    // ���������, ����� ������� (�� 3-��) �����������
    public void CheckDestroyBatteryNum(int numBattery) {
        if (numBattery == 1) DestroyBatteryNum_1 = true;
        if (numBattery == 2) DestroyBatteryNum_2 = true;
        if (numBattery == 3) DestroyBatteryNum_3 = true;
        if (DestroyBatteryNum_1 && DestroyBatteryNum_2 && DestroyBatteryNum_3) DestroyBatteryAll = true;
    }

}