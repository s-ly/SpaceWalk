using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI UserID_text_MainMenu;
    [SerializeField] TextMeshProUGUI PlayerDataText;
    [SerializeField] Yandex script_Yandex;
    [SerializeField] GameObject ButtonLoadGame;

    void Start()
    {        
        // Set_UserID_text_MainMenu("none");
        PlayerDataShowInMainMenu();

        #if UNITY_WEBGL
        // Debug.Log("Unity Editor");
        // script_Yandex.Button_LogUserID(); // ���������� id ������������
        script_Yandex.Button_Load();
        #endif
    }

    public void Set_UserID_text_MainMenu(string id_text)
    {
        UserID_text_MainMenu.text = "��� id: " + id_text;
    }

    // �������� ������ "��������� ���������� ����"
    public void Button_LoadGame_hide()
    {
        ButtonLoadGame.GetComponent<Button>().interactable = false;
    }
    
    // ���������� ������ ������ � ������� ����.
    public void PlayerDataShowInMainMenu()
    {        
        string Crystal = ProgressManager.Instance.YandexDataOBJ.Crystal.ToString();
        string Oxygen = ProgressManager.Instance.YandexDataOBJ.Oxygen.ToString(); 
        string TechnicalContainer = ProgressManager.Instance.YandexDataOBJ.TechnicalContainer.ToString(); 
        string Level = ProgressManager.Instance.YandexDataOBJ.GameState.ToString(); 
        string Rifle_shot_pause = ProgressManager.Instance.YandexDataOBJ.DATA_time_shot_pause.ToString();

        string PlayerDataString = (
            "������� ��������: " + "\n" +
            "���������: " + Crystal + "\n" +
            "��������: " + Oxygen + "\n" +
            "���-����������: " + TechnicalContainer + "\n" +
            "�������: " + Level + "\n" +
            "����� �����������: " + Rifle_shot_pause);

        PlayerDataText.text= PlayerDataString;
    }
}
