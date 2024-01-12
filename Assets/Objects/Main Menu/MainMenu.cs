using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {
    [SerializeField] TextMeshProUGUI UserID_text_MainMenu;
    [SerializeField] TextMeshProUGUI PlayerDataText;
    [SerializeField] Yandex script_Yandex;
    [SerializeField] GameObject ButtonLoadGame;
    [SerializeField] GameObject ButtonStartGame;
    [SerializeField] GameObject TextLoad;

    // ��� ������������ �������� (���� �� ����)
    //private bool isLoading = false; //��������� �� ���� � ��������� �������

    void Start() {
        TextLoad.SetActive(false);
        Set_UserID_text_MainMenu("none");
        PlayerDataShowInMainMenu();
        //Set_Text_DeviceInfo();

#if UNITY_WEBGL
        // Debug.Log("Unity Editor");
        script_Yandex.Button_LogUserID(); // ���������� id ������������
        script_Yandex.Button_Load();
        //script_Yandex.Yandex_JS_DeviceInfo(); // ������ ���������
#endif
    }

    public void Set_UserID_text_MainMenu(string id_text) {
        UserID_text_MainMenu.text = "��� id: " + id_text;
    }

    // �������� ������ "��������� ���������� ����"
    public void Button_LoadGame_hide() {
        ButtonLoadGame.GetComponent<Button>().interactable = false;
    }

    // ���������� ������ ������ � ������� ����.
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
            "������� ��������: " + "\n" +
            "���������: " + Crystal + "\n" +
            "������������ ����� ���������: " + Oxygen + "\n" +
            "���-����������: " + TechnicalContainer + "\n" +
            "������� �������: " + Level + "\n" +
            "����� ����������� ������: " + Rifle_shot_pause + "\n" +
            "�������� ������: " + player_speed + "\n" +
            "�������� ������: " + player_health + "\n" +
            "������������ ����� �������: " + fuel + "\n" +
            "������ ��������� ����: " + battary);

        PlayerDataText.text = PlayerDataString;
    }

    // ����������� �������� ������
    public void DEV_BUTTON_speed_player() {
        ProgressManager.Instance.YandexDataOBJ.DATA_player_speed = 3.0f;
        PlayerDataShowInMainMenu();
    }

    // ����������� �������� ������
    public void DEV_BUTTON_oxygen_player() {
        ProgressManager.Instance.YandexDataOBJ.Oxygen = 20000.0f;
        PlayerDataShowInMainMenu();
    }

    // ����������� �������� ������
    public void DEV_BUTTON_health_player() {
        ProgressManager.Instance.YandexDataOBJ.DATA_player_health = 100000;
        PlayerDataShowInMainMenu();
    }

    // ����������� ������������� � ��������� ������
    public void DEV_BUTTON_resourse_player() {
        ProgressManager.Instance.YandexDataOBJ.Crystal = 1000;
        ProgressManager.Instance.YandexDataOBJ.TechnicalContainer = 1000;
        PlayerDataShowInMainMenu();
    }


    public void DEV_BUTTON_stat_3() {
        ProgressManager.Instance.YandexDataOBJ.GameState = 3;
        PlayerDataShowInMainMenu();
    }
    public void DEV_BUTTON_state_5() {
        ProgressManager.Instance.YandexDataOBJ.GameState = 5;
        PlayerDataShowInMainMenu();
    }
    public void DEV_BUTTON_state_7() {
        ProgressManager.Instance.YandexDataOBJ.GameState = 7;
        PlayerDataShowInMainMenu();
    }
    public void DEV_BUTTON_state_9() {
        ProgressManager.Instance.YandexDataOBJ.GameState = 9;
        PlayerDataShowInMainMenu();
    }

    // ��� ������������ �������� (���� �� ����)
    //public void LoadLevel() {
    //    if (!isLoading) {
    //        StartCoroutine(ENUM_LoadLevel());
    //    }
    //}

    //IEnumerator ENUM_LoadLevel() {
    //    isLoading = true;
    //    ButtonStartGame.SetActive(false);
    //    ButtonLoadGame.SetActive(false);
    //    TextLoad.SetActive(true);
    //    yield return null;
    //    AsyncOperation asyncOp = SceneManager.LoadSceneAsync(1);

    //    // Wait until the asynchronous scene fully loads
    //    while (!asyncOp.isDone) {
    //        yield return null;
    //    }

    //    isLoading = false;
    //}

    public void LoadLevel() {
        StartCoroutine(ENUM_LoadLevel());
    }

    IEnumerator ENUM_LoadLevel() {
        ButtonStartGame.SetActive(false);
        yield return new WaitUntil(() => !ButtonStartGame.gameObject.activeSelf);
        ButtonLoadGame.SetActive(false);
        yield return new WaitUntil(() => !ButtonLoadGame.gameObject.activeSelf);
        TextLoad.SetActive(true);
        yield return new WaitUntil(() => TextLoad.gameObject.activeSelf);

        SceneManager.LoadScene(1);
    }
}
