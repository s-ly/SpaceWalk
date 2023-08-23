using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// ������� ����������
public class GameManager : MonoBehaviour
{
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

    private string TextDialog_0 = "���������� �� ����.";
    private string TextDialog_1 = "������ 10 ����������, ������� ����� ���������.";
    private string TextDialog_2 = "����� �����.";
    private string TextDialog_3 = "����� ��������� ������� � �������� � ������.";
    private string TextDialog_4 = "����� �������������� ������� � �������� � ������.";
    private string TextDialog_5 = "����� ������� ������ � �������� � ������.";
    private string TextDialog_6 = "�� ��� ��������.";

    [SerializeField] Yandex script_Yandex;

    //string dev_text;

    

    // Start is called before the first frame update
    void Start()
    {        
        GameState = ProgressManager.Instance.YandexDataOBJ.GameState; // �������� ��������� ����
        //TouchKeyboardActive = ProgressManager.Instance.YandexDataOBJ.TouchKeyboardActive;

        // ������ � �������� ������ � ��������� ���������
        scripc_player = Player.GetComponent<player>(); 
        script_crystalManager = crystalManager.GetComponent<crystalManager>();

        // ������� �������-����������
        script_rigger_Terminal_key = Trigger_Terminal_key.GetComponent<Trigger_Terminal>();
        script_rigger_Terminal_fuel = Trigger_Terminal_fuel.GetComponent<Trigger_Terminal>();
        script_rigger_Terminal_energy = Trigger_Terminal_energy.GetComponent<Trigger_Terminal>();

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
    void Update()
    {        
        // ����� ������ ������ ����� �������
        if (TimerDeath > 0 && DeathTriger)
        {
            TimerDeath = TimerDeath - Time.deltaTime;
            if (TimerDeath <= 0)
            {
                SceneManager.LoadScene(2);  // �������� GAME OVER
            }
        }
    }

    // ������ ��������� �������� � ���������.
    // ���-��� � ����� �������� ���� ����������� ��� ������, �� ��� �����,
    // � ����� ���������� ���� �����.
    IEnumerator Pause_TriggerActivation(float pauseSec)
    {
        yield return new WaitForSeconds(pauseSec);
        TriggerActivation();
    }

    // �������� ��������� ����
    public void Check_GameState(string GameEvent)
    {
        switch (GameEvent)
        {
            case "PlayerEnterSpacePod":
                {
                    // ��������� 0 - ����� � ������ ����, ��� ���� ������ �������� �� ����.
                    if (GameState == 0)
                    {
                        GameState = 1; // ��������� � ��������� 1
                        ProgressManager.Instance.YandexDataOBJ.GameState = GameState; // ���������� ������ ����� �������� 
                        OpenDialogMission();
                    }
#if UNITY_WEBGL
                    script_Yandex.Button_Save();
#endif
                    break;
                }
            case "PlayerBayOxygen":
                {
                    // ��������� 1 - ����� ������ ������� 10 ���������� � ������ ���������� ���������.
                    if (GameState == 1)
                    {
                        GameState = 2; // ��������� � ��������� 2
                        ProgressManager.Instance.YandexDataOBJ.GameState = GameState; // ���������� ������ ����� ��������
                        OpenDialogMission();
#if UNITY_WEBGL
                        script_Yandex.Button_Save();
#endif
                    }
                    break;
                }
            case "PlayerEnter_Space_Shuttle":
                {
                    // ��������� 2 - ����� ���� ����.
                    if (GameState == 2)
                    {
                        GameState = 3; // ��������� � ��������� 
                        ProgressManager.Instance.YandexDataOBJ.GameState = GameState; // ���������� ������ ����� ��������
                        OpenDialogMission();
                    }
                    // ��������� 3 - ����� ������ �������� �� ���� FUEL.
                    if (GameState == 3 && BAG_Player_fuel)
                    {
                        GameState = 4; // ��������� � ��������� 
                        ProgressManager.Instance.YandexDataOBJ.GameState = GameState; // ���������� ������ ����� ��������
                        BAG_Player(false, false, false); // � ������ � ����� ������ ���
                        OpenDialogMission();
                    }
                    // ��������� 4 - ����� ������ �������� �� ���� ENERGY.
                    if (GameState == 4 && BAG_Player_energy)
                    {
                        GameState = 5; // ��������� � ��������� 
                        ProgressManager.Instance.YandexDataOBJ.GameState = GameState; // ���������� ������ ����� ��������
                        BAG_Player(false, false, false); // � ������ � ����� ������ ���
                        OpenDialogMission();
                    }
                    // ��������� 5 - ����� ������ �������� �� ���� KEY.
                    if (GameState == 5 && BAG_Player_key)
                    {
                        GameState = 6; // ��������� � ��������� 
                        ProgressManager.Instance.YandexDataOBJ.GameState = GameState; // ���������� ������ ����� ��������
                        BAG_Player(false, false, false); // � ������ � ����� ������ ���
                        YouWin(); // ����� ����
                    }
                    TriggerActivation();
#if UNITY_WEBGL
                    script_Yandex.Button_Save();
#endif
                    break;
                }
        }
    }

    // ���������� ������
    public void OpenDialogMission()
    {
        if (GameState == 0) 
        {
            TextDialog.text = TextDialog_0;
            Text_Dialog_current_mission.text = "������� ����: " + TextDialog_0;
        }
        if (GameState == 1)
        {
            TextDialog.text = TextDialog_1;
            Text_Dialog_current_mission.text = "������� ����: " + TextDialog_1;
        }
        if (GameState == 2)
        {
            TextDialog.text = TextDialog_2;
            Text_Dialog_current_mission.text = "������� ����: " + TextDialog_2;
        }
        if (GameState == 3)
        {
            TextDialog.text = TextDialog_3;
            Text_Dialog_current_mission.text = "������� ����: " + TextDialog_3;
        }
        if (GameState == 4)
        {
            TextDialog.text = TextDialog_4;
            Text_Dialog_current_mission.text = "������� ����: " + TextDialog_4;
        }
        if (GameState == 5)
        {
            TextDialog.text = TextDialog_5;
            Text_Dialog_current_mission.text = "������� ����: " + TextDialog_5;
        }
        if (GameState == 6)
        {
            TextDialog.text = TextDialog_6;
            Text_Dialog_current_mission.text = "������� ����: " + TextDialog_6;
        }
        Dialog.SetActive(true);
        if (StoreButton.activeSelf) StoreButton.SetActive(false); // ������ ������ ��������
        if (Store.activeSelf) 
        { 
            Store.SetActive(false); // ������ �������
            script_StoreManager.flagStoreUIOn = false; // ������ ��� ������� �� ������
        } 

        Time.timeScale = 0; // �����
    }
    
    // ��������� ������
    public void CloseDialog(){
        Dialog.SetActive(false);
        if (SpacePodZone.activeSelf) StoreButton.SetActive(true); // �������� ������ ��������
        Time.timeScale = 1; // ������� �����
    }

    public void LoadLevel(){
        SceneManager.LoadScene(1);  // �������� ������
    }

    public void LoadMainMenu(){
        SceneManager.LoadScene(0);  // �������� �������� ����
    }

    public void LoadHelp(){
        SceneManager.LoadScene(4);  // �������� ������
    }

    public void ReloadGame(){
        SceneManager.LoadScene(0);  // �������� ���� ����� (����� ��������)
    }

    // ����� ����
    public void GameOwer(){
        script_crystalManager.SaveDataCrystal(); // ���������� ������
        animatorPlayer.SetTrigger("Death"); // ������ �������� ������
        scripc_player.enabled = false; // �������� ������ ������
        DeathTriger = true; // ������ ������� ������������
    }

    // ����� �������
    public void YouWin(){
        Debug.Log("You Win!");
        SceneManager.LoadScene(3);  // �������� You Win!
    }

    // ��������� ���� �� ���� �����
    public void FullScreen(){
        Screen.fullScreen = !Screen.fullScreen;
    }

    public void OpenDialog_menu()
    {
        Dialog_Menu.SetActive(true);
        Dialog.SetActive(false);
        Store.SetActive(false);
        StoreButton.SetActive(false);
        script_StoreManager.flagStoreUIOn = false; // ������ ��� ������� �� ������
        Dialog_current_mission.SetActive(false);
        Time.timeScale = 0; // �����
    }
    public void CloseDialog_menu()
    {
        Dialog_Menu.SetActive(false);
        if (SpacePodZone.activeSelf) StoreButton.SetActive(true); // �������� ������ ��������
        Time.timeScale = 1; // ������� �����
    }
    
    // �������� �������
    public void QualitySet(int quality)
    {
        QualitySettings.SetQualityLevel(quality, true);
        Debug.Log("setQ " + quality.ToString());
    }

    // ���������� �� ������, ��� ���� �����
    public void BAG_Player(bool key, bool fuel, bool energy)
    {
        BAG_IMG_Player_key.SetActive(key);
        BAG_IMG_Player_fuel.SetActive(fuel);
        BAG_IMG_Player_energy.SetActive(energy);
        BAG_Player_key = key;
        BAG_Player_fuel = fuel;
        BAG_Player_energy = energy;
    }

    // ���������� �������.
    void TriggerActivation()
    {
        if (GameState == 3) script_rigger_Terminal_fuel.ActiveTermonal(true); // ���� �������� FUEL
        if (GameState == 4) script_rigger_Terminal_energy.ActiveTermonal(true); // ���� �������� ENERGY
        if (GameState == 5) script_rigger_Terminal_key.ActiveTermonal(true); // ���� �������� KEY
    }

}