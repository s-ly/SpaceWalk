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

    public Sprite img_base;
    public Sprite img_crystall;
    public Sprite img_radar;
    public Sprite img_weapon;
    public Sprite img_shuttle;
    public Sprite img_engine;
    public Sprite img_fuel;
    public Sprite img_cyberlab;
    public Sprite img_energy;
    public Sprite img_battery;
    public Sprite img_gun;

    public Image img_mission;

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

    private string TextDialog_0 = "\t������� �������, ����� ��������, ��� ������� � ��������. "+
        "�� ������ ������ ��������, ������������� ��������. "+
        "<b><color=red>����� ������ �������� �� ����</color></b>"+
        ", ��� �������� �����������.�" +
        "\n\t��� ������������ ��������� ������� AWSD, ��� �������� � ������ �������, ��� ��������� ����������.";
    private string TextDialog_1 = "\t����� ��������� ������� ���. �� ���� ���� ����� ��� ���������. "+
        "���� �������� ��������� � ��������� �� �� ����, ����� �������� �����. "+
        "<b><color=red>����� ������� 10 ����������</color></b>"+
        ", �� ����� �����, �������� �� ���� "+
        "<b><color=red>� ������ ���������.</color></b> " +
        "\n\t��������� ���� �������, �� ����� ������� �� ����. �� ����� ���������� ���������, �� �������� �������� �����.";
    private string TextDialog_2 = "\t������ ���� ���������, �� �� ������� ���� ��� ������ ������ �������. "+
        "��� ���� ��������� ��������, � ��� ������ ���� ������� ������ ����������. "+
        "<b><color=red>����� ��������� ������. </color></b>"+
        "�� �����, � ������� �� ������. " +
        "\n\t����� ������� � ������, ����� �������� ����������� ���������� �����. "+
        "�� ��� ����� ������ ������ ��������� � ����������, ������� �������. "+
        "��� ������� ���������� ������� �������� �������������� ������� ����. "+
        "����������� ����� ��������� � ������� ��������� ��������� ������.";
    private string TextDialog_3 = "\t������������� ����� ������. ����� ��������, ��� ��� ����������. "+
        "�� � ������ ����� �������� ������, ����� �� ���������. "+
        "��� ������ ���� <b><color=red>����� �����������, ����� �</color></b>" +
        ", �����, �� ��� ����� �������� ������. " +
        "\n\t�������� ������� ������. ���, ���� �����������, ��������� ������� �� ������� ������� ���������� �����, "+
        "� ��������� �����. ��� ����� ���������� �����, ���� ������ �� ����� ������ ������� ����. �� ��� ����������� �������.";
    private string TextDialog_4 = "\t���������� �� ������ ������ �����. ����� ������ ������� � ���� �������. "+
        "��� ��������� �������, � ������� "+
        "<b><color=red>���������.</color></b> �� ��� �������� �� ���������������� �������. " +
        "�������, ��� ������� �����-������ ���������.�"+
        "\n\t��� ������ �� ����� �����������, �� ����� �����������, �����-�� ��������� ������� �� ����� ���������������. "+
        "��� ��� ����������?";
    private string TextDialog_5 = "\t������, ��� ������ �� �������. ����� ����������� ���-�� ��� ���-�� ���������. "+
        "��� �� ��������, ������� ��������� ��� ������. ��� ����� �������� ��������� ���� ����������� �����. "+
        "<b><color=red>����� ����� ������� ����������� ����������,</color></b>"+
        " ���, � ����� �������� ����������.�" +
        "\n\t���-����������, ���������� ����� ���������� ������� ���� ����� ������ ������� �� ������� ����, "+
        "��� �� ����� �������� ���������.";
    private string TextDialog_6 = "\t������, �������� ����������� ����������� ��������� ������������. "+
        "����� ������ �������. �� ��� ������ ����� �������. "+
        "<b><color=red>�� ���������� ������� ����� �������� ���� �������.</color></b> " +
        "���� ������� ����� ������� �� ������."+
        "\n\t��� ��������� ����, ��� ����� ��������� ���������� �����.";
    private string TextDialog_7 = "\t��� ��� �� ������, ������� ������. ����� �������� ���� �����, "+
        "����� ��� ������ � ������ ���� ���������. ��� �������� ������� �������� �����. "+
        "���� � <b><color=red>����� ���������������� ������������</color></b>" +
        ", � ����� �������� ���. "+
        "\n\t�������� ���� ��������, ����� � ���� �������� ��������, � ��� �������� ����. "+
        "���� ���� �������� �����������������, �� ������ ���� ����� �� ���.";
    private string TextDialog_8 = "\t��� ������ ����� �������. "+
        "<b><color=red>���� ������� ������ ���� �� ���������������</color></b>. ����� ����� �." +
        "���� ������� ����� ������� �� ������. " +
        "\n\t������ ������ ��� �������, ���������� ��� ���-��. ����� ����� ������ ���������� �������� �������.";
    private string TextDialog_9 = "\t�� �������������� � �������, � ����� ����� ������� ���������� ������ ��������, "+
        "�� � ���� ��� �������. ��� ����� ���������. �������� ������ "+
        "<b><color=red>3 ��������, ����� ���������� �� ���</color></b>." + 
        "\n\t�� ����� �������� ��� �����, ��� ����������� ��������. ��� ����� ������!";
    private string TextDialog_10 = "\t��� ��� �������� ����������, � ������ ��������� �������. "+
        "�������� <b><color=red>����� ���� ������� ��������� �������� � �������� �� ������</color></b>." +
        "\n\t�������, ��� ����� �� ��������� ���������, ��� ������� �� � ������ �����, � ������ ��� ��������? "+
        "��� ������� ������, ����� ������� ���� � �������.�";
    private string TextDialog_11 = "\t������, ���-�� ���������� ���������������� ������ ������ �� ���������. "+
        "��� � �����? � ��������� ��� ��������� � ����. "+
        "\n\t<b><color=red>������!</color></b>";

    [SerializeField] Yandex script_Yandex;

    // ����� ������� �����������
    bool DestroyBatteryNum_1 = false;
    bool DestroyBatteryNum_2 = false;
    bool DestroyBatteryNum_3 = false;
    public bool DestroyBatteryAll = false;

    // ���������� UI
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
                //script_Yandex.ShowAdv();
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
            img_mission.sprite = img_base;
            TextDialog.text = TextDialog_0;
            Text_Dialog_current_mission.text = "������� ����: " + TextDialog_0;
        }
        if (GameState == 1) {
            img_mission.sprite = img_crystall;
            TextDialog.text = TextDialog_1;
            Text_Dialog_current_mission.text = "������� ����: " + TextDialog_1;
        }
        if (GameState == 2) {
            img_mission.sprite = img_radar;
            TextDialog.text = TextDialog_2;
            Text_Dialog_current_mission.text = "������� ����: " + TextDialog_2;
        }
        if (GameState == 3) {
            img_mission.sprite = img_weapon;
            TextDialog.text = TextDialog_3;
            Text_Dialog_current_mission.text = "������� ����: " + TextDialog_3;
        }
        if (GameState == 4) {
            img_mission.sprite = img_shuttle;
            TextDialog.text = TextDialog_4;
            Text_Dialog_current_mission.text = "������� ����: " + TextDialog_4;
        }
        if (GameState == 5) {
            img_mission.sprite = img_engine;
            TextDialog.text = TextDialog_5;
            Text_Dialog_current_mission.text = "������� ����: " + TextDialog_5;
        }
        if (GameState == 6) {
            img_mission.sprite = img_fuel;
            TextDialog.text = TextDialog_6;
            Text_Dialog_current_mission.text = "������� ����: " + TextDialog_6;
        }
        if (GameState == 7) {
            img_mission.sprite = img_cyberlab;
            TextDialog.text = TextDialog_7;
            Text_Dialog_current_mission.text = "������� ����: " + TextDialog_7;
        }
        if (GameState == 8) {
            img_mission.sprite = img_energy;
            TextDialog.text = TextDialog_8;
            Text_Dialog_current_mission.text = "������� ����: " + TextDialog_8;
        }
        if (GameState == 9) {
            img_mission.sprite = img_battery;
            TextDialog.text = TextDialog_9;
            Text_Dialog_current_mission.text = "������� ����: " + TextDialog_9;
        }
        if (GameState == 10) {
            img_mission.sprite = img_gun;
            TextDialog.text = TextDialog_10;
            Text_Dialog_current_mission.text = "������� ����: " + TextDialog_10;
        }
        if (GameState == 11) {
            //img_mission.sprite = img_base;
            TextDialog.text = TextDialog_11;
            Text_Dialog_current_mission.text = "������� ����: " + TextDialog_11;
        }
        Dialog.SetActive(true);

        //SCRIPT_EngineBase.SwitchActive();
        //SCRIPT_EngineBase.Zone.SetActive(false);
        //SCRIPT_EngineBase.store.SetActive(false);

        //SRC_LabBase.SwitchActive();
        //SRC_LabBase.Zone.SetActive(false);
        //SRC_LabBase.store.SetActive(false);

        //STC_WeaponsBase.SwitchActive();
        //STC_WeaponsBase.Zone.SetActive(false);
        //STC_WeaponsBase.store.SetActive(false);
        STC_WeaponsBase.Store_off();
        SRC_LabBase.Store_off();
        SCRIPT_EngineBase.Store_off();

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

        //SCRIPT_EngineBase.SwitchActive();
        //SRC_LabBase.SwitchActive();
        //STC_WeaponsBase.SwitchActive();
        STC_WeaponsBase.flag_ui_on = true;
        SRC_LabBase.flag_ui_on = true;
        SCRIPT_EngineBase.flag_ui_on = true;

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

        // ��� �������������� ���������� ui
        //SCRIPT_EngineBase.SwitchActive();
        //SRC_LabBase.SwitchActive();
        //STC_WeaponsBase.SwitchActive();
        SCRIPT_EngineBase.Store_off();
        SRC_LabBase.Store_off();
        STC_WeaponsBase.Store_off();

        Time.timeScale = 0; // �����
    }
    public void CloseDialog_menu() {
        Dialog_Menu.SetActive(false);
        if (SpacePodZone.activeSelf) StoreButton.SetActive(true); // �������� ������ ��������

        //��� �������������� ���������� ui
        //SCRIPT_EngineBase.SwitchActive();
        //SRC_LabBase.SwitchActive();
        //STC_WeaponsBase.SwitchActive();
        STC_WeaponsBase.flag_ui_on = true;
        SRC_LabBase.flag_ui_on = true;
        SCRIPT_EngineBase.flag_ui_on = true;

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

    // ���������� ������� ��������� ����� ������
    // � �������: key fuel energy
    public bool[] BAG_Player_curent() {
        bool[] bag = new bool[3];
        bag[0] = BAG_Player_key;
        bag[1] = BAG_Player_fuel;
        bag[2] = BAG_Player_energy;
        return bag;
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