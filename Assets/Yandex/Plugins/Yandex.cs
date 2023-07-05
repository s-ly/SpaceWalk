using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Yandex : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void JS_LogUserID();
    [DllImport("__Internal")]
    private static extern void JS_Save(string data);
    [DllImport("__Internal")]
    private static extern void JS_Load();

    [SerializeField] MainMenu script_MainMenu;

    private void Start()
    {
        // script_MainMenu.PlayerDataShowInMainMenu();
    }

    /////////////////////////// ������ ���������� YANDEX
    public void Button_LogUserID()
    {
        JS_LogUserID();
    }    
    public void Button_Save()
    {
        string jsonString = JsonUtility.ToJson(ProgressManager.Instance.YandexDataOBJ); // ����������� ������ � ������
#if UNITY_WEBGL
        JS_Save(jsonString);
#endif
    }
    public void Button_Load()
    {
        JS_Load();
    }
    // �� ���������
    public void Button_Reset()
    {
        ProgressManager.Instance.YandexDataOBJ.Crystal = 0;
        ProgressManager.Instance.YandexDataOBJ.Oxygen = 20f;
        ProgressManager.Instance.YandexDataOBJ.TechnicalContainer = 0;
        ProgressManager.Instance.YandexDataOBJ.GameState = 0;
        ProgressManager.Instance.YandexDataOBJ.DATA_time_shot_pause = 0.3f;

        script_MainMenu.PlayerDataShowInMainMenu();
    }

    /////////////////////////// ���������� �� JS
    public void Show_LogUserID(string id)
    {
        script_MainMenu.Set_UserID_text_MainMenu(id);
    }
    public void LoadFromJS(string value)
    {
        // ��������� ������
        ProgressManager.Instance.YandexDataOBJ = JsonUtility.FromJson<YandexData>(value);

        // ���� ������ ��������� � Yandex, ��� ��� ������, � �� �������.
        if (ProgressManager.Instance.YandexDataOBJ.DATA_time_shot_pause == 0)
        {
            // ����� ��������� ��������, ���-��� ��� ����� �������� ����� �������� � �������.
            ProgressManager.Instance.YandexDataOBJ.DATA_time_shot_pause = 0.3f;
            ProgressManager.Instance.YandexDataOBJ.Oxygen = 20;

            // �������� ������ �������� ����
            script_MainMenu.Button_LoadGame_hide();
        }
        script_MainMenu.PlayerDataShowInMainMenu();
    }
}
