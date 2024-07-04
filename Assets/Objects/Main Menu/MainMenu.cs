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

  public TextMeshProUGUI text_game_name;


  //LOCAL
  // string main_UserID;


  void Start() {
    // LOCAL
    // main_UserID = TextManager.Inst_TextData.textsData.main_UserID;

    TextLoad.SetActive(false);
    Set_UserID_text_MainMenu("none");
    PlayerDataShowInMainMenu();
    //Set_Text_DeviceInfo();

#if UNITY_WEBGL
    // Debug.Log("Unity Editor");
    script_Yandex.Button_LogUserID(); // показывает id пользователя
    script_Yandex.Button_Load();
    //script_Yandex.Yandex_JS_DeviceInfo(); // узнать платформу
#endif
  }

  public void Set_UserID_text_MainMenu(string id_text) {
    UserID_text_MainMenu.text = "id: " + id_text;
  }

  // Скрывает кнопку "Загрузить сохранённую игру"
  public void Button_LoadGame_hide() {
    ButtonLoadGame.GetComponent<Button>().interactable = false;
  }

  // Показывает данные игрока в главном меню.
  public void PlayerDataShowInMainMenu() {
    // LOCAL
    string main_PlDatStr_GamP = TextManager.Inst_TextData.textsData.main_PlDatStr_GamP;
    string main_PlDatStr_Crys = TextManager.Inst_TextData.textsData.main_PlDatStr_Crys;
    string main_PlDatStr_MaOx = TextManager.Inst_TextData.textsData.main_PlDatStr_MaOx;
    string main_PlDatStr_Tech = TextManager.Inst_TextData.textsData.main_PlDatStr_Tech;
    string main_PlDatStr_CurT = TextManager.Inst_TextData.textsData.main_PlDatStr_CurT;
    string main_PlDatStr_WeRT = TextManager.Inst_TextData.textsData.main_PlDatStr_WeRT;
    string main_PlDatStr_PSpe = TextManager.Inst_TextData.textsData.main_PlDatStr_PSpe;
    string main_PlDatStr_Phea = TextManager.Inst_TextData.textsData.main_PlDatStr_Phea;
    string main_PlDatStr_MaFu = TextManager.Inst_TextData.textsData.main_PlDatStr_MaFu;
    string main_PlDatStr_PrFi = TextManager.Inst_TextData.textsData.main_PlDatStr_PrFi;
    // END LOCAL

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
        main_PlDatStr_GamP + "\n" +
        main_PlDatStr_Crys + Crystal + "\n" +
        main_PlDatStr_MaOx + Oxygen + "\n" +
        main_PlDatStr_Tech + TechnicalContainer + "\n" +
        main_PlDatStr_CurT + Level + "\n" +
        main_PlDatStr_WeRT + Rifle_shot_pause + "\n" +
        main_PlDatStr_PSpe + player_speed + "\n" +
        main_PlDatStr_Phea + player_health + "\n" +
        main_PlDatStr_MaFu + fuel + "\n" +
        main_PlDatStr_PrFi + battary);

    PlayerDataText.text = PlayerDataString;
  }

  // Увеличивает скорость игрока
  public void DEV_BUTTON_speed_player() {
    ProgressManager.Instance.YandexDataOBJ.DATA_player_speed = 3.0f;
    PlayerDataShowInMainMenu();
  }

  // Увеличивает кислород игрока
  public void DEV_BUTTON_oxygen_player() {
    ProgressManager.Instance.YandexDataOBJ.Oxygen = 20000.0f;
    PlayerDataShowInMainMenu();
  }

  // Увеличивает здоровье игрока
  public void DEV_BUTTON_health_player() {
    ProgressManager.Instance.YandexDataOBJ.DATA_player_health = 100000;
    PlayerDataShowInMainMenu();
  }

  // Увеличивает техконтейнеры и кристаллы игрока
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
